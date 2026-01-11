using UnityEngine;
using UnityEngine.UI;

public class ArrowDisplayGame : MonoBehaviour
{
    [Header("Отображение последовательности")]
    [SerializeField] private Image[] displayImages; // ← только они рандомные

    [Header("Кнопки игрока")]
    [SerializeField] private Button[] controlButtons; // ← всегда начинают с "вверх"

    [Header("Дверь")]
    [SerializeField] private Door door;

    private int[] correctSequence;

    private void Start()
    {
        if (displayImages.Length != controlButtons.Length)
        {
            Debug.LogError("Количество displayImages должно совпадать с controlButtons!");
            enabled = false;
            return;
        }

        // 1. Сначала сбрасываем кнопки в исходное состояние
        ResetPlayerButtons();

        // 2. Потом генерируем и показываем последовательность ТОЛЬКО на displayImages
        GenerateAndShowSequence();

        // 3. Настраиваем обработчики кликов
        SetupButtonListeners();
    }

    private void GenerateAndShowSequence()
    {
        int count = displayImages.Length;
        correctSequence = new int[count];

        Debug.Log("=== ГЕНЕРАЦИЯ НОВОЙ ПОСЛЕДОВАТЕЛЬНОСТИ ===");

        for (int i = 0; i < count; i++)
        {
            correctSequence[i] = (Random.value > 0.5f) ? 2 : 1;

            // Применяем ТОЛЬКО к displayImages
            if (correctSequence[i] == 1)
            {
                displayImages[i].transform.rotation = Quaternion.identity;
                Debug.Log($"Image[{i}] = ВВЕРХ (1)");
            }
            else
            {
                displayImages[i].transform.rotation = Quaternion.Euler(0, 0, 180);
                Debug.Log($"Image[{i}] = ВНИЗ (2)");
            }
        }

        // Проверим, что кнопки НЕ тронуты
        for (int i = 0; i < controlButtons.Length; i++)
        {
            float z = controlButtons[i].transform.rotation.eulerAngles.z;
            Debug.Log($"Button[{i}] rotation.z = {z}"); // должно быть 0!
        }
    }

    private void ResetPlayerButtons()
    {
        // Убедимся, что ВСЕ кнопки начинают строго с "вверх"
        for (int i = 0; i < controlButtons.Length; i++)
        {
            if (controlButtons[i] != null)
            {
                controlButtons[i].transform.rotation = Quaternion.identity;

                // Отключаем физику при старте
                Rigidbody2D rb = controlButtons[i].GetComponent<Rigidbody2D>();
                if (rb != null)
                {
                    rb.simulated = false;
                }
            }
        }
    }

    private void SetupButtonListeners()
    {
        for (int i = 0; i < controlButtons.Length; i++)
        {
            int index = i;
            controlButtons[i].onClick.AddListener(() => OnButtonClicked(index));
        }
    }

    public void OnButtonClicked(int buttonIndex)
    {
        if (buttonIndex < 0 || buttonIndex >= controlButtons.Length) return;

        Button btn = controlButtons[buttonIndex];
        if (btn == null) return;

        // Переключаем между вверх (1) и вниз (2)
        bool isCurrentlyUp = (btn.transform.rotation.eulerAngles.z == 0);

        if (isCurrentlyUp)
        {
            btn.transform.rotation = Quaternion.Euler(0, 0, 180); // вниз
        }
        else
        {
            btn.transform.rotation = Quaternion.identity; // вверх
        }

        CheckFullSequence();
    }

    private void CheckFullSequence()
    {
        bool allCorrect = true;

        for (int i = 0; i < controlButtons.Length; i++)
        {
            bool isUp = (controlButtons[i].transform.rotation.eulerAngles.z == 0);
            int playerValue = isUp ? 1 : 2;

            if (playerValue != correctSequence[i])
            {
                allCorrect = false;
                break;
            }
        }

        if (allCorrect)
        {
            Debug.Log("Последовательность верна!");

            // Включаем физику на кнопках
            foreach (Button btn in controlButtons)
            {
                if (btn != null)
                {
                    Rigidbody2D rb = btn.GetComponent<Rigidbody2D>();
                    if (rb != null)
                    {
                        rb.simulated = true;
                    }
                }
            }

            // Открываем дверь
            door?.Open();

            enabled = false;
        }
    }
}