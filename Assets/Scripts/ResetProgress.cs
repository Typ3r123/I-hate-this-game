using UnityEngine;
using UnityEngine.SceneManagement;

public class ResetProgress : MonoBehaviour
{
    public void ResetAllProgress()
    {
        // Сбрасываем все сохранённые данные (уровни, смерти, перезапуски)
        PlayerPrefs.DeleteAll();
        PlayerPrefs.Save();
        
        Debug.Log("Весь прогресс сброшен!");
        
        // Перезагружаем текущую сцену
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void ResetLevels()
    {
        // Сбрасываем только уровни (оставляем смерти и рестарты)
        PlayerPrefs.SetInt("levels", 1);
        PlayerPrefs.Save();
        
        Debug.Log("Уровни сброшены!");
    }

    public void ResetDeaths()
    {
        // Сбрасываем только смерти
        PlayerPrefs.SetInt("Смерти: ", 0);
        PlayerPrefs.Save();
        
        Debug.Log("Счётчик смертей сброшен!");
    }

    public void ResetRestarts()
    {
        // Сбрасываем только перезапуски
        PlayerPrefs.SetInt("Перезапуски: ", 0);
        PlayerPrefs.Save();
        
        Debug.Log("Счётчик перезапусков сброшен!");
    }
}
