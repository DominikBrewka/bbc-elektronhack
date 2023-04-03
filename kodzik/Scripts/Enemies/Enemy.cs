using UnityEngine;

public class Enemy : MonoBehaviour, IDamagable
{
    public EnemySpawner enemySpawner;
    public EnemyData data;
    float health, defense;
    bool whenYouDieDontCallOnDeathTwoTimesYouStupid = true;
    [SerializeField] protected Rigidbody rb;
    [SerializeField] protected float walkSpeed;

    void Start() {
        health = data.health;
        defense = data.defense;
        InvokeRepeating("CheckIfBuged", 0f, 2f);
    }
    public void CheckIfBuged()
    {
        if (transform.position.y < -100)
        {
            OnDeath();
        }
    }
    public virtual float Damage(float amount) {
        float startHealth = health;
        health -= amount / defense;
        if (health <= 0f) {
            OnDeath();
        }

        Debug.Log("Damaged " + gameObject + (health-startHealth));
        return startHealth-health;
    }
    public virtual void OnDeath() 
    {
        if (!whenYouDieDontCallOnDeathTwoTimesYouStupid)
        {
            return;
        }
        whenYouDieDontCallOnDeathTwoTimesYouStupid = false;
        Debug.Log("Died " + gameObject);
        health = 0f;
        
        if (enemySpawner != null) { enemySpawner.EnemyDown(); }
        Destroy(gameObject);
    }
}