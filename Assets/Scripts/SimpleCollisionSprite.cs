using UnityEngine;

public class SimpleCollisionSprite : MonoBehaviour
{
    [SerializeField] private GameObject objectToActivate; // Объект который активировать на месте падения
    [SerializeField] private AudioClip collisionSound; // Звук при столкновении
    [SerializeField] private float soundVolume = 1f; // Громкость звука
    
    private bool hasChanged = false;
    
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (hasChanged) return;
        hasChanged = true;
        SpawnObjectAtPosition();
    }
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (hasChanged) return;
        hasChanged = true;
        SpawnObjectAtPosition();
    }
    
    private void SpawnObjectAtPosition()
    {
        // Воспроизводим звук
        if (collisionSound != null)
        {
            AudioSource.PlayClipAtPoint(collisionSound, transform.position, soundVolume);
        }
        
        // Активируем объект на месте этого объекта
        if (objectToActivate != null)
        {
            objectToActivate.transform.position = transform.position;
            objectToActivate.SetActive(true);
        }
        
        // Деактивируем этот объект
        gameObject.SetActive(false);
    }
}
