using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting;

public class Shop : MonoBehaviour
{

    [SerializeField] GameObject canvas;
    FPSController fpsCon;
    public List<RawImage> ipadSprites;
    public List<TMP_Text> ipadLabels;
    public List<TMP_Text> IpadDescriptions;
    public PlayerHealth health;
    public Rocket rocket;
    PlayerManager playerManager;
    public WeaponData dataRocket;
    public WeaponData dataPistol;
    public Animator animator;

    public int whichItem;
    public ItemSet ItemSet;
    private void Start()
    {
        canvas.SetActive(false);
    }
    void drawItems()
    {
        for (int i = 0; i <= 2; i++)
        {
            ipadSprites[i].texture = ItemSet.itemSet[i].sprite;
            ipadLabels[i].text = ItemSet.itemSet[i].name;
            IpadDescriptions[i].text = ItemSet.itemSet[i].description;
        }

    }

    void appStats(int whichItem)
    {
        Debug.Log("zmieniono staty");
        Rocket.explosionDamage += ItemSet.itemSet[whichItem].rocketDamage;
        WaterDroplet.damageMultiplier += ItemSet.itemSet[whichItem].pistolDamage;
        FPSController.accelerationMultiplier += ItemSet.itemSet[whichItem].movementSpeed;
        Rocket.explosionRadius += ItemSet.itemSet[whichItem].blastRadius;
        health = GameObject.Find("PlayerHolderDungeon(Clone)").GetComponent<PlayerHealth>();
        health.ChangeHealth(ItemSet.itemSet[whichItem].recoverHP);
        health.maxHP += ItemSet.itemSet[whichItem].maxHP;
        // Weapon.currentAmmo += ItemSet.itemSet[whichItem].maxHP;

        // i ¿eby nie powtarza³y sie itemy które ju¿ zebra³eœ
        //no i mo¿e jeszcze troche itemków
        // omg tyle tego
        // strona z której s¹ sprity do itemów: https://www.flaticon.com/
        fpsCon.isInputDisabled = false;
        playerManager.cursorLock = false;
        canvas.SetActive(false);
    }
    public void changestuff1()
    {
        appStats(0);
    }
    public void changestuff2()
    {
        appStats(1);
    }
    public void changestuff3()
    {
        appStats(2);
    }

    public void TurnOn()
    {
        fpsCon = GameObject.Find("Player").GetComponent<FPSController>();
        fpsCon.gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
        fpsCon.isInputDisabled = true;
        playerManager.cursorLock = true;
        canvas.SetActive(true);
        gameObject.GetComponent<ItemSet>();
        ItemSet.SelectRandomItems();
        drawItems();
        animator.Play("ShopOpening");
    }
}