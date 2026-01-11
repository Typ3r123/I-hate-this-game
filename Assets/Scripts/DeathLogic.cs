using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathLogic : MonoBehaviour
{
    public void Death()
    {
        // Увеличиваем счётчик рестартов
        StatisticManager.AddDeath();

        // Перезагружаем текущую сцену
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
