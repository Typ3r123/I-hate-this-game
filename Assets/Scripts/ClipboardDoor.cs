using UnityEngine;
using TMPro;

public class ClipboardDoor : MonoBehaviour
{
    [Header("Дверь")]
    public Door door;
    
    [Header("Текст")]
    public TextMeshProUGUI statusText;
    
    [Header("Пароль")]
    private string correctPassword = "i hate this game";
    
    private bool doorOpened = false;
    private string lastClipboard = "";

    private void Update()
    {
        if (doorOpened) return;

        // Получаем текст из буфера обмена
        string clipboardText = GUIUtility.systemCopyBuffer;
        
        // Проверяем, изменился ли буфер обмена
        if (clipboardText != lastClipboard && !string.IsNullOrEmpty(clipboardText))
        {
            lastClipboard = clipboardText;
            
            // Проверяем, совпадает ли текст с паролем (без учёта регистра)
            if (clipboardText.ToLower().Trim() == correctPassword.ToLower())
            {
                // Правильный пароль
                door.Open();
                if (statusText != null)
                doorOpened = true;
            }
            else
            {

            }
        }
    }
}
