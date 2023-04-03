using UnityEngine;

public class Baguette : Enemy
{

    [SerializeField] float jumpForce;
    [SerializeField] float lookSpeed;
    [SerializeField] float range;
    [SerializeField] float attackRange;
    [SerializeField] float damageRange;
    [SerializeField] bool triedAttack = false;
    [SerializeField] LayerMask groundLayers;
    [SerializeField] Transform groundcheck;
    public int AIState;
    Transform target;

    void FixedUpdate()
    {

        if (target == null) {
            try { target = ManagerObject.gameStateManger.GetManager<PlayerPosManager>().transform; }
            catch { return; }
        }
        // doesn't work in Start() for some reason ???????

        // AI
        Vector3 dir = new Vector3(target.position.x, this.transform.position.y, target.position.z);
        float dist = Vector3.Distance(transform.position, target.position);
        if (dist > range) AIState = 0;
        else AIState = 1;
        if (dist < attackRange) {
            AIState = 2;
        }
        if (triedAttack) {
            AIState = 3;
        }

        switch (AIState)
        {
            case 0:
                // ...
                // idle
                // ...
                break;
            case 1:
                // fetch
                transform.LookAt(dir);
                rb.MovePosition(Vector3.MoveTowards(transform.position, target.position, walkSpeed * Time.deltaTime));
                break;
            case 2:
                // attack
                JumpTowards(dir);
                if (dist < damageRange && !triedAttack) {
                    TryAttack();
                }
                break;
            case 3:
                // retreat
                JumpTowards(dir, true);
                if (GroundCheck()) {
                    triedAttack = false;
                }
                break;
        }
    }

    void JumpTowards(Vector3 dir, bool backwards = false)
    {
        if (!GroundCheck()) return;
        transform.LookAt(dir);
        if (backwards) {
            rb.AddForce(jumpForce * -transform.forward);
        } else {
            rb.AddForce(jumpForce * transform.forward);
        }
        rb.AddForce(jumpForce * 1 * transform.up);
    }

    void TryAttack()
    {
        Collider[] hits = Physics.OverlapSphere(transform.position, damageRange);
        foreach (var hit in hits) {
            if (hit.gameObject == target.gameObject) {
                // print("ATTTACKKKKKK");
                target.parent.GetComponent<PlayerHealth>().ChangeHealth(-data.strength);
            }
        }
        triedAttack = true;
    }

    bool GroundCheck()
    {
        bool _gc = Physics.Raycast(groundcheck.position, Vector3.down, out RaycastHit hit, 0.1f, groundLayers);
        return _gc;
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, range);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, attackRange);
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, damageRange);
    }
}