using UnityEngine;

public class ClickableSprite : MonoBehaviour
{
    [SerializeField] private Sprite[] sprites; // Массив спрайтов для каждого клика
    [SerializeField] private AudioClip clickSound; // Звук при клике (опционально)
    [SerializeField] private float soundVolume = 1f; // Громкость звука
    [SerializeField] private GameObject keyPrefab; // Префаб ключа для выпадения
    [SerializeField] private float dropChance = 1f; // Шанс выпадения (1 = 100%)
    
    private SpriteRenderer spriteRenderer;
    private AudioSource audioSource;
    private int clickCount = 0;
    private int maxClicks = 7; // На пятом клике исчезает
    private static bool keyHasBeenDropped = false; // Статический флаг — ключ выпадает только один раз
    private bool isDestroying = false; // Флаг, чтобы не уничтожать дважды

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        audioSource = GetComponent<AudioSource>();
        
        // Если AudioSource не найден — создаём его
        if (audioSource == null && clickSound != null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
            audioSource.volume = soundVolume;
        }
        
        // Устанавливаем начальный спрайт
        if (sprites.Length > 0 && spriteRenderer != null)
        {
            spriteRenderer.sprite = sprites[0];
        }
    }

    void Update()
    {
        // Проверяем клик мышью
        if (Input.GetMouseButtonDown(0))
        {
            HandleClick();
        }
    }

    private void HandleClick()
    {
        // Проверяем, находится ли мышь над объектом
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = 10f;
        Vector3 worldMousePos = Camera.main.ScreenToWorldPoint(mousePos);

        Collider2D collider = GetComponent<Collider2D>();
        if (collider != null && collider.bounds.Contains(worldMousePos))
        {
            clickCount++;

            // Если это пятый клик — исчезаем
            if (clickCount >= maxClicks)
            {
                if (isDestroying) return; // Если уже уничтожаем — выходим
                isDestroying = true;
                
                // Воспроизводим звук перед исчезновением
                if (clickSound != null && audioSource != null)
                {
                    audioSource.PlayOneShot(clickSound, soundVolume);
                }
                
                // Проверяем шанс выпадения ключа
                if (!keyHasBeenDropped && Random.value < dropChance)
                {
                    // Выпадаем ключ
                    if (keyPrefab != null)
                    {
                        Instantiate(keyPrefab, transform.position, Quaternion.identity);
                        keyHasBeenDropped = true; // Больше не спавним ключ
                        Debug.Log("Ключ выпал!");
                    }
                }
                
                // Отключаем коллайдер, чтобы игрок не попал внутрь
                Collider2D col = GetComponent<Collider2D>();
                if (col != null)
                    col.enabled = false;
                
                // Уничтожаем объект с задержкой
                Destroy(gameObject, 0.1f);
                return;
            }

            // Меняем спрайт на соответствующий клику
            if (clickCount - 1 < sprites.Length && spriteRenderer != null)
            {
                spriteRenderer.sprite = sprites[clickCount - 1];
            }

            // Воспроизводим звук
            if (clickSound != null && audioSource != null)
            {
                audioSource.PlayOneShot(clickSound, soundVolume);
            }
        }
    }

    // Метод для сброса флага (вызови при загрузке нового уровня)
    public static void ResetKeyDropFlag()
    {
        keyHasBeenDropped = false;
    }
}
