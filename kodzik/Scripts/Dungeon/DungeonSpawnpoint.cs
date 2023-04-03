using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonSpawnpoint : MonoBehaviour
{
    [Header("1 to lewo, 2 to prawo, 3 to góra, 4 to dó³")]
    public int miejsceNaDrzwi;
    /*private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "dungeonRoom" || other.tag == "dungeon")
        {
            Destroy(gameObject);
        }
    }*/
}
