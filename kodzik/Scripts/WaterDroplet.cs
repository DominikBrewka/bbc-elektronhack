using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class WaterDroplet : MonoBehaviour
{
    float lifeTimer = 0f;
    public float speed = 5f;
    public static float damageMultiplier = 1;
    public float damage = 2.5f;
    public float lifeTime = 1.5f;
    public ParticleSystem particle;

    [SerializeField] Collider collider;
    [SerializeField] Rigidbody rb;
    [SerializeField] MeshRenderer mesh;

    void Start() {
        // add speed on spawn
        rb.AddForce(speed*transform.forward, ForceMode.Impulse);
    }

    void FixedUpdate() {
        // Destroy projectile after some time
        lifeTimer += Time.deltaTime;
        if (lifeTimer > lifeTime) {
            Destroy(gameObject);
        } 
    }

    private void OnCollisionEnter(Collision col) {
        // i know this shouldnt be hard coded it but i dont care anymore
        if (col.gameObject.layer == 10) {
            col.gameObject.GetComponent<IDamagable>().Damage(damage*damageMultiplier);
        }
        Destroy(gameObject,1f);
        rb.isKinematic = true;
        rb.velocity = Vector3.zero;
        transform.rotation = Quaternion.Euler(col.contacts[0].normal);
        collider.enabled = false;
        mesh.enabled = false;
        particle.Play();
        
    }
}
