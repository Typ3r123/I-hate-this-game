using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coins : MonoBehaviour
{
    public GameObject coinA;
    public GameObject coinB;
    public Door door;

    [Header("Звук подбора")]
    public AudioClip pickUpSound;       // ← звук в инспекторе
    [Range(0f, 1f)] public float soundVolume = 1f; // ← громкость

    private AudioSource audioSource;
    private int totalCoins = 0;
    private const int maxCoins = 10;

    void Start()
    {
        // Начинаем с монеты A
        if (coinA != null) coinA.SetActive(true);
        if (coinB != null) coinB.SetActive(false);

        // Создаём AudioSource, если его нет
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
            audioSource.playOnAwake = false;
            audioSource.spatialBlend = 0f; // 2D звук
        }
    }

    public void CollectCoin(GameObject collectedCoin)
    {
        totalCoins++;
        MoneyText.coin = totalCoins;
        Debug.Log($"Собрана монета {collectedCoin.name}. Всего: {totalCoins}");

        // Проигрываем звук
        if (pickUpSound != null && audioSource != null)
        {
            audioSource.PlayOneShot(pickUpSound, soundVolume);
        }

        // Скрываем собранную монету
        collectedCoin.SetActive(false);

        if (totalCoins >= maxCoins)
        {
            if (coinA != null) coinA.SetActive(false);
            if (coinB != null) coinB.SetActive(false);
            if (door != null) door.Open();
            return;
        }

        // Показываем другую монету
        if (collectedCoin == coinA && coinB != null)
        {
            coinB.SetActive(true);
        }
        else if (collectedCoin == coinB && coinA != null)
        {
            coinA.SetActive(true);
        }
    }
}
