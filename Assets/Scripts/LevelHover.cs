using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public class LevelHover : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private int levelIndex;  // Индекс уровня (1, 2, 3...)
    [SerializeField] private string levelName;  // Название уровня
    [SerializeField] private Sprite levelImage;  // Картинка уровня
    [SerializeField] private Sprite lockedSprite;  // Спрайт замка
    
    [SerializeField] private TextMeshProUGUI nameText;  // Текст для названия
    [SerializeField] private Image imageDisplay;  // Image для отображения картинки
    
    private SpriteRenderer spriteRenderer;
    private Image buttonImage;
    private Button button;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        buttonImage = GetComponent<Image>();
        button = GetComponent<Button>();
        
        // Проверяем, разблокирован ли уровень
        int unlockedLevel = PlayerPrefs.GetInt("levels", 1);
        
        if (levelIndex > unlockedLevel)
        {
            // Уровень заблокирован
            if (spriteRenderer != null && lockedSprite != null)
                spriteRenderer.sprite = lockedSprite;
            
            if (buttonImage != null && lockedSprite != null)
                buttonImage.sprite = lockedSprite;
            
            // Отключаем кнопку БЕЗ затемнения
            if (button != null)
            {
                button.interactable = false;
                // Убираем затемнение
                ColorBlock colors = button.colors;
                colors.disabledColor = Color.white;
                button.colors = colors;
            }
            
            // Скрываем все текст элементы на кнопке
            TextMeshProUGUI[] allTexts = GetComponentsInChildren<TextMeshProUGUI>();
            foreach (TextMeshProUGUI text in allTexts)
            {
                text.gameObject.SetActive(false);
            }
        }
        else
        {
            // Уровень разблокирован
            if (spriteRenderer != null && levelImage != null)
                spriteRenderer.sprite = levelImage;
            
            if (buttonImage != null && levelImage != null)
                buttonImage.sprite = levelImage;
            
            // Включаем кнопку
            if (button != null)
                button.interactable = true;
            
            // Показываем текст для разблокированного уровня
            TextMeshProUGUI[] allTexts = GetComponentsInChildren<TextMeshProUGUI>();
            foreach (TextMeshProUGUI text in allTexts)
            {
                text.gameObject.SetActive(true);
            }
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        // Проверяем, разблокирован ли уровень
        int unlockedLevel = PlayerPrefs.GetInt("levels", 1);
        
        // Показываем информацию только для разблокированных уровней
        if (levelIndex <= unlockedLevel)
        {
            if (nameText != null)
                nameText.text = levelName;
            
            if (imageDisplay != null && levelImage != null)
                imageDisplay.sprite = levelImage;
            
            // Показываем панель с информацией
            if (imageDisplay != null)
                imageDisplay.gameObject.SetActive(true);
            if (nameText != null)
                nameText.gameObject.SetActive(true);
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        // При уходе курсора скрываем информацию
        if (imageDisplay != null)
            imageDisplay.gameObject.SetActive(false);
        if (nameText != null)
            nameText.gameObject.SetActive(false);
    }
}
