using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : ManagerObject
{
    // Start is called before the first frame update
    public Enemy[] enemies;
    public override void Start()
    {
        base.Start();
    }
    [Serializable]
    public class Enemy
    {
        public GameObject obj;
        public float battleRating = 1;
    }
}
