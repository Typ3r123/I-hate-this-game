using UnityEngine;
using System.Collections;

public class ButtonSpriteChangeOnExit : MonoBehaviour
{
    [SerializeField] private Sprite[] sprites; // Массив спрайтов [0] = обычное состояние, [1] = нажатое состояние
    [SerializeField] private AudioClip pressSound; // Звук при нажатии
    [SerializeField] private AudioClip releaseSound; // Звук при отпускании
    [SerializeField] private float soundVolume = 1f; // Громкость звука
    [SerializeField] private Door door; // Дверь которая откроется при нажатии
    
    private SpriteRenderer spriteRenderer;
    private AudioSource audioSource;
    private bool isPressed = false;
    private bool objectOnButton = false;
    private bool soundReleasePlayedAlready = false;
    
    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        audioSource = GetComponent<AudioSource>();
        
        // Если AudioSource не найден — создаём его
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
        
        audioSource.volume = soundVolume;
        
        // Устанавливаем начальный спрайт (индекс 0)
        if (sprites.Length > 0 && spriteRenderer != null)
        {
            spriteRenderer.sprite = sprites[0];
        }
    }
    
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!isPressed)
        {
            PressButton();
        }
        objectOnButton = true;
    }
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!isPressed)
        {
            PressButton();
        }
        objectOnButton = true;
    }
    
    private void OnCollisionExit2D(Collision2D collision)
    {
        objectOnButton = false;
        ReleaseButton();
    }
    
    private void OnTriggerExit2D(Collider2D collision)
    {
        objectOnButton = false;
        ReleaseButton();
    }
    
    private void PressButton()
    {
        isPressed = true;
        soundReleasePlayedAlready = false;  // Сбрасываем флаг звука релиза
        
        // Воспроизводим звук
        if (pressSound != null && audioSource != null)
        {
            audioSource.PlayOneShot(pressSound, soundVolume);
        }
        
        // Открываем дверь
        if (door != null)
        {
            door.Open();
        }
        
        // Меняем спрайт на нажатое состояние (индекс 1)
        if (sprites.Length > 1 && spriteRenderer != null)
        {
            spriteRenderer.sprite = sprites[1];
        }
    }
    
    private void ReleaseButton()
    {
        if (isPressed && !objectOnButton && !soundReleasePlayedAlready)
        {
            isPressed = false;
            soundReleasePlayedAlready = true;  // Отмечаем, что звук уже воспроизведён
            
            // Возвращаем спрайт в исходное состояние (индекс 0)
            bool spriteChanged = false;
            if (sprites.Length > 0 && spriteRenderer != null)
            {
                spriteRenderer.sprite = sprites[0];
                spriteChanged = true;
            }
            
            // Воспроизводим звук отпускания ТОЛЬКО если спрайт изменился
            if (spriteChanged && releaseSound != null && audioSource != null)
            {
                audioSource.PlayOneShot(releaseSound, soundVolume);
            }
        }
    }
}
