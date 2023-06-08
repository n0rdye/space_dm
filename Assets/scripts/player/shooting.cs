using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class shooting : MonoBehaviour
{
    public Transform bullet;
    public Transform spawns;

    public vars var;
    public weapon stats;
    const float speed = 4000;
    public float nextfire;
    public bool reloading;
    public bool canshoot = true;
    public Text name_text;
    public Text ammo_text;
    public Text[] stats_text;

    private void Start()
    {
        //new save_load().wrestat();
        load();
    }

    public void load()
    {
        var = new save_load().load();
        var.weapon = gameObject.name;
        stats = new save_load().loadw(var.weapon);
        stats_text[0].text = stats.max_ammo.ToString();
        stats_text[1].text = stats.reload_speed.ToString();
        stats_text[2].text = stats.damage.ToString();
        ammo_text.text = stats.cur_ammo + " / " + stats.inv_ammo;
        name_text.text = var.weapon;
        //Debug.Log("weapon load");
        //load();
    }

    public void save()
    {
        new save_load().savew(var.weapon, stats);
        Debug.Log(stats);
    }

    void Update()
    {
        //stats = new save_load().loadw(var.weapon);
        if (reloading)
        {
            ammo_text.text = "reloading";

        }
        else if (!reloading)
        {
            ammo_text.text = stats.cur_ammo + " / " + stats.inv_ammo;

        }

        if (stats.cur_ammo <= stats.max_ammo - 1 && !reloading && stats.inv_ammo > 0)
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                reloading = true;
                stats.inv_ammo += stats.cur_ammo;
                stats.cur_ammo = 0;
                StartCoroutine(reload());
                return;
            }
        }

        if (stats.inv_ammo > 0 && stats.cur_ammo <= 0 && !reloading)
        {
            reloading = true;
            StartCoroutine(reload());
            return;
        }
        if (stats.cur_ammo <= 0 && stats.inv_ammo <= 0)
        {
            canshoot = false;
            return;
        }

        if (Input.GetKey(KeyCode.Mouse0) && Time.time >= nextfire && canshoot)
        {
            nextfire = Time.time + 1f / stats.firerate;
            shoot();
            new save_load().savew(var.weapon, stats);
        }
    }

    private void OnEnable()
    {
        reloading = false;
    }

    private void OnDisable()
    {
    }

    IEnumerator reload()
    {
        if (reloading)
        {
            canshoot = false;
            yield return new WaitForSeconds(stats.reload_speed);

            if (stats.cur_ammo != stats.max_ammo)
            {
                if (stats.inv_ammo <= 0)
                {
                    Debug.Log("no ammo");
                    reloading = false;
                }
                if (stats.inv_ammo - stats.max_ammo <= 0)
                {
                    stats.cur_ammo = stats.inv_ammo;
                    stats.inv_ammo = 0;
                    reloading = false;
                }
                else if (stats.inv_ammo >= 0)
                {
                    stats.cur_ammo = stats.max_ammo;
                    reloading = false;
                    stats.inv_ammo -= stats.max_ammo;
                }

                else if (stats.inv_ammo <= stats.max_ammo)
                {
                    stats.cur_ammo = stats.inv_ammo;
                    reloading = false;
                    stats.inv_ammo -= stats.max_ammo;
                }

                new save_load().savew(var.weapon, stats);
            }

            reloading = false;
            canshoot = true;
        }
    }

    public void shoot()
    {
        if (canshoot)
        {
            var bstats = bullet.GetComponent<bullet>();
            bstats.damage = stats.damage;
            Transform bulletshoot = (Transform)Instantiate(bullet, spawns.position, spawns.rotation);
            bulletshoot.GetComponent<Rigidbody>().AddForce(spawns.forward * speed);
            stats.cur_ammo--;
        }
    }

    
}
