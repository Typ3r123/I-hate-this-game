using UnityEngine;
using UnityEngine.SceneManagement;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance = null;
    public AudioSource musicSource;

    // Массив для музыки уровней (добавь 2 трека в инспекторе)
    public AudioClip[] levelMusicTracks;
    public AudioClip menuMusic;
    
    [SerializeField] private float distortionPitch = 1f; 
    [SerializeField] private bool enableDistortion = true; // Включить искажение
    
    private bool isOnLevel = false;
    private float originalPitch = 1f;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        if (musicSource == null)
            musicSource = GetComponent<AudioSource>();

        originalPitch = musicSource.pitch;

        // Подписываемся на событие загрузки сцены
        SceneManager.sceneLoaded += OnSceneLoaded;
        
        // Включаем музыку меню при старте
        PlayMenuMusic();
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // Автоматически меняем музыку в зависимости от сцены
        string sceneName = scene.name;
        
        if (sceneName == "Menu" || sceneName.Contains("Menu"))
        {
            isOnLevel = false;
            if (musicSource.clip != menuMusic || !musicSource.isPlaying)
            {
                PlayMenuMusic();
            }
        }
        else if (sceneName.Contains("Level_20") || sceneName == "Level_20")
        {
            // На уровне 20 музыка не играет
            isOnLevel = false;
            StopMusic();
        }
        else if (sceneName.Contains("Level_23") || sceneName == "Level_23")
        {
            // На уровне 23 специальный pitch 0.7
            isOnLevel = true;
            if (!musicSource.isPlaying)
            {
                PlayRandomLevelMusic();
            }
            else
            {
                musicSource.pitch = originalPitch * 0.7f;
            }
        }
        else if (sceneName.Contains("Level"))
        {
            isOnLevel = true;
            // Если музыка уже играет, не трогаем
            if (!musicSource.isPlaying)
            {
                PlayRandomLevelMusic();
            }
            else
            {
                // Если музыка уже играет, просто меняем pitch на обычный
                musicSource.pitch = originalPitch * distortionPitch;
            }
        }
    }

    private void Update()
    {
        // Если на уровне и музыка закончилась, запускаем следующий случайный трек
        if (isOnLevel && !musicSource.isPlaying)
        {
            PlayRandomLevelMusic();
        }
    }

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    public void PlayMenuMusic()
    {
        if (menuMusic != null)
        {
            musicSource.clip = menuMusic;
            musicSource.loop = true;
            musicSource.pitch = originalPitch;
            musicSource.Play();
        }
    }

    public void PlayRandomLevelMusic()
    {
        if (levelMusicTracks.Length == 0) return;
        
        int randomIndex = Random.Range(0, levelMusicTracks.Length);
        musicSource.clip = levelMusicTracks[randomIndex];
        musicSource.loop = false;
        
        // Проверяем текущий уровень
        string sceneName = SceneManager.GetActiveScene().name;
        
        if (sceneName.Contains("Level_23") || sceneName == "Level_23")
        {
            // На уровне 23 pitch 0.7
            musicSource.pitch = originalPitch * 0.7f;
        }
        else if (enableDistortion)
        {
            // На других уровнях используем distortionPitch
            musicSource.pitch = originalPitch * distortionPitch;
        }
        else
        {
            musicSource.pitch = originalPitch;
        }
        
        musicSource.Play();
    }

    public void StopMusic()
    {
        musicSource.Stop();
    }
}

