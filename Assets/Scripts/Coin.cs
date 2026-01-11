using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    public Coins manager; // ← ссылка на менеджер

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            manager.CollectCoin(gameObject); // передаём саму монету
        }
    }
}
