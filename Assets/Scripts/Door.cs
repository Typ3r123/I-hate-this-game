using UnityEngine;

public class Door : MonoBehaviour
{
    [Header("Спрайты")]
    public Sprite openSprite;           // спрайт открытой двери (назначь в инспекторе)
    private Sprite closedSprite;        // исходный спрайт (сохраняется при старте)

    [Header("Звук")]
    public AudioClip openSound;         // звук открытия
    [Range(0f, 1f)] public float soundVolume = 1f; // громкость в инспекторе

    [Header("Компоненты")]
    private SpriteRenderer sr;
    private BoxCollider2D col;
    private AudioSource audioSource;

    void Awake()
    {
        // Получаем компоненты
        sr = GetComponent<SpriteRenderer>();
        col = GetComponent<BoxCollider2D>();

        if (sr == null)
        {
            Debug.LogError("Нет SpriteRenderer на двери!");
            return;
        }

        if (col == null)
        {
            Debug.LogError("Нет BoxCollider2D на двери!");
            return;
        }

        // Сохраняем исходный спрайт
        closedSprite = sr.sprite;

        // Получаем или создаём AudioSource
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
            audioSource.playOnAwake = false;
            audioSource.spatialBlend = 0f; // 2D звук
        }
    }

    public void Open()
    {
        if (sr == null || col == null)
        {
            Debug.LogWarning("Компоненты двери не найдены. Открытие невозможно.");
            return;
        }

        // Меняем спрайт
        if (openSprite != null)
        {
            sr.sprite = openSprite;
            Debug.Log("Спрайт двери изменён на 'Open'");
        }
        else
        {
            Debug.LogWarning("openSprite не назначен. Спрайт не изменён.");
        }

        // Делаем проходимой
        col.enabled = false;
        Debug.Log("Коллизия двери отключена");

        // Проигрываем звук
        if (openSound != null && audioSource != null)
        {
            audioSource.PlayOneShot(openSound, soundVolume);
            Debug.Log($"Звук открытия проигран с громкостью {soundVolume}");
        }
        else
        {
            Debug.LogWarning("openSound не назначен или нет AudioSource");
        }
    }

    public void Close()
    {
        if (sr == null || col == null)
        {
            Debug.LogWarning("Компоненты двери не найдены. Закрытие невозможно.");
            return;
        }

        // Возвращаем исходный спрайт
        if (closedSprite != null)
        {
            sr.sprite = closedSprite;
            Debug.Log("Спрайт двери восстановлен на 'Closed'");
        }
        else
        {
            Debug.LogWarning("Исходный спрайт не сохранён. Спрайт не изменён.");
        }

        // Делаем непроходимой
        col.enabled = true;
        Debug.Log("Коллизия двери включена");
    }
}