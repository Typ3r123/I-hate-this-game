using UnityEngine;

public class VerticalMovement : MonoBehaviour
{
    [SerializeField] private float speed = 5f;  // Скорость движения
    [SerializeField] private float distance = 10f;  // Расстояние в одну сторону
    [SerializeField] private bool startMovingUp = true;  // Галочка: true = вверх, false = вниз
    
    private Vector3 startPosition;
    private bool movingUp;
    private float movedDistance = 0f;

    private void Start()
    {
        startPosition = transform.position;
        movingUp = startMovingUp;
    }

    private void Update()
    {
        if (movingUp)
        {
            // Движемся вверх
            transform.position += Vector3.up * speed * Time.deltaTime;
            movedDistance += speed * Time.deltaTime;
            
            if (movedDistance >= distance)
            {
                movingUp = false;
                movedDistance = 0f;
            }
        }
        else
        {
            // Движемся вниз
            transform.position += Vector3.down * speed * Time.deltaTime;
            movedDistance += speed * Time.deltaTime;
            
            if (movedDistance >= distance)
            {
                movingUp = true;
                movedDistance = 0f;
            }
        }
    }
}
