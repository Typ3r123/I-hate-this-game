using UnityEngine;
using TMPro;

public class DeathCounterDoor : MonoBehaviour
{
    [Header("Дверь")]
    public Door door;
    
    [Header("Текст")]
    public TextMeshProUGUI questionText;  // Текст вопроса
    public TextMeshProUGUI inputText;     // Текст для отображения ввода
    
    private string userInput = "";
    private bool doorOpened = false;
    private int correctDeaths;

    private void Start()
    {
        // Получаем количество смертей из StatisticManager
        correctDeaths = StatisticManager.GetDeath();
        
        if (questionText != null)
            questionText.text = "Сколько раз ты умирал?";
        
        if (inputText != null)
            inputText.text = "";
    }

    private void Update()
    {
        if (doorOpened) return;

        // Обработка ввода с клавиатуры
        foreach (char c in Input.inputString)
        {
            if (char.IsDigit(c))
            {
                userInput += c;
                
                // Проверяем, совпадает ли введённое число с правильным ответом
                if (int.TryParse(userInput, out int userAnswer))
                {
                    if (userAnswer == correctDeaths)
                    {
                        // Правильный ответ
                        door.Open();
                        if (questionText != null)
                            questionText.text = "Молодец!";
                        doorOpened = true;
                    }
                    else if (userAnswer > correctDeaths)
                    {
                        // Число слишком большое, сбрасываем
                        userInput = "";
                    }
                }
            }
            else if (c == '\b')  // Backspace
            {
                if (userInput.Length > 0)
                {
                    userInput = userInput.Substring(0, userInput.Length - 1);
                }
            }
        }
    }
}

