using UnityEngine;
using TMPro;
using System.Collections;
using System.Collections.Generic;

public class SoundSequenceGame : MonoBehaviour
{
    [Header("Звуки")]
    [SerializeField] private AudioClip[] soundClips; // 8 звуков (индексы 0-7)

    [Header("Громкость")]
    [Range(0f, 1f)] public float sequenceVolume = 1f; // ← регулируй в инспекторе!

    [Header("Дверь")]
    [SerializeField] private Door door;

    [Header("Текст")]
    [SerializeField] private TextMeshProUGUI statusText;

    [Header("Блоки для ввода")]
    [SerializeField] private TextMeshProUGUI[] blockTexts;

    [Header("Настройки")]
    [SerializeField] private float checkInterval = 3f;

    private List<int> correctSequence = new List<int>();
    private AudioSource audioSource;
    private bool sequencePlayed = false;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
            audioSource = gameObject.AddComponent<AudioSource>();

        if (blockTexts.Length != 8)
        {
            Debug.LogError("Нужно ровно 8 TextMeshPro в blockTexts!");
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
        for (int i = 0; i < 8; i++)
        {
            int randomNumber = Random.Range(1, 9);
            correctSequence.Add(randomNumber);
        }

        Debug.Log("Правильная последовательность: " + string.Join(", ", correctSequence));
    }

    private IEnumerator PlaySequence()
    {
        if (statusText != null)
            statusText.text = "Слушай последовательность...";

        yield return new WaitForSeconds(1f);

        foreach (int soundNumber in correctSequence)
        {
            yield return new WaitForSeconds(0.5f);

            int soundIndex = soundNumber - 1;

            if (audioSource != null && soundClips[soundIndex] != null)
            {
                // 🔊 Используем sequenceVolume!
                audioSource.PlayOneShot(soundClips[soundIndex], sequenceVolume);
                yield return new WaitForSeconds(soundClips[soundIndex].length + 0.2f);
            }
        }

        sequencePlayed = true;
        if (statusText != null)
            statusText.text = "Установи правильные числа!";
    }

    private IEnumerator CheckSequenceRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(checkInterval);

            if (!sequencePlayed) continue;

            List<int> playerInput = new List<int>();
            bool allValid = true;

            for (int i = 0; i < 8; i++)
            {
                if (blockTexts[i] == null)
                {
                    allValid = false;
                    break;
                }

                string text = blockTexts[i].text;
                if (int.TryParse(text, out int number) && number >= 1 && number <= 8)
                {
                    playerInput.Add(number);
                }
                else
                {
                    allValid = false;
                    break;
                }
            }

            if (!allValid || playerInput.Count != 8)
            {
                if (statusText != null)
                    statusText.text = "Установи числа от 1 до 8!";
                continue;
            }

            bool isCorrect = true;
            for (int i = 0; i < 8; i++)
            {
                if (playerInput[i] != correctSequence[i])
                {
                    isCorrect = false;
                    break;
                }
            }

            if (isCorrect)
            {
                Debug.Log("ПОБЕДА! Дверь открывается!");
                if (door != null)
                    door.Open();
                if (statusText != null)
                    statusText.text = "Молодец!";
                
                // Включаем обычную музыку уровня
                if (SoundManager.instance != null)
                {
                    SoundManager.instance.PlayRandomLevelMusic();
                }
                
                enabled = false;
                break;
            }
        }
    }
}