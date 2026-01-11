using UnityEngine;
using TMPro; // ← ВАЖНО!

public class MoneyText : MonoBehaviour
{
    public static int coin = 0;
    private TMP_Text text;

    void Start()
    {
        text = GetComponent<TMP_Text>();
        coin = 0; // сброс при старте
    }

    void Update()
    {
        if (text != null)
            text.text = coin.ToString();
    }
}