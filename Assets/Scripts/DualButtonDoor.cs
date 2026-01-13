using UnityEngine;

public class DualButtonDoor : MonoBehaviour
{
    [SerializeField] private ButtonSpriteChangeOnExit button1; // Первая кнопка
    [SerializeField] private ButtonSpriteChangeOnExit button2; // Вторая кнопка
    [SerializeField] private GameObject holdObject; // Объект, который нужно зажимать
    [SerializeField] private Door door; // Дверь
    [SerializeField] private float checkDelay = 0.1f; // Задержка проверки состояния кнопок
    
    private float checkTimer = 0f;
    private bool doorIsOpen = false;
    private bool isHoldingObject = false;

    void Update()
    {
        checkTimer += Time.deltaTime;

        if (checkTimer >= checkDelay)
        {
            checkTimer = 0f;
            CheckButtons();
        }

        // Проверяем, зажимает ли игрок объект
        CheckHoldObject();
    }

    private void CheckButtons()
    {
        // Проверяем, нажаты ли обе кнопки
        bool button1Pressed = IsButtonPressed(button1);
        bool button2Pressed = IsButtonPressed(button2);

        // Дверь открывается ТОЛЬКО если обе кнопки нажаты И зажимается объект одновременно
        if (button1Pressed && button2Pressed && isHoldingObject)
        {
            if (!doorIsOpen && door != null)
            {
                door.Open();
                doorIsOpen = true;
            }
        }
        // Дверь больше не закрывается, остаётся открытой
    }

    private void CheckHoldObject()
    {
        if (holdObject == null)
            return;

        // Проверяем, находится ли мышь над объектом
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = 10f;
        Vector3 worldMousePos = Camera.main.ScreenToWorldPoint(mousePos);

        Collider2D collider = holdObject.GetComponent<Collider2D>();
        if (collider != null && collider.bounds.Contains(worldMousePos))
        {
            // Мышь над объектом и зажата ЛКМ
            if (Input.GetMouseButton(0))
            {
                isHoldingObject = true;
            }
            else
            {
                isHoldingObject = false;
            }
        }
        else
        {
            isHoldingObject = false;
        }
    }

    private bool IsButtonPressed(ButtonSpriteChangeOnExit button)
    {
        if (button == null)
            return false;

        // Проверяем, есть ли объект на кнопке через Collider2D
        Collider2D collider = button.GetComponent<Collider2D>();
        if (collider == null)
            return false;

        // Проверяем, есть ли коллизии с объектами на кнопке
        Collider2D[] colliders = Physics2D.OverlapAreaAll(
            collider.bounds.min,
            collider.bounds.max
        );

        return colliders.Length > 1; // Больше 1, потому что сама кнопка тоже считается
    }
}
