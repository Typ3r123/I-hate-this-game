using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static bool PauseGame;
    public GameObject pauseMenu;
    public Door door;
    public bool openDoorOnPause = true;
    public GameObject textClosed;
    public GameObject textOpened;
    public GameObject[] objectsToHideOnPause; // Объекты которые скрываются при паузе

    private bool doorAlreadyOpened = false; // чтобы не открывать дважды
    private Dictionary<GameObject, bool> objectStates = new Dictionary<GameObject, bool>(); // Сохраняем состояние объектов

    private void Start()
    {
        if (textClosed != null) textClosed.SetActive(true);
        if (textOpened != null) textOpened.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (PauseGame)
            {
                Resume();
            }
            else
            {
                ActivatePause();
            }
        }
    }

    // ←←← НОВЫЙ МЕТОД: всё, что нужно при активации паузы
    void ActivatePause()
    {
        // Открываем дверь (если ещё не открывали)
        if (openDoorOnPause && door != null && !doorAlreadyOpened)
        {
            door.Open();
            doorAlreadyOpened = true;
        }

        // Меняем тексты
        if (textClosed != null) textClosed.SetActive(false);
        if (textOpened != null) textOpened.SetActive(true);

        // Включаем паузу
        Pause();
    }

    public void Resume()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
        PauseGame = false;

        if (textClosed != null) textClosed.SetActive(false);
        if (textOpened != null) textOpened.SetActive(true);
        
        // Восстанавливаем состояние объектов какое было перед паузой
        foreach (GameObject obj in objectsToHideOnPause)
        {
            if (obj != null && objectStates.ContainsKey(obj))
            {
                obj.SetActive(objectStates[obj]);
            }
        }
    }

    public void Pause()
    {
        pauseMenu.SetActive(true);
        Time.timeScale = 0f;
        PauseGame = true;

        // Сохраняем состояние объектов перед скрытием
        objectStates.Clear();
        foreach (GameObject obj in objectsToHideOnPause)
        {
            if (obj != null)
            {
                objectStates[obj] = obj.activeSelf;
                obj.SetActive(false);
            }
        }

        // Скрываем тексты в меню паузы
        if (textClosed != null) textClosed.SetActive(false);
        if (textOpened != null) textOpened.SetActive(false);
    }

    // ←←← КНОПКА В UI ДОЛЖНА ВЫЗЫВАТЬ ЭТОТ МЕТОД!
    public void OnPauseButtonClicked()
    {
        if (!PauseGame)
        {
            ActivatePause();
        }
        else
        {
            Resume();
        }
    }

    public void LoadMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Menu");
    }
}