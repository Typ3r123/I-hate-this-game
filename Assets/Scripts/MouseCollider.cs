using UnityEngine;

public class MouseCollider : MonoBehaviour
{
    private Camera mainCamera;
    private BoxCollider2D boxCollider;
    
    void Start()
    {
        // Находим камеру
        mainCamera = Camera.main;
        if (mainCamera == null)
        {
            mainCamera = FindObjectOfType<Camera>();
        }
        
        // Получаем или добавляем BoxCollider2D
        boxCollider = GetComponent<BoxCollider2D>();
        if (boxCollider == null)
        {
            boxCollider = gameObject.AddComponent<BoxCollider2D>();
        }
        
        // Если камера не найдена, выводим предупреждение
        if (mainCamera == null)
        {
            Debug.LogWarning("MouseCollider: Камера не найдена! Убедитесь, что в сцене есть камера с тегом MainCamera.");
        }
    }
    
    void Update()
    {
        if (mainCamera != null)
        {
            // Получаем позицию мыши в экранных координатах
            Vector3 mousePosition = Input.mousePosition;
            
            // Для ортографической камеры (2D) используем правильную конвертацию
            // Z должен быть расстоянием от камеры до плоскости игры
            float distanceFromCamera = Mathf.Abs(mainCamera.transform.position.z);
            Vector3 worldPosition = mainCamera.ScreenToWorldPoint(new Vector3(mousePosition.x, mousePosition.y, distanceFromCamera));
            worldPosition.z = 0; // Для 2D игры устанавливаем Z = 0
            
            // Обновляем позицию объекта (и коллайдера)
            transform.position = worldPosition;
        }
    }
}
