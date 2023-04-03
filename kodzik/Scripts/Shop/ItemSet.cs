using UnityEngine;
using System;

public class ItemSet : MonoBehaviour
{

    public static ItemData[] itemSet = new ItemData[3];
    public ItemData[] scriptableObjects;
    [SerializeField] ItemData[] scriptableObjectsTemp;
    public void SelectRandomItems()
    {
        scriptableObjectsTemp = new ItemData[scriptableObjects.Length];
        Array.Copy(scriptableObjects,scriptableObjectsTemp,scriptableObjects.Length);
            for (int i = 0; i <= 2; i++) {
                
                int count = UnityEngine.Random.Range(0, scriptableObjectsTemp.Length);
                if (scriptableObjectsTemp[count] != null)
                {
                    itemSet[i] = scriptableObjectsTemp[count];
                    scriptableObjectsTemp[count]=null;
                }
                else
                {
                    i--;
                }
                
            }
  
    }
}
