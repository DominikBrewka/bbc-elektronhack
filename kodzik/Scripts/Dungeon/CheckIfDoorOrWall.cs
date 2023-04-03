using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CheckIfDoorOrWall : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject door;
    public GameObject fill;
    public GameObject trigger;
    int isDoorOrWallOrNothingLol = 0;
    Transform minimapPlate;
    // 0 - nothing | 1 - wall | 2 - door
    void Start()
    {
        minimapPlate = transform.parent.parent.parent.GetChild(0);
        StartCoroutine(nameof(CheckIfTrigger));
    }
    void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("dungeonDoor"))
        {
            isDoorOrWallOrNothingLol = 2;
        }
        else if(other.CompareTag("dungeonRoom"))
        {
            isDoorOrWallOrNothingLol = 1;
        }
    }

    IEnumerator CheckIfTrigger()
    {
        //I need to use random because without it the same door will still appear
        yield return new WaitForSeconds(Random.Range(0.1f, 0.2f));
        switch (isDoorOrWallOrNothingLol)
        {
            case 0:
                if (GetComponentInParent<DoorRespControler>().howMuchDoorItHave >= GetComponentInParent<DoorRespControler>().whatIsMaxNumberOfDoors)
                {
                    yield return null;
                }
                else
                {
                    GetComponentInParent<DoorRespControler>().howMuchDoorItHave++;
                    transform.gameObject.tag = "dungeonDoor";
                    fill.SetActive(false);
                    door.SetActive(true);
                    door.transform.GetChild(0).transform.SetParent(minimapPlate);
                    transform.GetChild(1).tag = "dungeonDoor";
                    transform.GetChild(2).tag = "dungeonDoor";
                    transform.GetChild(0).tag = "dungeonDoor";
                }

                /*int los = Random.Range(1, 100);
                if(los < 80 ) 
                {
                    //chyba nic bo juz jest sciana lol?
                }
                else
                {
                    //jakos zrobic sie drzwiami musi lmao :)
                    trigger.tag = "dungeonDoor";
                    door.SetActive(true);
                }*/
                break; 
            case 1:
                // chyba nic lol jest sciana przeciez juz
                break;
            case 2:
                // tutaj musi byc puste bo drzwi juz sa po drugiej stronie
                fill.SetActive(false);
                break;
            default:
                Debug.Log("error");
                break;
        }
    }
}
