using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class RestartCounterUI : MonoBehaviour
{
    public TMP_Text restartsText;
    
    void Start()
    {
        UpdateRestartsCount();
        
        // Подписываемся на событие загрузки сцены для обновления счетчика
        SceneManager.sceneLoaded += OnSceneLoaded;
    }
    
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // Обновляем счетчик после загрузки сцены
        UpdateRestartsCount();
    }

    public void UpdateRestartsCount()
    {
        if (restartsText != null)
        {
            int count = StatisticManager.GetRestarts();
            restartsText.text = "Рестарты: " + count;
        }
    }
    
    private void OnDestroy()
    {
        // Отписываемся от события при уничтожении объекта
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
}
