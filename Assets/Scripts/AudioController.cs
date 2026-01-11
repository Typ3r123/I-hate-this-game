using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class AudioController : MonoBehaviour
{
    public Slider slider;
    public AudioClip clip;
    public AudioSource audio; //   

    private float reinitializeTimer = 0f;
    private const float REINITIALIZE_INTERVAL = 0.5f; // Проверяем каждые 0.5 секунды

    void Start()
    {
        // Инициализация компонентов
        InitializeComponents();
        
        // Подписываемся на событие загрузки сцены
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnEnable()
    {
        // При активации объекта также инициализируем компоненты
        InitializeComponents();
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // После загрузки сцены сбрасываем ссылки и ищем компоненты заново
        audio = null;
        slider = null;
        InitializeComponents();
    }

    private void InitializeComponents()
    {
        // Ищем AudioSource, если не назначен
        if (audio == null)
            audio = GetComponent<AudioSource>();

        if (audio == null)
            audio = GetComponentInChildren<AudioSource>();

        // Если всё ещё не найден, пытаемся найти через SoundManager
        if (audio == null && SoundManager.instance != null)
        {
            audio = SoundManager.instance.musicSource;
        }

        // Ищем Slider, если не назначен
        if (slider == null)
            slider = GetComponentInChildren<Slider>();

        // Если slider всё ещё не найден, ищем по всему сцене
        if (slider == null)
        {
            Slider[] sliders = FindObjectsOfType<Slider>();
            foreach (Slider s in sliders)
            {
                // Предполагаем, что слайдер для музыки имеет определённое имя или находится в определённом месте
                if (s.name.Contains("Music") || s.name.Contains("Volume") || s.name.Contains("Audio"))
                {
                    slider = s;
                    break;
                }
            }
        }

        // Устанавливаем начальное значение громкости
        if (audio != null && slider != null)
        {
            // Загружаем сохранённое значение громкости, если есть
            if (PlayerPrefs.HasKey("MusicVolume"))
            {
                float savedVolume = PlayerPrefs.GetFloat("MusicVolume");
                slider.value = savedVolume;
                audio.volume = savedVolume;
            }
            else
            {
                audio.volume = slider.value;
            }
        }
    }

    private void Update()
    {
        // Периодически проверяем и переинициализируем компоненты, если они потеряны
        reinitializeTimer += Time.deltaTime;
        if (reinitializeTimer >= REINITIALIZE_INTERVAL)
        {
            reinitializeTimer = 0f;
            
            // Если компоненты потеряны (после перезагрузки сцены), пытаемся их найти снова
            if (audio == null || slider == null)
            {
                InitializeComponents();
            }
        }

        if (audio != null && slider != null)
        {
            audio.volume = slider.value;
            // Сохраняем значение громкости
            PlayerPrefs.SetFloat("MusicVolume", slider.value);
        }
    }

    private void OnDestroy()
    {
        // Отписываемся от события при уничтожении объекта
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    public void PlaySound()
    {
        if (audio != null && clip != null)
        {
            audio.PlayOneShot(clip);
        }
    }
}
