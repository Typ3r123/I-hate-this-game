using UnityEngine;
using TMPro;
using System.Collections;
using System.Collections.Generic;

public class LampSequence : MonoBehaviour
{
    [Header("Лампы (ровно 4)")]
    [SerializeField] private GameObject[] lampObjects; // Лампа1, Лампа2, Лампа3, Лампа4

    [Header("Поля ввода (ровно 3)")]
    [SerializeField] private TextMeshProUGUI[] lampTexts;

    [Header("Дверь")]
    [SerializeField] private Door door;

    [Header("Текст")]
    [SerializeField] private TextMeshProUGUI statusText;

    [Header("Настройки")]
    [SerializeField] private float checkInterval = 3f;
    [SerializeField] private float lampDisplayTime = 1f;
    [SerializeField] private float delayBetweenNumbers = 1f;

    private List<int> correctSequence = new List<int>();
    private bool sequencePlayed = false;

    // 🔥 ТВОЙ МАППИНГ: число → индексы ламп (0-based)
    // Индекс 0 = число 1, индекс 8 = число 9
    private static readonly int[][] NumberToLamps = new int[][]
    {
        new int[] { 0 },               // 1 → лампа 1
        new int[] { 1 },               // 2 → лампа 2
        new int[] { 2 },               // 3 → лампа 3
        new int[] { 3 },               // 4 → лампа 4
        new int[] { 1, 2 },            // 5 → лампы 2 и 3
        new int[] { 0, 1, 2 },         // 6 → лампы 1, 2, 3
        new int[] { 2, 3 },            // 7 → лампы 3 и 4
        new int[] { 0, 2, 3 },         // 8 → лампы 1, 3, 4
        new int[] { 0, 1, 2, 3 }       // 9 → все лампы
    };

    private void Start()
    {
        if (lampTexts.Length != 3 || lampObjects.Length != 4)
        {
            Debug.LogError("Нужно 3 поля и 4 лампы!");
            enabled = false;
            return;
        }

        GenerateSequence();
        StartCoroutine(PlaySequence());
        StartCoroutine(CheckSequenceRoutine());
    }

    private void GenerateSequence()
    {
        correctSequence.Clear();
        for (int i = 0; i < 3; i++)
        {
            int num = Random.Range(1, 10); // 1–9
            correctSequence.Add(num);
        }
        Debug.Log("Сгенерирована последовательность: " + string.Join(", ", correctSequence));
    }

    private IEnumerator PlaySequence()
    {
        if (statusText != null)
            statusText.text = "Запомни последовательность...";

        yield return new WaitForSeconds(1f);

        for (int i = 0; i < correctSequence.Count; i++)
        {
            yield return new WaitForSeconds(delayBetweenNumbers);

            int number = correctSequence[i];
            if (number < 1 || number > 9)
            {
                Debug.LogError($"Неподдерживаемое число: {number}");
                continue;
            }

            int[] lampIndices = NumberToLamps[number - 1]; // 1 → индекс 0, 9 → индекс 8

            Debug.Log($"Число {number} → включаем лампы: {string.Join(", ", System.Array.ConvertAll(lampIndices, x => x + 1))}");

            // Включаем нужные лампы
            for (int j = 0; j < lampObjects.Length; j++)
            {
                lampObjects[j].SetActive(System.Array.IndexOf(lampIndices, j) >= 0);
            }

            yield return new WaitForSeconds(lampDisplayTime);

            // Выключаем все
            foreach (var lamp in lampObjects)
                lamp.SetActive(false);
        }

        sequencePlayed = true;
        if (statusText != null)
            statusText.text = "Введи последовательность!";
    }

    private IEnumerator CheckSequenceRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(checkInterval);

            if (!sequencePlayed) continue;

            List<int> input = new List<int>();
            bool valid = true;

            for (int i = 0; i < lampTexts.Length; i++)
            {
                if (lampTexts[i] == null || !int.TryParse(lampTexts[i].text, out int n) || n < 1 || n > 9)
                {
                    valid = false;
                    break;
                }
                input.Add(n);
            }

            if (!valid || input.Count != 3)
            {
                if (statusText != null)
                    statusText.text = "Числа от 1 до 9!";
                continue;
            }

            if (input[0] == correctSequence[0] &&
                input[1] == correctSequence[1] &&
                input[2] == correctSequence[2])
            {
                Debug.Log("✅ ПОБЕДА!");
                door?.Open();
                if (statusText != null) statusText.text = "Молодец!";
                enabled = false;
                break;
            }
        }
    }
}