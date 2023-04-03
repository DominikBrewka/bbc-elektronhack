using UnityEngine;
using TMPro;

public class PlayerCurrency : MonoBehaviour
{

    // TODO move this to PlayerManager.cs
    public int balance;
    [SerializeField] TMP_Text text;

    void OnCollisionEnter(Collision collision) {
        Money moneyCollect = collision.gameObject.GetComponent<Money>();
        if (moneyCollect == null)
        {
            return;
        }

        AddBalance(moneyCollect.amount);
        Destroy(collision.gameObject);
    }

    void UpdateGUI() {
        text.text = "$" + balance.ToString("N0");
    }

    public int GetBalance() {
        UpdateGUI();
        return balance;
    }

    public bool SetBalance(int _bal) {
        if (_bal < 0) return false; 

        balance = _bal;
        UpdateGUI();
        return true;
    }

    public bool AddBalance(int _bal)     {
        if (_bal + balance < 0) return false;

        balance += _bal;
        UpdateGUI();
        return true;
    }
}
