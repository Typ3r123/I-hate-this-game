using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class DisableSpaceOnButton : MonoBehaviour
{
    private Button button;

    private void Start()
    {
        button = GetComponent<Button>();
        if (button != null)
        {
            // Отключаем навигацию по пробелу
            Navigation nav = button.navigation;
            nav.mode = Navigation.Mode.None;
            button.navigation = nav;
        }
    }

    private void Update()
    {
        // Если пробел нажат и кнопка в фокусе, отменяем это
        if (Input.GetKeyDown(KeyCode.Space) && button != null && button.IsInteractable())
        {
            EventSystem.current.SetSelectedGameObject(null);
        }
    }
}
