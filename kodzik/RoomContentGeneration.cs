using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomContentGeneration : MonoBehaviour
{
    public RoomVariant[] roomVariants;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine("RespVariant");
    }
    public IEnumerator RespVariant()
    {
        yield return new WaitForSeconds(0.5f);
        int index = UnityEngine.Random.Range(0, roomVariants.Length);
        Instantiate(roomVariants[index].roomObj, transform.parent);
        yield return null;
    }

    [Serializable]
    public class RoomVariant
    {
        public GameObject roomObj;
        public float chance = 0;
    }
}
