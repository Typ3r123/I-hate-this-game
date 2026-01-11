using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

public class ArrowSequenceGame : MonoBehaviour
{
    [Header("Стрелки")]
    [SerializeField] private Image[] displayArrows;  // Картинки, которые показывают последовательность (рандомно повернуты)
    [SerializeField] private Button[] controlButtons;  // Кнопки для ввода (всегда смотрят вверх)
    
    [Header("Дверь")]
    [SerializeField] private Door door;
    
    [Header("Текст")]
    [SerializeField] private TextMeshProUGUI statusText;
    
    private List<int> correctSequence = new List<int>();
    private List<int> playerSequence = new List<int>();
    private int sequenceLength = 6;

    private void Start()
    {
        GenerateSequence();
        SetupDisplayArrows();
        SetupControlButtons();
        
        if (statusText != null)
            statusText.text = "Повтори последовательность!";
    }

    private void GenerateSequence()
    {
        correctSequence.Clear();
        for (int i = 0; i < sequenceLength; i++)
        {
            int randomValue = Random.Range(1, 3);  // 1 или 2
            correctSequence.Add(randomValue);
        }
        
        Debug.Log("Правильная последовательность: " + string.Join(", ", correctSequence));
    }

    private void SetupDisplayArrows()
    {
        // Показываем рандомные стрелки согласно последовательности
        for (int i = 0; i < displayArrows.Length && i < correctSequence.Count; i++)
        {
            if (correctSequence[i] == 1)
            {
                // Стрелка смотрит вверх
                displayArrows[i].transform.rotation = Quaternion.Euler(0, 0, 0);
            }
            else
            {
                // Стрелка смотрит вниз (повернута на 180)
                displayArrows[i].transform.rotation = Quaternion.Euler(0, 0, 180);
            }
        }
    }

    private void SetupControlButtons()
    {
        // Все кнопки смотрят вверх (значение 1)
        for (int i = 0; i < controlButtons.Length; i++)
        {
            controlButtons[i].transform.rotation = Quaternion.Euler(0, 0, 0);
            
            int index = i;  // Для замыкания в лямбде
            controlButtons[i].onClick.AddListener(() => OnButtonPressed(index));
        }
    }

    private void OnButtonPressed(int buttonIndex)
    {
        // Проверяем что это не пробел
        if (Input.GetKeyDown(KeyCode.Space))
            return;
        
        // Переворачиваем кнопку
        if (controlButtons[buttonIndex].transform.rotation.z == 0)
        {
            // Была вверх (1), делаем вниз (2)
            controlButtons[buttonIndex].transform.rotation = Quaternion.Euler(0, 0, 180);
            playerSequence.Add(2);
        }
        else
        {
            // Была вниз (2), делаем вверх (1)
            controlButtons[buttonIndex].transform.rotation = Quaternion.Euler(0, 0, 0);
            playerSequence.Add(1);
        }
        
        Debug.Log("Игрок нажал кнопку " + buttonIndex + ". Последовательность: " + string.Join(", ", playerSequence));
        
        // Проверяем, правильно ли нажата кнопка
        if (playerSequence[playerSequence.Count - 1] != correctSequence[playerSequence.Count - 1])
        {
            Debug.Log("ОШИБКА!");
            if (statusText != null)
                statusText.text = "Ошибка! Попробуй ещё.";
            
            ResetGame();
            return;
        }
        
        Debug.Log("Правильно! " + playerSequence.Count + "/" + sequenceLength);
        
        // Проверяем, завершена ли последовательность
        if (playerSequence.Count == sequenceLength)
        {
            Debug.Log("ПОБЕДА! Дверь открывается!");
            
            if (door != null)
                door.Open();
            
            if (statusText != null)
                statusText.text = "Молодец!";
            
            enabled = false;
        }
    }

    private void ResetGame()
    {
        playerSequence.Clear();
        
        // Возвращаем все кнопки в исходное положение (вверх)
        for (int i = 0; i < controlButtons.Length; i++)
        {
            controlButtons[i].transform.rotation = Quaternion.Euler(0, 0, 0);
        }
    }
}
