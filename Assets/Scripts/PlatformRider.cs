using UnityEngine;

public class PlatformRider : MonoBehaviour
{
    public LayerMask platformLayer;  // Слой платформы
    public float checkDistance = 0.5f;  // Расстояние проверки вниз
    
    private Transform currentPlatform;
    private Rigidbody2D rb;
    private bool onPlatform = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        CheckPlatform();
    }

    void CheckPlatform()
    {
        // Проверяем, есть ли платформа под объектом
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, checkDistance, platformLayer);

        if (hit.collider != null)
        {
            // Если это новая платформа
            if (currentPlatform != hit.transform)
            {
                // Отвязываем от старой платформы
                if (currentPlatform != null)
                {
                    transform.parent = null;
                }

                // Привязываем к новой платформе
                currentPlatform = hit.transform;
                transform.parent = currentPlatform;
                onPlatform = true;
            }
        }
        else
        {
            // Если нет платформы под объектом — отвязываем
            if (currentPlatform != null)
            {
                transform.parent = null;
                currentPlatform = null;
                onPlatform = false;
            }
        }
    }
}
