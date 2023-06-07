using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class enemy_stats
{
    public float reload_time;
    public int max_ammo;
    public int health;
    public int damage;
    public float sightRange;
    public float attackRange;
    public float bulletspeed;
    public float timeBetweenAttacks;
    public float walkPointRange;
    public float speed;
    public GameObject drop;
    public drop_stats dstats;
}
