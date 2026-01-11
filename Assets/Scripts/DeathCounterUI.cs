using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class DeathCounterUI : MonoBehaviour
{
    public TMP_Text deathText;
    
    void Start()
    {
        UpdateDeathCount();
        
        // Подписываемся на событие загрузки сцены для обновления счетчика
        SceneManager.sceneLoaded += OnSceneLoaded;
    }
    
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // Обновляем счетчик после загрузки сцены
        UpdateDeathCount();
    }

    public void UpdateDeathCount()
    {
        if (deathText != null)
        {
            int count = StatisticManager.GetDeath();
            deathText.text = "Смерти: " + count;
        }
    }
    
    private void OnDestroy()
    {
        // Отписываемся от события при уничтожении объекта
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
}
