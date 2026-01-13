using UnityEngine;

public class HeavyObject : MonoBehaviour
{
    public float mass = 10f;              // Масса объекта (чем больше, тем тяжелее)
    public float maxAngularVelocity = 2f; // Максимальная скорость вращения
    
    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        
        // Устанавливаем массу
        rb.mass = mass;
        
        // Увеличиваем линейное сопротивление (drag) для более медленного падения
        rb.drag = 0.5f;
    }

    void Update()
    {
        // Постоянно ограничиваем угловую скорость, чтобы объект не крутился сильно
        if (Mathf.Abs(rb.angularVelocity) > maxAngularVelocity)
        {
            rb.angularVelocity = Mathf.Clamp(rb.angularVelocity, -maxAngularVelocity, maxAngularVelocity);
        }
    }
}
