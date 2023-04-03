using UnityEngine;

public class Cake : Enemy
{

    [SerializeField] float jumpForce;
    [SerializeField] float lookSpeed;
    [SerializeField] float range;
    [SerializeField] float attackRange;
    [SerializeField] float damageRange;
    [SerializeField] bool triedAttack = false;
    [SerializeField] LayerMask groundLayers;
    [SerializeField] Transform groundcheck;
    [SerializeField] Transform mouth;
    public int AIState;

    [SerializeField] float attackDelay;
    [SerializeField] float attackRest;
    float attackTimer;
    
    [SerializeField] float fireDelay;
    float fireTimer = .2f;
    [SerializeField] float rocketDelay;
    float rocketTimer = .2f;
    Transform target;

    [SerializeField] GameObject fireAttack;
    [SerializeField] GameObject rocketAttack;

    int shot = 0;

    void FixedUpdate()
    {

        // doesn't work in Start() for some reason ???????
        if (target == null) {
            try { target = ManagerObject.gameStateManger.GetManager<PlayerPosManager>().transform; }
            catch { return; }
        }

        
        // AI
        
        // timers
        attackTimer += Time.deltaTime;
        fireTimer += Time.deltaTime;
        rocketTimer += Time.deltaTime;

        // calculate stuff for changing direction
        Vector3 dir = new Vector3(target.position.x, this.transform.position.y, target.position.z);
        float dist = Vector3.Distance(transform.position, target.position);
        if (dist > range) AIState = 0;
        else if (AIState != 3 && AIState != 4) AIState = 2;
        if (dist < attackRange) {
            // if (AIState != 4) {
            //     if (attackTimer > attackDelay) {
            //         AIState++;
            //         attackTimer = 0;
            //     }
            // } else {
            //     if (attackTimer > attackRest) {
            //         AIState = 2;
            //         attackTimer = 0;
            //     }

            // }
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
                
                // look smooth
                LookSmooth(target.position, Time.deltaTime);
                // no bueno 🥲
                
                rb.MovePosition(Vector3.MoveTowards(transform.position, target.position, walkSpeed * Time.deltaTime));
                break;
            case 2:
                // attack 1
                // bro does his little thing where he shoots missiles 🔥🔥

                LookSmooth(target.position, Time.deltaTime);
                if (rocketTimer > rocketDelay) {
                    shot++;
                    rocketTimer = 0;
                    Instantiate(rocketAttack, mouth.position, mouth.rotation);
                }

                if (shot > 10) {
                    shot = 0;
                    AIState = 3;
                }
                break;
            case 3:
                // attack 2

                LookSmooth(target.position, Time.deltaTime);
                if (fireTimer > fireDelay) {
                    shot++;
                    fireTimer = 0;
                    Instantiate(fireAttack, mouth.position, mouth.rotation);
                }

                if (shot > 10) {
                    shot = 0;
                    AIState = 4;
                }
                break;
            case 4:
                AIState= 2;
                // idle ???
                break;
        }
    }

    void LookSmooth(Vector3 dir, float dt) {
        Quaternion secondDir = Quaternion.LookRotation(dir - transform.position);
        secondDir.z = secondDir.x = 0;
        transform.rotation = Quaternion.Slerp(transform.rotation, secondDir, dt * lookSpeed);
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

    bool GroundCheck()
    {
        bool _gc = Physics.Raycast(groundcheck.position, Vector3.down, out RaycastHit hit, 0.5f, groundLayers);
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