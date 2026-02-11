using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadNextScene : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // Отвязываем игрока от платформы перед загрузкой новой сцены
            other.transform.parent = null;
            
            Unlocklevel();
            int current = SceneManager.GetActiveScene().buildIndex;
            int next = current + 1;
            
            SceneManager.LoadScene(next);
        }
    }

    public void Unlocklevel()
    {
        int currentlevel = SceneManager.GetActiveScene().buildIndex;

        if(currentlevel >= PlayerPrefs.GetInt("levels"))
        {
            PlayerPrefs.SetInt("levels", currentlevel + 1);
        }
    }
}
