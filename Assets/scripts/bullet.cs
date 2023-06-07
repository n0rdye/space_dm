using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bullet : MonoBehaviour
{
    public int damage;
    public float deltime = 2f;
    public bool enemy = false;


    void Update()
    {
        Destroy(gameObject, deltime);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "enemy")
        {
            var enemy_stats = collision.gameObject.GetComponent<enemy>(); ;
            enemy_stats.stats.health -= damage;
            enemy_stats.hit();
            Destroy(gameObject);
        }
        else if (collision.gameObject.tag == "player" && enemy)
        {
            var inv = collision.gameObject.GetComponent<inventory>();
            var stats = new save_load().load();
            stats.health -= damage;
            save_load sl = new save_load();
            sl.save(stats);
            //Debug.Log(stats.health);
            inv.load();
            Destroy(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
