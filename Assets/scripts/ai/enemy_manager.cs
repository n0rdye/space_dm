using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class enemy_manager : MonoBehaviour
{
    public Transform player;
    public map_lvl lvl_var;
    public Transform[] spawns;
    public GameObject[] enemies;
    public GameObject nenemy_pref;
    public int kills = -1;
    public float map_enemies;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("charecter").transform;


        lvl_var = new save_load().loadmap(SceneManager.GetActiveScene().name);
        enemies = GameObject.FindGameObjectsWithTag("enemy");

        if (lvl_var.max_enemies == -1) lvl_var.max_enemies = map_enemies;

        if (lvl_var.past == false)
        {
            lvl_var.enemy_lvl += new save_load().load().player_lvl;
            lvl_var.past = true;
            new save_load().savemap(lvl_var);
        }
        foreach (var item in enemies)
        {
            var stats = item.GetComponent<enemy>().stats;
            stats.damage += (int)lvl_var.enemy_lvl;
            stats.health += (int)lvl_var.enemy_lvl;
            stats.reload_time -= (lvl_var.enemy_lvl / 40);
            stats.speed += (lvl_var.enemy_lvl / 20);
        }
        spawn();
        save();
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void spawn()
    {
        lvl_var = new save_load().loadmap(SceneManager.GetActiveScene().name);

        try
        {
            if (lvl_var.enemies_pos.Length > 0)
            {
                for (int i = 0; i <= lvl_var.enemies_pos.Length -1; i++)
                {
                    try
                    {
                        var tmp_enemy = Instantiate(nenemy_pref, lvl_var.enemies_pos[i], Quaternion.identity);
                        tmp_enemy.transform.parent = transform;
                    }
                    catch
                    {
                        var tmp_enemy = Instantiate(nenemy_pref, spawns[(int)Random.Range(0, spawns.Length)].position, Quaternion.identity);
                        tmp_enemy.transform.parent = transform;
                    }
                }
            }
            else
            {
                for (int i = 0; i <= lvl_var.max_enemies - 1; i++)
                {
                    var tmp_enemy = Instantiate(nenemy_pref, spawns[(int)Random.Range(0, spawns.Length)].position, Quaternion.identity);
                    tmp_enemy.transform.parent = transform;
                }
            }
        }
        catch (System.NullReferenceException)
        {
            lvl_var = new save_load().loadmap(SceneManager.GetActiveScene().name);
            spawn();
        }
    }

    public void force_spawn(Vector3 pos,int count)
    {
        lvl_var = new save_load().loadmap(SceneManager.GetActiveScene().name);

        for (int i = 0; i <= count-1; i++)
        {
            var tmp_enemy = Instantiate(nenemy_pref, new Vector3(pos.x * rendom_pos(0.5f,2),0, pos.z * rendom_pos(0.5f, 2)), Quaternion.identity);
            tmp_enemy.transform.parent = transform;
        }

        save();

    }

    public float rendom_pos(float min, float max)
    {
        float rpos = 0;
        while (true) {
            rpos = Random.Range(-max, max);
            if (rpos < 0)
            {
                if (rpos < -min)
                {
                    break;
                }
            }
            else if (rpos > 0)
            {
                if (rpos > min)
                {
                    break;
                }
            }
        }
        return rpos;
    }

    public void set_stats( )
    {
        try
        {
            enemies = GameObject.FindGameObjectsWithTag("enemy");
            //Debug.Log(lvl_var.enemy_lvl);

            foreach (var item in enemies)
            {
                var stats = item.GetComponent<enemy>().stats;
                stats.damage -= (int)lvl_var.enemy_lvl;
                stats.health -= (int)lvl_var.enemy_lvl;
                stats.reload_time += (lvl_var.enemy_lvl / 80);
                stats.speed -= (lvl_var.enemy_lvl / 90);
            }
            lvl_var = new save_load().loadmap(SceneManager.GetActiveScene().name);
            foreach (var item in enemies)
            {
                var stats = item.GetComponent<enemy>().stats;
                stats.damage += (int)lvl_var.enemy_lvl;
                stats.health += (int)lvl_var.enemy_lvl;
                stats.reload_time -= (lvl_var.enemy_lvl / 80);
                stats.speed += (lvl_var.enemy_lvl / 50);
            }
        }
        catch
        {
            
        }
    }

    public void save()
    {
        enemies = GameObject.FindGameObjectsWithTag("enemy");

        Vector3[] transArray = new Vector3[enemies.Length];

        for (int i = 0; i != enemies.Length; i++)
        {
            transArray[i] = enemies[i].transform.position;
        }
        lvl_var.enemies_pos = transArray;
        //Debug.Log(enemies.Length-1);
        new save_load().savemap(lvl_var);
    }

    public void hit(GameObject main)
    {
        enemies = GameObject.FindGameObjectsWithTag("enemy");
        foreach (var item in enemies)
        {
                //Debug.Log(Vector3.Distance(main.transform.position, item.transform.position));
            if (Vector3.Distance(main.transform.position, item.transform.position) <= 15 && item.name != main.name)
            {
                //Debug.Log(item.gameObject.name);
                var script = item.gameObject.GetComponent<enemy>();
                script.last_seen = player;
                //Debug.Log("em hit");
            }
        }
    }

}
