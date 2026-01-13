using UnityEngine;

public class PendulumMovement : MonoBehaviour
{
    public float speed = 2f;           // Скорость движения
    public float distance = 5f;        // Расстояние от центра
    
    private Vector3 centerPosition;
    private Vector3 targetPosition;
    private int direction = 1;         // 1 = вправо, -1 = влево
    private bool movingToTarget = true;

    void Start()
    {
        centerPosition = transform.position;
        targetPosition = centerPosition + new Vector3(direction * distance, 0, 0);
    }

    void Update()
    {
        if (movingToTarget)
        {
            // Движемся к целевой позиции
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);

            // Если достигли цели
            if (Vector3.Distance(transform.position, targetPosition) < 0.01f)
            {
                movingToTarget = false;
                targetPosition = centerPosition;
            }
        }
        else
        {
            // Возвращаемся в центр
            transform.position = Vector3.MoveTowards(transform.position, centerPosition, speed * Time.deltaTime);

            // Если вернулись в центр
            if (Vector3.Distance(transform.position, centerPosition) < 0.01f)
            {
                transform.position = centerPosition;
                movingToTarget = true;
                direction *= -1;  // Меняем направление
                targetPosition = centerPosition + new Vector3(direction * distance, 0, 0);
            }
        }
    }
}
