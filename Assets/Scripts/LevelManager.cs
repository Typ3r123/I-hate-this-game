using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    int levelunlock;
    public Button[] buttons;
    void Start()
    {
        int levelUnlock = PlayerPrefs.GetInt("levels", 1);

        // Disable all buttons first
        for (int i = 0; i < buttons.Length; i++)
        {
            buttons[i].interactable = false;
        }

        // Enable only the allowed levels (within button count)
        for (int i = 0; i < levelUnlock && i < buttons.Length; i++)
        {
            buttons[i].interactable = true;
        }
    }

    public void loadLevel(int levelIndex)
    {
        // Меняем музыку на музыку уровня перед загрузкой сцены
        if (SoundManager.instance != null)
        {
            SoundManager.instance.PlayRandomLevelMusic();
        }
        SceneManager.LoadScene(levelIndex);
    }
}
