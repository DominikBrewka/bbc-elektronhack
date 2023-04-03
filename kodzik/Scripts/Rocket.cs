using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : MonoBehaviour
{
    public static float explosionRadius = 5.0f;
    public float explosionForce = 1000.0f;
    public static float explosionDamage = 15f;
    [SerializeField] LayerMask enemyLayer;
    [SerializeField] float rocketSpeed;
    [SerializeField] Rigidbody rb;
    [SerializeField] MeshRenderer mesh;
    public ParticleSystem particle;

    void Update() {
        rb.AddForce(transform.forward * rocketSpeed * Time.fixedDeltaTime, ForceMode.Force);
    }

    void OnTriggerEnter(Collider col) {
        // explode
        // Make the enemy disappear
        
        // if (col.gameObject.layer == LayerMask.NameToLayer("Interactable")) Destroy(col.gameObject);
        
        // Push other objects away from the explosion

        Vector3 explosionPos = transform.position;
        Collider[] colliders = Physics.OverlapSphere(explosionPos, explosionRadius);
        
        foreach (Collider hit in colliders)
        {
            Rigidbody _rb = hit.GetComponent<Rigidbody>();
            if (_rb != null)
            {
                _rb.AddExplosionForce(explosionForce, transform.position, explosionRadius);
            }
            IDamagable dmg = hit.GetComponent<IDamagable>();
            if (dmg != null) {
                float dist = Vector3.Distance(transform.position, hit.transform.position);
                if (dist < 1) dist = 1;
                float _dmg = explosionDamage * (1/dist);
                dmg.Damage(_dmg);
            }
        }
        // destroy
        Destroy(gameObject, 1f);
        rb.isKinematic = false;
        particle.Play();
        rb.isKinematic = true;
        rb.velocity = Vector3.zero;
        mesh.enabled = false;
    }
    
}
