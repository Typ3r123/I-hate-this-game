using UnityEngine;

public class HorizontalDrag : MonoBehaviour
{
    [SerializeField] private float maxDragDistance = 5f; // Максимальное расстояние перетаскивания
    [SerializeField] private bool useWorldSpace = true; // Использовать мировые координаты или локальные
    
    private Vector3 initialPosition; // Начальная позиция при старте
    private Vector3 currentStartPosition; // Позиция когда начинаем перетаскивание
    private Vector3 startMousePosition;
    private bool isDragging = false;
    
    private void Start()
    {
        initialPosition = transform.position;
        currentStartPosition = transform.position;
    }
    
    private void Update()
    {
        // Нажатие ЛКМ
        if (Input.GetMouseButtonDown(0))
        {
            if (IsMouseOverObject())
            {
                isDragging = true;
                startMousePosition = Input.mousePosition;
                currentStartPosition = transform.position; // Сохраняем текущую позицию
            }
        }
        
        // Отпускание ЛКМ
        if (Input.GetMouseButtonUp(0))
        {
            isDragging = false;
        }
        
        // Перетаскивание
        if (isDragging)
        {
            DragObject();
        }
    }
    
    private bool IsMouseOverObject()
    {
        // Проверяем есть ли Collider на объекте
        Collider2D collider = GetComponent<Collider2D>();
        if (collider == null)
            return false;
        
        // Получаем позицию мыши в мировых координатах
        Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        
        // Проверяем попадает ли мышь в коллайдер
        return collider.OverlapPoint(mouseWorldPos);
    }
    
    private void DragObject()
    {
        // Получаем текущую позицию мыши
        Vector3 currentMousePosition = Input.mousePosition;
        
        // Вычисляем смещение мыши по X
        float mouseDeltaX = currentMousePosition.x - startMousePosition.x;
        
        // Конвертируем пиксели в мировые координаты
        float worldDeltaX = mouseDeltaX / Screen.width * Camera.main.orthographicSize * 2f;
        
        // Вычисляем новую позицию от текущей стартовой позиции
        Vector3 newPosition = currentStartPosition + new Vector3(worldDeltaX, 0, 0);
        
        // Ограничиваем движение на maxDragDistance от начальной позиции
        float maxX = initialPosition.x + maxDragDistance;
        float minX = initialPosition.x - maxDragDistance;
        
        newPosition.x = Mathf.Clamp(newPosition.x, minX, maxX);
        
        // Применяем новую позицию
        transform.position = newPosition;
    }
}
