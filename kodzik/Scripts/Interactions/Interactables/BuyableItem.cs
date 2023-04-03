using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Events;

public class BuyableItem : MonoBehaviour, IInteractable
{
    public UnityEvent buyEvent; //JD (jest dobrze)
    public int price;
    public TMP_Text notEnoughMoneyText;
    public PlayerCurrency playerCurrency;
    int playerMoney;
    public GameObject playerHolderPrefab;
    public Weapon weapon;
    public int weaponSlot;

    [SerializeField] private string _prompt;
    public string InteractionPrompt => _prompt;
   
    void Awake()
    {
        playerMoney = playerCurrency.balance;
    }
    void Start()
    {
        print(playerCurrency.balance);
    }

    public bool Interact(InteractionManager interactor)
    {
        if (playerCurrency.AddBalance(-price)) 
        {
            //Weapon pick up

            
            PlayerShooting playerShootingScript = interactor.transform.parent.GetComponent<PlayerShooting>();
            weapon = playerShootingScript.weaponsList[2];
            playerShootingScript.heldWeapons[weaponSlot] = weapon;

            buyEvent.Invoke();
            print(playerCurrency.balance);
            return true;
        }
        else
        {
            notEnoughMoneyText.gameObject.SetActive(true);
            return false;
        }
    }

    public GameObject getGameObject()
    {
        return this.gameObject;
    }
}
