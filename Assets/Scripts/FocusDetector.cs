using UnityEngine;

public class FocusDetector : MonoBehaviour
{
    [SerializeField] private Door door; // Дверь которая откроется
    
    private bool wasFocused = true;
    
    private void OnApplicationFocus(bool hasFocus)
    {
        // Если приложение потеряло фокус
        if (!hasFocus)
        {
            wasFocused = false;
        }
        // Если приложение получило фокус обратно
        else if (!wasFocused)
        {
            wasFocused = true;
            
            // Открываем дверь
            if (door != null)
            {
                door.Open();
            }
        }
    }
}
