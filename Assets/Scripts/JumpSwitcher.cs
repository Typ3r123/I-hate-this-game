using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpSwitcher : MonoBehaviour
{
    public GameObject[] blocksA; // ← массив блоков A
    public GameObject[] blocksB; // ← массив блоков B

    private int state = 0; // 0 = A, 1 = B

    void Start()
    {
        ShowState(0); // начинаем с A
    }

    public void ToggleState()
    {
        state = 1 - state; // переключаем: 0 → 1, 1 → 0
        ShowState(state);
    }

    void ShowState(int s)
    {
        if (s == 0) // показываем A
        {
            foreach (GameObject block in blocksA)
                if (block != null) block.SetActive(true);

            foreach (GameObject block in blocksB)
                if (block != null) block.SetActive(false);
        }
        else // показываем B
        {
            foreach (GameObject block in blocksA)
                if (block != null) block.SetActive(false);

            foreach (GameObject block in blocksB)
                if (block != null) block.SetActive(true);
        }

        Debug.Log("Показаны: " + (s == 0 ? "Blocks A" : "Blocks B"));
    }
}
