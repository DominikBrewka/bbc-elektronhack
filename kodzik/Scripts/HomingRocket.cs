using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomingRocket : MonoBehaviour
{
    public float explosionRadius = 5.0f;
    public float explosionForce = 1000.0f;
    public float explosionDamage = 60f;
    Transform target;
    [SerializeField] float rocketSpeed;
    [SerializeField] float rotateSpeed;
    [SerializeField] Rigidbody rb;
    [SerializeField] MeshRenderer mesh;
    public ParticleSystem particle;

    void Start() {
        target = ManagerObject.gameStateManger.GetManager<PlayerPosManager>().transform;
    }

    void FixedUpdate() {
        if (Input.GetKey(KeyCode.Backslash)) Destroy(gameObject);
        Vector3 dir = target.position - transform.position;
        Vector3 newDir = Vector3.RotateTowards(transform.forward, dir, rotateSpeed * Time.deltaTime, 0.0f);
        // transform.Translate(Vector3.forward * Time.deltaTime * rocketSpeed, Space.Self);

        transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(newDir), Time.deltaTime * rotateSpeed);

        rb.velocity = transform.forward * rocketSpeed;
    }

    void OnTriggerEnter(Collider col) {

        if (col.gameObject.layer == LayerMask.NameToLayer("Minimap")) return;
        print(col.transform.name);

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
            if (hit.transform == target) {
                float dist = Vector3.Distance(transform.position, hit.transform.position);
                if (dist < 1) dist = 1;
                float _dmg = explosionDamage * (1/dist);
                hit.transform.parent.gameObject.GetComponent<PlayerHealth>(). ChangeHealth(-_dmg);
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
