using UnityEngine;

public class BlinkObject : MonoBehaviour
{
    [SerializeField] private float visibleDuration = 2f; // Время на которое объект видим
    [SerializeField] private float invisibleDuration = 1f; // Время на которое объект невидим
    
    private float timer = 0f;
    private bool isVisible = true;
    private CanvasGroup canvasGroup;
    private SpriteRenderer spriteRenderer;
    private BoxCollider2D boxCollider;
    
    private void Start()
    {
        // Пытаемся найти CanvasGroup (для UI элементов)
        canvasGroup = GetComponent<CanvasGroup>();
        
        // Пытаемся найти SpriteRenderer (для спрайтов)
        spriteRenderer = GetComponent<SpriteRenderer>();
        
        // Пытаемся найти BoxCollider2D
        boxCollider = GetComponent<BoxCollider2D>();
        
        // Если ничего не найдено, отключаем gameObject
        if (canvasGroup == null && spriteRenderer == null)
        {
            Debug.LogWarning("BlinkObject: не найден ни CanvasGroup ни SpriteRenderer!");
        }
        
        timer = 0f;
        isVisible = true;
        SetVisibility(true);
    }
    
    private void Update()
    {
        timer += Time.deltaTime;
        
        if (isVisible)
        {
            // Объект видим, ждем visibleDuration
            if (timer >= visibleDuration)
            {
                isVisible = false;
                timer = 0f;
                SetVisibility(false);
            }
        }
        else
        {
            // Объект невидим, ждем invisibleDuration
            if (timer >= invisibleDuration)
            {
                isVisible = true;
                timer = 0f;
                SetVisibility(true);
            }
        }
    }
    
    private void SetVisibility(bool visible)
    {
        if (canvasGroup != null)
        {
            canvasGroup.alpha = visible ? 1f : 0f;
        }
        
        if (spriteRenderer != null)
        {
            spriteRenderer.enabled = visible;
        }
        
        if (boxCollider != null)
        {
            boxCollider.enabled = visible;
        }
    }
}
