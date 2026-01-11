using UnityEngine;

public class HorizontalMovement : MonoBehaviour
{
    [SerializeField] private float speed = 5f;  // Скорость движения
    [SerializeField] private float distance = 10f;  // Расстояние в одну сторону
    [SerializeField] private bool startMovingRight = true;  // Галочка: true = вправо, false = влево
    
    private Vector3 startPosition;
    private bool movingRight;
    private float movedDistance = 0f;

    private void Start()
    {
        startPosition = transform.position;
        movingRight = startMovingRight;
    }

    private void Update()
    {
        if (movingRight)
        {
            // Движемся вправо
            transform.position += Vector3.right * speed * Time.deltaTime;
            movedDistance += speed * Time.deltaTime;
            
            if (movedDistance >= distance)
            {
                movingRight = false;
                movedDistance = 0f;
            }
        }
        else
        {
            // Движемся влево
            transform.position += Vector3.left * speed * Time.deltaTime;
            movedDistance += speed * Time.deltaTime;
            
            if (movedDistance >= distance)
            {
                movingRight = true;
                movedDistance = 0f;
            }
        }
    }
}
