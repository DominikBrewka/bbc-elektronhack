using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorTrigger : MonoBehaviour
{
    public Door doorScript;
    public DoorButton doorButton;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            doorButton.GetRoom();
            doorScript.ShowSector();
            Destroy(gameObject);
        }
    }
}
