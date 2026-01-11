using UnityEngine;

public class AudioDistortion : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource; // Источник звука
    [SerializeField] private float slowdownAmount = 0.7f; // На сколько замедлить (0.7 = 70% от нормальной скорости)
    [SerializeField] private bool enableDistortion = true; // Включить эффект
    
    private float originalPitch;
    
    private void Start()
    {
        if (audioSource == null)
        {
            audioSource = GetComponent<AudioSource>();
        }
        
        if (audioSource == null)
        {
            // Ищем AudioSource в сцене если не найден на объекте
            audioSource = FindObjectOfType<AudioSource>();
        }
        
        if (audioSource != null)
        {
            originalPitch = audioSource.pitch;
            
            if (enableDistortion)
            {
                // Просто меняем pitch уже играющей музыки
                audioSource.pitch = originalPitch * slowdownAmount;
            }
        }
        else
        {
            Debug.LogWarning("AudioDistortion: AudioSource не найден!");
        }
    }
    
    private void OnDisable()
    {
        if (audioSource != null)
        {
            audioSource.pitch = originalPitch;
        }
    }
}



