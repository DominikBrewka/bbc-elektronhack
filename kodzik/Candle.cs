using UnityEngine;

public class Candle : Enemy
{
    [SerializeField] Collider col;
    [SerializeField] GameObject explosion;
    public override void OnDeath() 
    {
        col.enabled = false;
        Instantiate(explosion, transform.position, transform.rotation);
        Destroy(gameObject);
    }
}