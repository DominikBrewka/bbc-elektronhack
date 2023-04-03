using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public float maxHP=100;
    public float health =100;
    public bool isDed=false;
    [SerializeField] TMP_Text text;
    FPSController fpsCon;
    PlayerManager playerManager;
    private void Start()
    {
        text = GameObject.Find("HEALTH").GetComponent<TMP_Text>();
        text.text = "Zdrowie: " + health;
    }
    
    void UpdateGUI()
    {
        text.text = "Zdrowie: " + (int) health;
    }
    public void Dead()
    {

        PlayerManager playerManager = (PlayerManager)ManagerObject.gameStateManger.GetManager<PlayerManager>();
        fpsCon = GameObject.Find("Player").GetComponent<FPSController>();
        fpsCon.gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
        fpsCon.isInputDisabled = true;
        playerManager.cursorLock = true;
        Time.timeScale = 0;
        GameObject.Find("LOSE CANVAS").transform.GetChild(0).gameObject.SetActive(true);
    }
    public float Gethealth()
    {
        UpdateGUI();
        return health;
    }

    public bool Sethealth(float _hp)
    {
        if (_hp < 0)
        {
            Dead();
            return false;
        }

        health = _hp;
        UpdateGUI();
        return true;
    }
    
    public bool ChangeHealth(float _hp)
    {
        
        if (_hp + health < 0) 
        { 
            Dead();
            isDed=true; 
            health = 0; 
        }
        if(health > maxHP) _hp=_hp-((_hp+health)-maxHP);

        health += _hp;
        UpdateGUI();
        return true;
    }

}
