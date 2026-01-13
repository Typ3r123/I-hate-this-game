using UnityEngine;

public class HoldSpriteChange : MonoBehaviour
{
    [SerializeField] private Sprite normalSprite;  // Спрайт в обычном состоянии
    [SerializeField] private Sprite heldSprite;    // Спрайт при зажатии
    
    private SpriteRenderer spriteRenderer;
    private bool isHeld = false;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        
        // Устанавливаем начальный спрайт
        if (normalSprite != null && spriteRenderer != null)
        {
            spriteRenderer.sprite = normalSprite;
        }
    }

    void Update()
    {
        // Проверяем, находится ли мышь над объектом
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = 10f;
        Vector3 worldMousePos = Camera.main.ScreenToWorldPoint(mousePos);

        Collider2D collider = GetComponent<Collider2D>();
        bool mouseOverObject = collider != null && collider.bounds.Contains(worldMousePos);

        // Если мышь над объектом и зажата ЛКМ
        if (mouseOverObject && Input.GetMouseButton(0))
        {
            if (!isHeld)
            {
                isHeld = true;
                // Меняем спрайт на зажатое состояние
                if (heldSprite != null && spriteRenderer != null)
                {
                    spriteRenderer.sprite = heldSprite;
                }
            }
        }
        else
        {
            if (isHeld)
            {
                isHeld = false;
                // Возвращаем спрайт в обычное состояние
                if (normalSprite != null && spriteRenderer != null)
                {
                    spriteRenderer.sprite = normalSprite;
                }
            }
        }
    }
}
