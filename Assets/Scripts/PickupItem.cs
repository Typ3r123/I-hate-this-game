using UnityEngine;

public class PickupItem : MonoBehaviour
{
    [SerializeField] private AudioClip pickupSound; // Звук при подборе
    [SerializeField] private float soundVolume = 1f; // Громкость звука
    [SerializeField] private bool destroyOnPickup = true; // Уничтожить объект при подборе
    
    private AudioSource audioSource;
    private bool alreadyPickedUp = false;

    void Start()
    {
        // Создаём AudioSource, если его нет
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null && pickupSound != null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
            audioSource.playOnAwake = false;
            audioSource.spatialBlend = 0f; // 2D звук
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !alreadyPickedUp)
        {
            OnPickup();
        }
    }

    private void OnPickup()
    {
        alreadyPickedUp = true;

        // Воспроизводим звук
        if (pickupSound != null && audioSource != null)
        {
            audioSource.PlayOneShot(pickupSound, soundVolume);
        }

        Debug.Log($"Подобран предмет: {gameObject.name}");

        // Уничтожаем объект или скрываем его
        if (destroyOnPickup)
        {
            Destroy(gameObject);
        }
        else
        {
            gameObject.SetActive(false);
        }
    }
}
