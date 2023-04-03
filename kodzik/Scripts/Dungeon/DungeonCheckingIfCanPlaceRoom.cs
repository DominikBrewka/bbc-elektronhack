using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonCheckingIfCanPlaceRoom : MonoBehaviour
{
    public GameObject sciany;
    public bool czySprawdziloCzyMozeBycPostawione = false;
    public bool mozeBycPostawione = true;

    //HEJ CZY TY WIESZ, ZE JA SIE W TOBIE KOCHAM
    //HEJ CZY TY WIESZ, ZE TO POWAZNA RZECZ
    public void TworzenieScian()
    {
        transform.localScale += new Vector3(0.05f, 0f, 0.05f);
        GetComponent<Rigidbody>().isKinematic = true;
        sciany.SetActive(true);
        czySprawdziloCzyMozeBycPostawione = true;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (czySprawdziloCzyMozeBycPostawione)
        {
            return;
        }
        if (collision.gameObject.tag == "dungeonRoom")
        {
            //Debug.Log("Zablokowane bo " + collision.gameObject.name +" Jestem " + gameObject.name);
            Destroy(gameObject.transform.parent.gameObject);
        }
        else
        {
            //Debug.Log("Odblokowane bo " + gameObject.GetComponent<Collider>().name);
        }
    }

}