using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy : MonoBehaviour
{
    private UnityEngine.AI.NavMeshAgent agent;
    private Transform player;

    private bullet bulletstats;
    public float ammo;

    public LayerMask whatIsGround, whatIsPlayer;
    private enemy_manager manager = null;

    //Patroling
    private Vector3 walkPoint;

    //Attacking
    public Transform projectile;
    public Transform spawns;

    private bool walking;
    public bool walkPointSet;
    public bool canshoot = true;
    public bool plspoted;
    bool alreadyAttacked;
    public bool playerInSightRange, playerInAttackRange;
    public Transform last_seen;

    public enemy_stats stats;


    private void Awake()
    {
        last_seen = null;
        ammo = stats.max_ammo;
        canshoot = true;
        player = GameObject.Find("charecter").transform;
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();

        //agent.stoppingDistance = stats.attackRange / 2;
    }

    private void Update()
    {
        if(manager == null)
        {
            manager = this.transform.parent.gameObject.GetComponent<enemy_manager>();
        }
        if (stats.health <= 0)
        {
            StartCoroutine(die());
        }

        agent.speed = stats.speed;


        try
        {
            this.gameObject.transform.Find("health").GetComponent<TextMesh>().text = stats.health.ToString();
        }
        catch { }

        //Check for sight and attack range
        playerInSightRange = Physics.CheckSphere(transform.position, stats.sightRange, whatIsPlayer);
        playerInAttackRange = Physics.CheckSphere(transform.position, stats.attackRange, whatIsPlayer);


        if (!playerInSightRange && !playerInAttackRange) Patroling();
        if (playerInSightRange && !playerInAttackRange) ChasePlayer();
        if (playerInAttackRange && playerInSightRange) AttackPlayer();

        if (!canshoot) return;

        if (ammo <= 0){
            StartCoroutine(reload());
            return;
        }
    }


    // logic
    //private void forgeting()
    //{
    //    //walkPoint = player.transform.position;
    //    plspoted = false;
    //
    //    walking = false;
    //    if (walkPointSet)
    //    {
    //        agent.SetDestination(last_seen.transform.position);
    //        walkPointSet = false;
    //    }
    //    //Patroling();
    //    Vector3 distanceToWalkPoint = transform.position - last_seen.transform.position;
    //
    //    if (distanceToWalkPoint.magnitude < 0.2f) walkPointSet = false; fargot = false;
    //}



    public void hit()
    {
        manager.hit(this.gameObject);
        //share.gameObject.SetActive(true);
        last_seen = player;
        walkPointSet = false;
        //share.gameObject.SetActive(false);
        //playerInAttackRange = Physics.CheckSphere(transform.position, stats.sightRange, whatIsPlayer);

    }
    private void Patroling(){
        plspoted = false;
        if (!walkPointSet){
            walking = false;
            agent.SetDestination(transform.position);
            SearchWalkPoint();
        }

        if (walkPointSet){
            agent.SetDestination(walkPoint);
            walking = true;
        }
        Vector3 distanceToWalkPoint = transform.position - walkPoint;

        if (distanceToWalkPoint.magnitude < 0.3f) 
            walkPointSet = false;
    }
    private void SearchWalkPoint(){
        walking = false;
        float randomZ = Random.Range(-stats.walkPointRange, stats.walkPointRange);
        float randomX = Random.Range(-stats.walkPointRange, stats.walkPointRange);

        
        if (last_seen != null)
        {
            walkPoint = new Vector3(last_seen.position.x, transform.position.y, last_seen.position.z);
            if (Physics.Raycast(walkPoint, -transform.up, 2f, whatIsGround))
            {
                last_seen = null;
                StartCoroutine(pointsearch());
            }
            else
            {
                Debug.Log("set ground layer on floor");
            }


        }
        else
        {
            walkPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);

            if (Physics.Raycast(walkPoint, -transform.up, 2f, whatIsGround))
                StartCoroutine(pointsearch());
            else
                Debug.Log("set ground layer on floor");

        }
    }
    IEnumerator pointsearch(){
        walkPointSet = true;
        //Debug.Log("walk");
        yield return new WaitForSeconds(Random.Range(6, 10));
        walkPointSet = false;
        //Debug.Log("no walk");
    }

    IEnumerator die()
    {
        yield return new WaitForSeconds(0.2f);
        manager.save();
        stats.drop.GetComponent<pick_up>().drop_stats = stats.dstats;
        Instantiate(stats.drop, transform.position, transform.rotation);
        Destroy(gameObject);


        var tmp = manager.lvl_var.enemies_pos;
        RemoveAt(ref tmp, tmp.Length -1);
        manager.lvl_var.enemies_pos = tmp;

        manager.kills--;
        new save_load().savemap(manager.lvl_var);
    }

    public static void RemoveAt<T>(ref T[] arr, int index)
    {
        for (int a = index; a < arr.Length - 1; a++)
        {
            // moving elements downwards, to fill the gap at [index]
            arr[a] = arr[a + 1];
        }
        // finally, let's decrement Array's size by one
        System.Array.Resize(ref arr, arr.Length - 1);
    }

    private void ChasePlayer()
    {
        plspoted = true;
        walking = false;
        agent.SetDestination(new Vector3(player.position.x, player.position.y, player.position.z));
        last_seen = player;
        hit();
    }


    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, stats.attackRange);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, stats.sightRange);
    }

    // weapon
    private void AttackPlayer()
    {

        walking = false;

        Vector3 toPlayer = player.transform.position - transform.position;
        //Debug.Log(Vector3.Distance(player.transform.position, transform.position));
        if (Vector3.Distance(player.transform.position, transform.position) <= (stats.attackRange / 2))
        {
            Vector3 targetPosition = toPlayer.normalized * -1f;
            agent.destination = targetPosition;
        }
        else
        {
            agent.destination = player.transform.position;
        }

        

        //agent.SetDestination(new Vector3(agent.transform.localPosition.x, agent.transform.localPosition.y, agent.transform.localPosition.z - 3));
        transform.LookAt(new Vector3(player.transform.position.x, transform.position.y, player.transform.position.z));

        if (!alreadyAttacked && canshoot)
        {
            bulletstats = projectile.GetComponent<bullet>();
            bulletstats.damage = stats.damage;
            bulletstats.enemy = true;
            ///Attack code here
            //agent.SetDestination(transform.position);
            ammo--;
            Transform bulletshoot = (Transform)Instantiate(projectile, spawns.position, spawns.rotation);
            bulletshoot.GetComponent<Rigidbody>().AddForce(spawns.forward * stats.bulletspeed);
            ///End of attack code

            alreadyAttacked = true;
            Invoke("ResetAttack", stats.timeBetweenAttacks);
        }
    }
    void ResetAttack() { 
        alreadyAttacked = false;
        //agent.SetDestination(new Vector3(transform.localPosition.x - 10, transform.localPosition.y, transform.localPosition.z));

        //agent.SetDestination( new Vector3 (transform.position.x - 10, transform.position.y, transform.position.z));

    }

    IEnumerator reload()
    {
        canshoot = false;
        yield return new WaitForSeconds(stats.reload_time);
        ammo = stats.max_ammo;
        canshoot = true;
    }
}
