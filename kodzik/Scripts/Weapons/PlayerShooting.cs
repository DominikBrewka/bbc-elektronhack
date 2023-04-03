using System;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    public static Action shootAction;
    public static Action reloadAction;
    public static Weapon drawnWeapon;
    public static LayerMask enemyLayer;
    // unity can't serialize static variables for some reason
    [SerializeField] LayerMask _enemylayer;
    public Weapon[] heldWeapons = new Weapon[2];
    public List<Weapon> weaponsList;

    private void Start() {
        enemyLayer = _enemylayer;
    }


    void Update() {
        // lord forgive me for my sins
        // 22h till deadline
        // jest dokladnie 2:49 a ja szukam gdzie ty kochany przyjacielu dominiku dales umieranie tej bagietki 
        // wprowadzilem cenzure
        if (drawnWeapon == heldWeapons[0]) {
            if (Input.GetKey(KeyCode.Mouse0)) {
                shootAction?.Invoke();
            }
        } else if (drawnWeapon == heldWeapons[1])
        {
            if (Input.GetKeyDown(KeyCode.Mouse0)) {
                shootAction?.Invoke();
            }
            
        }
        if (Input.GetKeyDown(KeyCode.R)) {
            reloadAction?.Invoke();
        }
        
        // Weapon switching
        if (Input.GetKeyDown(KeyCode.Alpha1)) 
        {
            if (drawnWeapon == heldWeapons[0]) drawnWeapon = null;
            else
            {
                drawnWeapon = heldWeapons[0];
            }
        } 
        else if (Input.GetKeyDown(KeyCode.Alpha2)) 
        {  
            if (drawnWeapon == heldWeapons[1]) drawnWeapon = null;
            else drawnWeapon = heldWeapons[1];
        }
        
    }
}
