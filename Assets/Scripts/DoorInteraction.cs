using UnityEngine;

public class DoorInteraction : MonoBehaviour
{
    [SerializeField] private GameObject[] objectsToHide; // Объекты которые пропадут
    [SerializeField] private GameObject[] objectsToShow; // Объекты которые появятся
    [SerializeField] private float interactionDistance = 1f; // Расстояние для взаимодействия
    [SerializeField] private Transform player; // Позиция игрока
    [SerializeField] private AudioClip interactionSound; // Звук при взаимодействии
    [SerializeField] private float soundVolume = 1f; // Громкость звука (0-1)
    
    private bool isNearDoor = false;
    private bool[] objectsHidden; // Отслеживаем состояние каждого объекта
    private bool[] objectsShown; // Отслеживаем состояние объектов которые появляются
    private AudioSource audioSource;
    
    private void Start()
    {
        if (player == null)
        {
            // Ищем игрока по тегу
            GameObject playerObj = GameObject.FindWithTag("Player");
            if (playerObj != null)
                player = playerObj.transform;
        }
        
        // Создаем AudioSource если его нет
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
        
        // Инициализируем массив состояний для скрываемых объектов
        objectsHidden = new bool[objectsToHide.Length];
        for (int i = 0; i < objectsHidden.Length; i++)
        {
            objectsHidden[i] = false; // Все видимы в начале
        }
        
        // Инициализируем массив состояний для появляемых объектов
        objectsShown = new bool[objectsToShow.Length];
        for (int i = 0; i < objectsShown.Length; i++)
        {
            objectsShown[i] = false; // Все неактивны в начале
            if (objectsToShow[i] != null)
            {
                objectsToShow[i].SetActive(false); // Убедимся что они неактивны
            }
        }
    }
    
    private void Update()
    {
        if (player == null)
            return;
        
        // Проверяем расстояние до двери
        float distance = Vector2.Distance(transform.position, player.position);
        isNearDoor = distance <= interactionDistance;
        
        // Если рядом с дверью и нажал E
        if (isNearDoor && Input.GetKeyDown(KeyCode.E))
        {
            ToggleObjects();
            PlayInteractionSound();
        }
    }
    
    private void ToggleObjects()
    {
        // Переключаем видимость объектов которые пропадают
        for (int i = 0; i < objectsToHide.Length; i++)
        {
            if (objectsToHide[i] != null)
            {
                objectsHidden[i] = !objectsHidden[i]; // Переключаем состояние
                
                // Отключаем/включаем BoxCollider2D
                BoxCollider2D boxCollider = objectsToHide[i].GetComponent<BoxCollider2D>();
                if (boxCollider != null)
                    boxCollider.enabled = !objectsHidden[i];
                
                // Отключаем/включаем SpriteRenderer
                SpriteRenderer spriteRenderer = objectsToHide[i].GetComponent<SpriteRenderer>();
                if (spriteRenderer != null)
                    spriteRenderer.enabled = !objectsHidden[i];
            }
        }
        
        // Переключаем видимость объектов которые появляются
        for (int i = 0; i < objectsToShow.Length; i++)
        {
            if (objectsToShow[i] != null)
            {
                objectsShown[i] = !objectsShown[i]; // Переключаем состояние
                objectsToShow[i].SetActive(objectsShown[i]); // Включаем/отключаем GameObject
            }
        }
    }
    
    private void PlayInteractionSound()
    {
        if (audioSource != null && interactionSound != null)
        {
            audioSource.PlayOneShot(interactionSound, soundVolume);
        }
    }
}
