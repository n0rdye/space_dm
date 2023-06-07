using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;


public class inventory : MonoBehaviour
{
    public vars var;
    //public shooting shot;
    //public weapon weapon;
    public GameObject[] weapons;
    public Canvas tab_menu;

    public Text health_text;
    public Text[] player_text;
    public Text materials_text;

    public bool tab = false;

    // Start is called before the first frame update
    void Start()
    {
        load();
        set_weapon(var.weapon);
    }

    public void load()
    {
        var = new save_load().load();
        //shot = GetComponent<shooting>();
        //weapon = new save_load().loadw(current);
    }

    public void save()
    {
        new save_load().save(var);
    }

    // Update is called once per frame
    void Update()
    {
        health_text.text = var.health.ToString();
        player_text[0].text = var.max_health.ToString();
        player_text[1].text = var.dash_time.ToString();
        materials_text.text = var.materials.ToString();
        if (var.health > 100)
        {
            var.health = 100;
            save();
        }

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            set_weapon("pistol");
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            set_weapon("smg");
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            set_weapon("shotgun");
        }
        else if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            set_weapon("rifle");
        }
        else if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            set_weapon("sniper");
        }
        else if (Input.GetKeyDown(KeyCode.Alpha6))
        {
            set_weapon("lmg");
        }
        else if (var.weapon == "none")
        {
            set_weapon("none");
        }

        if (Input.GetKeyDown(KeyCode.Tab) || tab == true)
        {
            tab_menu.gameObject.SetActive(true);
            foreach (var item in weapons)
            {
                if (item.activeSelf)
                {
                    item.GetComponent<shooting>().canshoot = false;
                }
            }
        }
        else if (Input.GetKeyUp(KeyCode.Tab) || tab == false)
        {
            tab_menu.gameObject.SetActive(false);
            foreach (var item in weapons)
            {
                if (item.activeSelf)
                {
                    item.GetComponent<shooting>().canshoot = true;
                }
            }
        }
    }

    public void set_weapon(string name)
    {
       if (new save_load().loadw(name).unlock)
        {
            var.weapon = name;
            foreach (var item in weapons)
            {
                if (item.name == name)
                {
                    item.SetActive(true);
                    var.weapon = name;
                    item.GetComponent<shooting>().load();
                    new save_load().save(var);
                }
                else
                {
                    item.SetActive(false);
                }
            }
            //weapon = new save_load().loadw(name);
            //shot.damage = weapon.damage;
            //shot.reloadtime = weapon.reload_speed;
            //shot.ammoinv = weapon.inv_ammo;
            //shot.maxammo = weapon.max_ammo;
            //shot.currammo = weapon.max_ammo;
        }
    }
}
