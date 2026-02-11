using UnityEngine;

public class DestructibleBlock : MonoBehaviour
{
    [SerializeField] private GameObject keyPrefab; // Префаб ключа для выпадения
    [SerializeField] private float dropChance = 0.02f; // 2% шанс выпадения (0.02 = 2%)
    [SerializeField] private AudioClip breakSound; // Звук при разрушении
    [SerializeField] private float soundVolume = 1f; // Громкость звука
    
    private AudioSource audioSource;
    private static bool keyHasBeenDropped = false; // Статический флаг для всех блоков

    void Start()
    {
        // Создаём AudioSource, если его нет
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null && breakSound != null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
            audioSource.playOnAwake = false;
            audioSource.spatialBlend = 0f; // 2D звук
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        // Проверяем, что это игрок или какой-то объект, который разрушает блок
        if (collision.CompareTag("Player") || collision.name.Contains("Projectile"))
        {
            DestroyBlock();
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        // Альтернативный способ разрушения через коллизию
        if (collision.gameObject.CompareTag("Player"))
        {
            DestroyBlock();
        }
    }

    private void DestroyBlock()
    {
        // Воспроизводим звук разрушения
        if (breakSound != null && audioSource != null)
        {
            audioSource.PlayOneShot(breakSound, soundVolume);
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

        Debug.Log($"Блок разрушен! Шанс был: {dropChance * 100}%");

        // Уничтожаем блок
        Destroy(gameObject);
    }

    // Метод для сброса флага (если нужно перезагрузить уровень)
    public static void ResetKeyDropFlag()
    {
        keyHasBeenDropped = false;
    }
}
