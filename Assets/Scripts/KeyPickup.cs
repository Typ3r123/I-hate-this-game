using UnityEngine;

public class KeyPickup : MonoBehaviour
{
    [SerializeField] private Door door; // Дверь которая откроется при подборе ключа
    [SerializeField] private AudioClip pickupSound; // Звук при подборе
    [SerializeField] private float soundVolume = 1f; // Громкость звука
    
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
            OnKeyPickup();
        }
    }

    private void OnKeyPickup()
    {
        alreadyPickedUp = true;

        // Воспроизводим звук
        if (pickupSound != null && audioSource != null)
        {
            audioSource.PlayOneShot(pickupSound, soundVolume);
        }

        Debug.Log("Ключ подобран!");

        // Открываем дверь
        if (door != null)
        {
            door.Open();
        }

        // Уничтожаем ключ
        Destroy(gameObject);
    }
}
