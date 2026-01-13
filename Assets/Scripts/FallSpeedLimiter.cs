using UnityEngine;

public class FallSpeedLimiter : MonoBehaviour
{
    public float maxFallSpeed = 5f;  // Максимальная скорость падения
    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // Если объект падает слишком быстро — ограничиваем скорость
        if (rb.velocity.y < -maxFallSpeed)
        {
            rb.velocity = new Vector2(rb.velocity.x, -maxFallSpeed);
        }
    }
}
