using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class DoorButton : MonoBehaviour, IInteractable
{
    [SerializeField] private string _prompt;

    public LayerMask ground;
    public string InteractionPrompt => openOrClose();

    public GameObject secButton;

    RaycastHit hit;
    [SerializeField] Door door;
    bool opened = false;
    bool wasOpenedBefore = false;
    string openOrClose()
    {
        PlayerManager playerManager = (PlayerManager)ManagerObject.gameStateManger.GetManager<PlayerManager>();
        if (playerManager.canOpenDoor == false)
        {
            return "Zablokowane";
        }
        return "Otwarte";

    }
    public bool Interact(InteractionManager interactor) 
    {
        PlayerManager playerManager = (PlayerManager)ManagerObject.gameStateManger.GetManager<PlayerManager>();
        if (playerManager.canOpenDoor == false)
        {
            return true;
        }
        if (opened) {
            door.Close();
        }
        else {
            door.Open();
            Physics.Raycast(secButton.transform.position, Vector3.down, out hit, 3f, ground);
            Debug.Log(hit.collider.transform.parent.GetChild(0).gameObject.activeInHierarchy);
            Debug.Log(hit.collider.transform.parent.GetChild(0).gameObject == null);
            if (hit.collider.transform.parent.GetChild(0).gameObject.activeInHierarchy == false)
            {
                // jesli nie byly otwarte lub jesli plate juz jest aktywny to nie zabiera graczowi mozliwosci otwierania drzwi
                playerManager.canOpenDoor = false;
                wasOpenedBefore = true;
                playerManager.canOpenDoorText.text = "Nie mozesz otworzyc drzwi";
            }
        }

        opened = !opened;
        return true;
    }
        
    public void GetRoom()
    {
        Physics.Raycast(transform.position, Vector3.down, out hit, 3f, ground);
        if(hit.collider.transform.parent.GetComponentInChildren<EnemySpawner>() != null)
        {
            hit.collider.transform.parent.GetComponentInChildren<EnemySpawner>().StartCoroutine("RespEnemies");
        }
    }
    public GameObject getGameObject() {
        return this.gameObject;
    }
    void OnDrawGizmos()
    {
        Gizmos.DrawRay(transform.position, Vector3.down * 3f);
    }
}
