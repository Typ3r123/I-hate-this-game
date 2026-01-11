using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RestartLogic : MonoBehaviour
{
    public void RestartLevel()
    {
        // Увеличиваем счётчик рестартов
        StatisticManager.AddRestart();

        // Перезагружаем текущую сцену
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}