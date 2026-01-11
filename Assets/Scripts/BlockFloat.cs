using UnityEngine;

public class BlockFloat : MonoBehaviour
{
    public float speed = 2f;          // скорость движения вверх
    public float force = 10f;         // сила притяжения к цели
    public Transform target;          // объект-цель

    [Header("Проседание под игроком")]
    public float sinkAmount = 0.1f;   // на сколько опускается, когда игрок стоит

    private Rigidbody2D rb;
    private bool playerOnBlock = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        if (rb == null)
        {
            Debug.LogError("Нет Rigidbody2D!");
            enabled = false;
            return;
        }
        rb.gravityScale = 0f;
    }

    void FixedUpdate()
    {
        if (target == null) return;

        Vector2 direction = (Vector2)target.position - rb.position;

        // Основное движение
        Vector2 baseVelocity = direction * force + Vector2.up * speed;

        // Если игрок стоит — немного опускаем блок
        if (playerOnBlock)
        {
            baseVelocity += Vector2.down * sinkAmount;
        }

        rb.velocity = baseVelocity;
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            playerOnBlock = true;
        }
    }

    void OnCollisionExit2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            playerOnBlock = false;
        }
    }
}
