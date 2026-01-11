using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RestartTrigger : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        // Проверяем, что это игрок (персонаж с тегом Player)
        if (other.CompareTag("RestartTrigger"))
        {
            // Засчитываем смерть при падении
            StatisticManager.AddDeath();
            
            // Перезагружаем сцену
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}
