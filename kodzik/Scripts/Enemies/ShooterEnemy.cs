using UnityEngine;

public class ShooterEnemy : Enemy
{

    [SerializeField] float jumpForce;
    [SerializeField] float lookSpeed;
    [SerializeField] float range;
    [SerializeField] float attackRange;
    [SerializeField] LayerMask groundLayers;
    [SerializeField] Transform groundcheck;

    [SerializeField] float shootDelay;
    [SerializeField] float shootTimer = 0;
    [SerializeField] GameObject projectile;
    public int AIState;
    Transform target;

    void FixedUpdate()
    {

        if (target == null) {
            try { target = ManagerObject.gameStateManger.GetManager<PlayerPosManager>().transform; }
            catch { return; }
        }

        shootTimer += Time.deltaTime;

        // AI
        Vector3 dir = new Vector3(target.position.x, this.transform.position.y, target.position.z);
        float dist = Vector3.Distance(transform.position, target.position);
        if (dist > range) AIState = 0;
        else AIState = 1;
        if (dist < attackRange) {
            AIState = 2;
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
                if (shootTimer > shootDelay) {
                    TryAttack();
                    shootTimer = 0f;
                }
                break;
        }
    }
    void TryAttack()
    {
        Vector3 pos = transform.position;
        pos.y += 1;
        Instantiate(Instantiate(projectile, pos, transform.rotation));
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
    }
}