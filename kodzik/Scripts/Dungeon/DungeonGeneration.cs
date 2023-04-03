using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DungeonGeneration : MonoBehaviour
{
    public int ileMaBycPokoiWDungeonie = 50;
    public Room[] listaPokojowBossa;
    
    public Room[] lista2X2;
    public GameObject pokoj1X1;
    public GameObject pokoj1X1Koncowy;
    public Room[] listaPokoi;

    Quaternion[] listaMozliwychObrotowBudowli = { Quaternion.Euler(0, 0, 0), Quaternion.Euler(0, 90, 0), Quaternion.Euler(0, 180, 0) };
    GameObject pokoj;
    void Start()
    {
        StartCoroutine("UtworzDungeonV2");
    }
    IEnumerator UtworzDungeonV2()
    {
        bool czyBossJestZrespiony = false;
        GameObject[] miejsceNaDungeon;
        for (int i = 1 /* 0 */; i < /* ilo��Przej��GenerowaniaDungeon�w */ ileMaBycPokoiWDungeonie; /* i++ */)
        {
            miejsceNaDungeon = GameObject.FindGameObjectsWithTag("dungeon");
            if (miejsceNaDungeon.Length == 0)
            {
                print("Brak spawpointow - cos sie musialo zbugowac");
                yield break;
            }
            foreach (GameObject spawpoint in miejsceNaDungeon)
            {
                int licznikObroceniaPokoju = 0;
                Quaternion jakaRotacjaPomieszczenia;
                bool czyMiejsceZnalezione = false;
                void ObrocPokoj()
                {
                    jakaRotacjaPomieszczenia = (listaMozliwychObrotowBudowli[licznikObroceniaPokoju]);
                    licznikObroceniaPokoju++;
                }
                void StworzIUstawPokoj(GameObject obj)
                {
                    pokoj = Instantiate(obj, spawpoint.transform);
                    pokoj.transform.SetParent(null);
                    pokoj.transform.rotation = jakaRotacjaPomieszczenia;
                }

                //Sprawdza jaki pok�j si� zmie�ci.
                ObrocPokoj();
                /*                bool czyLosowacNowyIndex = true;
                                int index = 0;
                                listaPokoi = lista2X2;
                                int dlugoscListyPokoi = listaPokoi.Length;
                                while (!czyMiejsceZnalezione || dlugoscListyPokoi >= 0)
                                {
                                    if (czyLosowacNowyIndex == true) 
                                    {
                                        print("1");
                                        index = UnityEngine.Random.Range(0, listaPokoi.Length);
                                        czyLosowacNowyIndex = false;
                                    }
                                    if (listaPokoi[index].roomObj != null)
                                    {
                                        print("2");
                                        if (listaPokoi[index].chance >= UnityEngine.Random.Range(0, 100))
                                        {
                                            print("4");
                                            Stw�rzIUstawPok�j(listaPokoi[index].roomObj);
                                            yield return new WaitForSeconds(0.02f);
                                            if (pok�j != null)
                                            {
                                                print("5");
                                                //Moze byc postawione 
                                                czyMiejsceZnalezione = true;
                                            }
                                            else
                                            {
                                                print("6");
                                                if (licznikObr�ceniaPokoju < listaMo�liwychObrot�wBudowli.Length)
                                                {
                                                    print("7");
                                                    Obr��Pok�j();
                                                    czyLosowacNowyIndex = false;
                                                    // czyLosowacNowyIndex �eby ca�y czas ten sam budynek by� testowany :)
                                                }
                                                else
                                                {
                                                    print("8");
                                                    listaPokoi[index].roomObj = null;
                                                    dlugoscListyPokoi--;
                                                    licznikObr�ceniaPokoju = 0;
                                                    czyLosowacNowyIndex = true;
                                                }
                                            }
                                        }
                                        else
                                        {
                                            print("5");
                                            listaPokoi[index].roomObj = null;
                                            czyLosowacNowyIndex = true;
                                        }
                                    }
                                    else
                                    {
                                        print("3");
                                        czyLosowacNowyIndex = true;
                                    }
                                }*/

                // Dzia�a i wybiera najwi�kszy mo�liwy
                for (int j = 0; j < lista2X2.Length && !czyMiejsceZnalezione; j++)
                {
                    StworzIUstawPokoj(lista2X2[j].roomObj);
                    yield return new WaitForSeconds(0.02f);
                    if (pokoj != null)
                    {
                        //Moze byc postawione 
                        czyMiejsceZnalezione = true;
                    }
                    else
                    {
                        if (licznikObroceniaPokoju < listaMozliwychObrotowBudowli.Length)
                        {
                            ObrocPokoj();
                            j--;
                            // j--, �eby ca�y czas ten sam budynek by� testowany :)
                        }
                        else
                        {
                            licznikObroceniaPokoju = 0;
                        }
                    }
                }


                if (!czyMiejsceZnalezione)
                {
                    StworzIUstawPokoj(pokoj1X1);
                }

                yield return new WaitForSeconds(0.02f);
                if (pokoj != null)
                {
                    i++;
                    pokoj.GetComponentInChildren<DungeonCheckingIfCanPlaceRoom>().TworzenieScian();
                }

                Destroy(spawpoint);

                // Jedynie je�li generujemy ilo�ciowo pokoje.
            }
            yield return new WaitForSeconds(0.001f);
        }
        //Ma respi� zako�czenia budowli, i bossa. Wyst�puje, gdy ilo�� podziemi przekroczy limit.
        miejsceNaDungeon = GameObject.FindGameObjectsWithTag("dungeon");

        foreach (GameObject spawpoint in miejsceNaDungeon)
        {

            if (!czyBossJestZrespiony)
            {
                pokoj = Instantiate(listaPokojowBossa[0].roomObj, spawpoint.transform);
                pokoj.transform.SetParent(null);
                yield return new WaitForSeconds(0.02f);
                if (pokoj != null)
                {
                    czyBossJestZrespiony = true;
                    print("Boss jest niby");
                    pokoj.GetComponentInChildren<DungeonCheckingIfCanPlaceRoom>().TworzenieScian();
                }
            }
            else
            {
                pokoj = Instantiate(pokoj1X1Koncowy, spawpoint.transform);
                pokoj.transform.SetParent(null);

                yield return new WaitForSeconds(0.02f);
                if (pokoj != null)
                {
                    pokoj.GetComponentInChildren<DungeonCheckingIfCanPlaceRoom>().TworzenieScian();
                }
            }
            Destroy(spawpoint);
        }
        Debug.Log("Koniec");
        yield return null;
    }
    [Serializable]
    public class Room
    {
        public float chance = 1;
        public GameObject roomObj;
    }
}
