using UnityEngine;
using TMPro;

public class DoorTextSwitch : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI textDefault; // Текст по умолчанию (далеко от двери)
    [SerializeField] private TextMeshProUGUI textNear; // Текст когда рядом с дверью (до взаимодействия)
    [SerializeField] private TextMeshProUGUI textNear2; // Текст когда рядом с дверью (после взаимодействия)
    [SerializeField] private TextMeshProUGUI textInteracted; // Текст после взаимодействия (когда далеко)
    [SerializeField] private float interactionDistance = 1f; // Расстояние для взаимодействия
    [SerializeField] private Transform player; // Позиция игрока
    
    private bool isNearDoor = false;
    private bool hasInteracted = false;
    
    private void Start()
    {
        if (player == null)
        {
            // Ищем игрока по тегу
            GameObject playerObj = GameObject.FindWithTag("Player");
            if (playerObj != null)
                player = playerObj.transform;
        }
        
        // Изначально показываем только первый текст
        if (textDefault != null)
            textDefault.enabled = true;
        if (textNear != null)
            textNear.enabled = false;
        if (textNear2 != null)
            textNear2.enabled = false;
        if (textInteracted != null)
            textInteracted.enabled = false;
    }
    
    private void Update()
    {
        if (player == null)
            return;
        
        // Проверяем расстояние до двери
        float distance = Vector2.Distance(transform.position, player.position);
        isNearDoor = distance <= interactionDistance;
        
        // Если нажал E рядом с дверью
        if (isNearDoor && Input.GetKeyDown(KeyCode.E))
        {
            hasInteracted = !hasInteracted;
        }
        
        // Обновляем текст в зависимости от расстояния и состояния
        UpdateTextDisplay();
    }
    
    private void UpdateTextDisplay()
    {
        // Скрываем все тексты
        if (textDefault != null)
            textDefault.enabled = false;
        if (textNear != null)
            textNear.enabled = false;
        if (textNear2 != null)
            textNear2.enabled = false;
        if (textInteracted != null)
            textInteracted.enabled = false;
        
        // Показываем нужный текст
        if (isNearDoor)
        {
            // Рядом с дверью
            if (hasInteracted)
            {
                // После взаимодействия
                if (textNear2 != null)
                    textNear2.enabled = true;
            }
            else
            {
                // До взаимодействия
                if (textNear != null)
                    textNear.enabled = true;
            }
        }
        else
        {
            // Далеко от двери
            if (hasInteracted)
            {
                // После взаимодействия
                if (textInteracted != null)
                    textInteracted.enabled = true;
            }
            else
            {
                // До взаимодействия
                if (textDefault != null)
                    textDefault.enabled = true;
            }
        }
    }
}
