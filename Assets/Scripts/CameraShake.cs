using UnityEngine;

public class CameraShake : MonoBehaviour
{
    [SerializeField] private float shakeAmount = 0.2f; // Сила покачивания
    [SerializeField] private float shakeSpeed = 3f; // Скорость покачивания
    [SerializeField] private float circleAmount = 0.15f; // Сила круговых движений
    [SerializeField] private bool enableShake = true; // Включить эффект
    
    private Vector3 originalPosition;
    private float time = 0f;
    
    private void Start()
    {
        originalPosition = transform.position;
    }
    
    private void Update()
    {
        if (!enableShake)
        {
            transform.position = originalPosition;
            return;
        }
        
        time += Time.deltaTime;
        
        // Волнистое движение - камера движется в разные стороны
        float shakeX = Mathf.Sin(time * shakeSpeed) * shakeAmount;
        float shakeY = Mathf.Sin(time * shakeSpeed + Mathf.PI / 2) * shakeAmount;
        
        // Круговые движения
        float circleX = Mathf.Cos(time * shakeSpeed * 0.5f) * circleAmount;
        float circleY = Mathf.Sin(time * shakeSpeed * 0.5f) * circleAmount;
        
        // Комбинируем волну и круговые движения
        Vector3 totalShake = new Vector3(shakeX + circleX, shakeY + circleY, 0);
        
        // Применяем покачивание
        transform.position = originalPosition + totalShake;
    }
}


