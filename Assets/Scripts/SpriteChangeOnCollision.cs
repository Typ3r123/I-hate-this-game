using UnityEngine;

public class SpriteChangeOnCollision : MonoBehaviour
{
    [SerializeField] private Sprite newSprite; // Новый спрайт при столкновении
    [SerializeField] private bool changeOnce = true; // Менять спрайт только один раз
    [SerializeField] private string collisionTag = ""; // Тег объекта для столкновения (оставить пусто для любого)
    [SerializeField] private GameObject specificObject; // Конкретный объект для столкновения (оставить пусто для любого)
    [SerializeField] private bool disableScript = true; // Отключить скрипт при столкновении
    [SerializeField] private MonoBehaviour scriptToDisable; // Скрипт который отключить
    [SerializeField] private AudioClip collisionSound; // Звук при столкновении
    [SerializeField] private float soundVolume = 1f; // Громкость звука (0-1)
    
    private SpriteRenderer spriteRenderer;
    private Sprite originalSprite;
    private bool hasChanged = false;
    private AudioSource audioSource;
    
    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer != null)
        {
            originalSprite = spriteRenderer.sprite;
        }
        else
        {
            Debug.LogWarning("SpriteChangeOnCollision: SpriteRenderer не найден!");
        }
        
        // Создаем AudioSource если его нет
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
    }
    
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Если нужно менять только один раз и уже изменили - выходим
        if (changeOnce && hasChanged)
            return;
        
        // Проверяем конкретный объект
        if (specificObject != null && collision.gameObject != specificObject)
            return;
        
        // Если указан тег - проверяем его
        if (!string.IsNullOrEmpty(collisionTag))
        {
            if (!collision.gameObject.CompareTag(collisionTag))
                return;
        }
        
        // Меняем спрайт
        if (spriteRenderer != null && newSprite != null)
        {
            spriteRenderer.sprite = newSprite;
            hasChanged = true;
            Debug.Log("Спрайт изменен при столкновении с " + collision.gameObject.name);
        }
        
        // Воспроизводим звук
        PlayCollisionSound();
        
        // Отключаем скрипт
        if (disableScript)
        {
            DisableScript();
        }
    }
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Если нужно менять только один раз и уже изменили - выходим
        if (changeOnce && hasChanged)
            return;
        
        // Проверяем конкретный объект
        if (specificObject != null && collision.gameObject != specificObject)
            return;
        
        // Если указан тег - проверяем его
        if (!string.IsNullOrEmpty(collisionTag))
        {
            if (!collision.gameObject.CompareTag(collisionTag))
                return;
        }
        
        // Меняем спрайт
        if (spriteRenderer != null && newSprite != null)
        {
            spriteRenderer.sprite = newSprite;
            hasChanged = true;
            Debug.Log("Спрайт изменен при входе в триггер " + collision.gameObject.name);
        }
        
        // Воспроизводим звук
        PlayCollisionSound();
        
        // Отключаем скрипт
        if (disableScript)
        {
            DisableScript();
        }
    }
    
    private void PlayCollisionSound()
    {
        if (audioSource != null && collisionSound != null)
        {
            audioSource.PlayOneShot(collisionSound, soundVolume);
            Debug.Log("Звук столкновения воспроизведен");
        }
    }
    
    private void DisableScript()
    {
        if (scriptToDisable != null)
        {
            scriptToDisable.enabled = false;
            Debug.Log(scriptToDisable.GetType().Name + " отключен");
            
            // Обнуляем velocity Rigidbody2D
            Rigidbody2D rb = GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                rb.velocity = Vector2.zero;
                rb.angularVelocity = 0f;
                
                // Замораживаем Rigidbody2D чтобы объект не двигался
                rb.constraints = RigidbodyConstraints2D.FreezeAll;
            }
        }
        else
        {
            Debug.LogWarning("SpriteChangeOnCollision: scriptToDisable не указан!");
        }
    }
    
    // Метод для восстановления оригинального спрайта
    public void ResetSprite()
    {
        if (spriteRenderer != null && originalSprite != null)
        {
            spriteRenderer.sprite = originalSprite;
            hasChanged = false;
        }
    }
}
