using UnityEngine;

public class MousePlatformDrawer : MonoBehaviour
{
    [Header("Настройки платформ")]
    public GameObject platformPrefab; // Префаб платформы (если есть)
    public float platformSize = 0.5f; // Размер квадратика
    public float spawnInterval = 0.1f; // Интервал между созданием платформ (в секундах)
    public LayerMask platformLayer; // Слой для платформ (опционально)
    
    [Header("Материал платформы")]
    public Material platformMaterial; // Материал для платформ (опционально)
    public Color platformColor = Color.white; // Цвет платформы
    public bool allowOverlap = true; // Разрешать перекрытие квадратов
    
    private Camera mainCamera;
    private float lastSpawnTime = 0f;
    private bool isDrawing = false;
    
    void Start()
    {
        // Находим камеру
        mainCamera = Camera.main;
        if (mainCamera == null)
        {
            mainCamera = FindObjectOfType<Camera>();
        }
        
        if (mainCamera == null)
        {
            Debug.LogError("MousePlatformDrawer: Камера не найдена!");
        }
    }
    
    void Update()
    {
        // Проверяем зажатие ЛКМ
        if (Input.GetMouseButton(0))
        {
            isDrawing = true;
            
            // Проверяем интервал между созданием платформ
            if (Time.time - lastSpawnTime >= spawnInterval)
            {
                CreatePlatformAtMousePosition();
                lastSpawnTime = Time.time;
            }
        }
        else
        {
            isDrawing = false;
        }
    }
    
    void CreatePlatformAtMousePosition()
    {
        if (mainCamera == null) return;
        
        // Получаем позицию мыши в мировых координатах
        Vector3 mousePosition = Input.mousePosition;
        float distanceFromCamera = Mathf.Abs(mainCamera.transform.position.z);
        Vector3 worldPosition = mainCamera.ScreenToWorldPoint(new Vector3(mousePosition.x, mousePosition.y, distanceFromCamera));
        worldPosition.z = 0;
        
        // Если перекрытие запрещено — проверяем наличие существующей платформы в точке
        if (!allowOverlap)
        {
            Collider2D existingPlatform = Physics2D.OverlapBox(worldPosition, Vector2.one * platformSize * 0.9f, 0f, platformLayer);
            if (existingPlatform != null)
            {
                return; // Платформа уже существует, выходим
            }
        }
        
        // Создаем платформу
        GameObject platform;
        
        if (platformPrefab != null)
        {
            // Используем префаб, если он назначен
            platform = Instantiate(platformPrefab, worldPosition, Quaternion.identity);
        }
        else
        {
            // Создаем платформу программно
            platform = CreatePlatformObject(worldPosition);
        }
        
        // Устанавливаем родителя (опционально, для организации в иерархии)
        platform.transform.SetParent(transform);
    }
    
    GameObject CreatePlatformObject(Vector3 position)
    {
        // Создаем GameObject
        GameObject platform = new GameObject("Platform");
        platform.transform.position = position;
        
        // Добавляем SpriteRenderer для визуализации
        SpriteRenderer spriteRenderer = platform.AddComponent<SpriteRenderer>();
        
        // Создаем простой квадратный спрайт
        Texture2D texture = new Texture2D(1, 1);
        texture.SetPixel(0, 0, platformColor);
        texture.Apply();
        
        Sprite sprite = Sprite.Create(texture, new Rect(0, 0, 1, 1), new Vector2(0.5f, 0.5f), 100f);
        spriteRenderer.sprite = sprite;
        spriteRenderer.color = platformColor;
        
        // Устанавливаем размер
        platform.transform.localScale = new Vector3(platformSize, platformSize, 1f);
        
        // Добавляем материал, если он назначен
        if (platformMaterial != null)
        {
            spriteRenderer.material = platformMaterial;
        }
        
        // Добавляем BoxCollider2D для физики
        BoxCollider2D collider = platform.AddComponent<BoxCollider2D>();
        collider.size = Vector2.one;
        
        // Устанавливаем слой, если он задан
        if (platformLayer.value != 0)
        {
            // Находим номер слоя из LayerMask
            int layerNumber = 0;
            int layerMask = platformLayer.value;
            while (layerMask > 1)
            {
                layerMask = layerMask >> 1;
                layerNumber++;
            }
            platform.layer = layerNumber;
        }
        
        return platform;
    }
    
    // Метод для удаления всех созданных платформ (можно вызвать из другого скрипта)
    public void ClearAllPlatforms()
    {
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }
    }
}
