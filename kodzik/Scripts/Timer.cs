using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{
    [SerializeField] TMP_Text text;
    float timer = 0;

    void Update()
    {
        timer += Time.deltaTime;
        float minutes = Mathf.Floor(timer / 60);
        float seconds = Mathf.Floor(timer % 60);

        text.text = minutes.ToString("00") + ":" + seconds.ToString("00");
    }
}
