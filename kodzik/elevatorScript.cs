using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElevatorScript : MonoBehaviour
{
    public Animator animator;
    public Transform bossSpawn;
    public void GoUp()
    {
        animator.Play("Elevator");
        GetComponent<EnemySpawner>().RespEnemies();
    }    
}
