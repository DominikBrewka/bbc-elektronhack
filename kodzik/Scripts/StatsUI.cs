using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class StatsUI : MonoBehaviour
{
    [SerializeField] GameObject stats;
    bool isEnabled = true;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            if(isEnabled)
            {
                stats.SetActive(true);
                isEnabled = false;
            }
            else
            {
                stats.SetActive(false);
                isEnabled = true;
            }
        }
        
    }
}
