using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{

    Animator anim;

    void Start()
    {
        anim = GetComponent<Animator>();
    }

    public void Open() {
        anim.SetBool("IsOpen", true);
    }
    
    public void Close() {
        anim.SetBool("IsOpen", false);
    }
    public void ShowSector()
    {
        anim.SetBool("IsOpen", false);
        // nie pokazuje bo nie wiem jak referencje zrobic do enemy spawner a czasu nie ma ez
    }
}
