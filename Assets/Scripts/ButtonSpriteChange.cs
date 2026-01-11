using UnityEngine;
using System.Collections;

public class ButtonSpriteChange : MonoBehaviour
{
    [SerializeField] private Sprite[] sprites; // Массив спрайтов [0] = обычное состояние, [1] = нажатое состояние
    [SerializeField] private float resetDelay = 2f; // Время до возврата в исходное состояние
    [SerializeField] private AudioClip pressSound; // Звук при нажатии
    [SerializeField] private AudioClip releaseSound; // Звук при отпускании
    [SerializeField] private float soundVolume = 1f; // Громкость звука
    [SerializeField] private Door door; // Дверь которая откроется при нажатии
    
    private SpriteRenderer spriteRenderer;
    private bool isPressed = false;
    
    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        
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
            ChangeSprite();
        }
    }
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!isPressed)
        {
            ChangeSprite();
        }
    }
    
    private void ChangeSprite()
    {
        isPressed = true;
        
        // Воспроизводим звук
        if (pressSound != null)
        {
            AudioSource.PlayClipAtPoint(pressSound, transform.position, soundVolume);
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
        
        // Запускаем корутину для возврата в исходное состояние
        StartCoroutine(ResetSpriteAfterDelay());
    }
    
    private IEnumerator ResetSpriteAfterDelay()
    {
        yield return new WaitForSeconds(resetDelay);
        
        isPressed = false;
        
        // Воспроизводим звук отпускания
        if (releaseSound != null)
        {
            AudioSource.PlayClipAtPoint(releaseSound, transform.position, soundVolume);
        }
        
        // Возвращаем спрайт в исходное состояние (индекс 0)
        if (sprites.Length > 0 && spriteRenderer != null)
        {
            spriteRenderer.sprite = sprites[0];
        }
    }
}
