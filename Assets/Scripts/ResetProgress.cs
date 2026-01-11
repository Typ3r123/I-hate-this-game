using UnityEngine;

public class ResetProgress : MonoBehaviour
{
    public void ResetAllProgress()
    {
        // Сбрасываем все сохранённые данные
        PlayerPrefs.DeleteAll();
        PlayerPrefs.Save();
        
        Debug.Log("Прогресс сброшен!");
    }

    public void ResetLevels()
    {
        // Сбрасываем только уровни (оставляем смерти и рестарты)
        PlayerPrefs.SetInt("levels", 1);
        PlayerPrefs.Save();
        
        Debug.Log("Уровни сброшены!");
    }
}
