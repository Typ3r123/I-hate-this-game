using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelMusic : MonoBehaviour
{

    void Start()
    {
        gameObject.SetActive(true); // включает объект
        GetComponent<AudioSource>().Play();
    }
}
