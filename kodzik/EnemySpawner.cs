using System;
using System.Collections;
using UnityEngine;


public class EnemySpawner : MonoBehaviour
{
    public EnemyManager.Enemy[] enemies;
    public int howMuchEnemyIsInRoom = 0;
    public GameObject[] enemySpawnpoints;
    public float difficultyLevel = 1;
    public bool isForBoss = false;
    // Start is called before the first frame update
    void Start()
    {
        //StartCoroutine("RespEnemies");
    }
    // for debug
    public IEnumerator RespEnemies()
    {
        transform.parent.GetChild(0).gameObject.SetActive(true);

        EnemyManager enemyManager = (EnemyManager)ManagerObject.gameStateManger.GetManager<EnemyManager>();
        if (!isForBoss)
        {
            enemies = enemyManager.enemies;
            float howMuchBattleRatingForOneSpawnpoint = enemySpawnpoints.Length * 1.2f / difficultyLevel;
            for (int i = 0; i < enemySpawnpoints.Length; i++)
            {
                float howMuchRating = howMuchBattleRatingForOneSpawnpoint + UnityEngine.Random.Range(-4 * enemySpawnpoints.Length * 0.2f, 4 * enemySpawnpoints.Length * 0.2f);
                EnemyManager.Enemy bestEnemy = enemies[0];
                float closest = float.MaxValue;
                float minDifference = float.MaxValue;
                foreach (EnemyManager.Enemy element in enemies)
                {
                    var difference = Math.Abs((long)element.battleRating - howMuchRating);
                    if (minDifference > difference)
                    {
                        minDifference = (int)difference;
                        closest = element.battleRating;
                        bestEnemy = element;
                    }
                }
                howMuchEnemyIsInRoom++;
                GameObject go = Instantiate(bestEnemy.obj, enemySpawnpoints[i].transform);
                //go.transform.SetParent(null);
                go.GetComponent<Enemy>().enemySpawner = this;
            }
        }
        else
        {
            GameObject go = Instantiate(enemies[0].obj, enemySpawnpoints[0].transform);
            go.GetComponent<Enemy>().enemySpawner = this;
            Debug.Log("Boss");
        }
        //Destroy(enemySpawnpoints[i]);
        //bestEnemy.obj.GetComponent < "NazwaScryptuOponenta" >.enemySpawner = this;

        yield return null;
    }
    public void EnemyDown()
    {

        howMuchEnemyIsInRoom--;
        if (howMuchEnemyIsInRoom == 0)
        {
            PlayerManager playerManager = (PlayerManager)ManagerObject.gameStateManger.GetManager<PlayerManager>();
            playerManager.canOpenDoor = true;
            playerManager.canOpenDoorText.text = "Mozesz otworzyc drzwi";
            UIManager ui = (UIManager)ManagerObject.gameStateManger.GetManager<UIManager>();
            ui.showShop();
            Destroy(this);
            return;
        }
    }
}
