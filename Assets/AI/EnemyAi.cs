using UnityEngine;
using UnityEngine.AI;

public class EnemyAi : MonoBehaviour
{
    Animator animator;
    int walk;
    int attacking;


    public NavMeshAgent agent;

    public Transform player;

    public LayerMask whatIsGround, whatIsPlayer;

    public float health;

    //Patroling
    public Vector3 walkPoint;
    bool walkPointSet;
    public float walkPointRange;

    //Attacking
    public float timeBetweenAttacks;
    bool alreadyAttacked;
    public GameObject projectile;



    //States
    public float sightRange, attackRange;
    public bool playerInSightRange, playerInAttackRange;



    public GameObject AttackPoint;
    public Transform attackpoint;

     public float spread;

    private void Awake()
    {
        player = GameObject.Find("Player").transform;
        agent = GetComponent<NavMeshAgent>();
    }
    private void Start()
    {
        animator = GetComponent<Animator>();
        walk =  Animator.StringToHash("Running");
        attacking = Animator.StringToHash("Attacking");
    }
    private void Update()
    {
        //Check for sight and attack range
        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);
        Patroling(); animator.SetBool(walk, true); animator.SetBool(attacking, false);
        if (!playerInSightRange && !playerInAttackRange) { Patroling(); animator.SetBool(walk, true); animator.SetBool(attacking, false); }
        if (playerInSightRange && !playerInAttackRange) { ChasePlayer(); animator.SetBool(walk, true); animator.SetBool(attacking, false); }
        if (playerInAttackRange && playerInSightRange) { AttackPlayer(); animator.SetBool(attacking, true); }
    }

    private void Patroling()
    {
        if (!walkPointSet) SearchWalkPoint();

        if (walkPointSet)
            agent.SetDestination(walkPoint);

        Vector3 distanceToWalkPoint = transform.position - walkPoint;

        //Walkpoint reached
        if (distanceToWalkPoint.magnitude < 1f)
            walkPointSet = false;
    }
    private void SearchWalkPoint()
    {
        //Calculate random point in range
        float randomZ = Random.Range(-walkPointRange, walkPointRange);
        float randomX = Random.Range(-walkPointRange, walkPointRange);

        walkPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);

        if (Physics.Raycast(walkPoint, -transform.up, 2f, whatIsGround))
            walkPointSet = true;
    }

    private void ChasePlayer()
    {
        agent.SetDestination(player.position);
    }

    private void AttackPlayer()
    {
        //Make sure enemy doesn't move
        agent.SetDestination(transform.position);

        transform.LookAt(player);

        if (!alreadyAttacked)
        {
            shoot();//shoot
            alreadyAttacked = true;
            Invoke(nameof(ResetAttack), timeBetweenAttacks);
        }
    }
    private void ResetAttack()
    {
        alreadyAttacked = false;
    }

    public void TakeDamage(int damage)
    {
        health -= damage;

        if (health <= 0) Invoke(nameof(DestroyEnemy), 0.5f);
    }
    private void DestroyEnemy()
    {
        Destroy(gameObject);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, sightRange);
    }


    private void shoot()
    {
        ///Attack code here
        //Rigidbody rb = Instantiate(projectile, AttackPoint.transform.position, Quaternion.identity).GetComponent<Rigidbody>();
        //rb.AddForce(transform.TransformDirection(Vector3.down) * 32f, ForceMode.Impulse);
        //rb.AddForce(transform.up * 8f, ForceMode.Impulse);
        ///End of attack code
        int layerMask = 1 << 8;
        layerMask = ~layerMask;
        Vector3 targetpoint;
        RaycastHit hit;
        if (Physics.Raycast(AttackPoint.transform.position, AttackPoint.transform.TransformDirection(Vector3.forward), out hit, Mathf.Infinity, layerMask))
        {
            targetpoint = hit.point;
            Debug.DrawRay(AttackPoint.transform.position, AttackPoint.transform.TransformDirection(Vector3.forward) * hit.distance, Color.yellow, 2f, false);

            Debug.Log("Did Hit" + hit.distance);
        }
        else
        {
            Debug.DrawRay(AttackPoint.transform.position, AttackPoint.transform.TransformDirection(Vector3.forward) * 1000, Color.white, 2f, false);
            Debug.Log("Did not Hit");
            targetpoint = hit.point;
        }

        Vector3 directionwithoutspread = targetpoint - attackpoint.position;

        float x = Random.Range(-spread, spread);
        float y = Random.Range(-spread, spread);


        Vector3 dirrectionwithspread = directionwithoutspread + new Vector3(x, y, 0);

        GameObject currentbullet = Instantiate(projectile, attackpoint.position, Quaternion.identity);

        currentbullet.transform.forward = dirrectionwithspread.normalized;


        currentbullet.GetComponent<Rigidbody>().AddForce(dirrectionwithspread.normalized * 32, ForceMode.Impulse);

        //currentbullet.GetComponent<Rigidbody>().AddForce(fpscam.transform.up * upwardForce, ForceMode.Impulse);
        Destroy(currentbullet, 2f);
    }


}
