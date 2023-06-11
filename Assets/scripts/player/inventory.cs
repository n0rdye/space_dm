using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class inventory : MonoBehaviour
{
    public vars var;
    public map_lvl lvl_var;
    //public shooting shot;
    //public weapon weapon;
    public GameObject[] weapons;
    public Canvas tab_menu;

    public Text health_text;
    public Text dash_text;
    public Text lvl_text;
    public Text[] player_text;
    public Text materials_text;
    public bool canswitch = true;
    public GameObject compas;
    public bool menu_active;

    // Start is called before the first frame update
    void Start()
    {
        compas = GameObject.FindGameObjectsWithTag("compas")[0];
        compas.SetActive(false);

        load();
        set_weapon(var.weapon);
        Cursor.visible = false;
        if (lvl_var.player_position != new Vector3(0, 0, 0))
        {
            transform.position = lvl_var.player_position;
        }
        var.current_map = SceneManager.GetActiveScene();
    }

    public void load()
    {
        var = new save_load().load();
        lvl_var = new save_load().loadmap(SceneManager.GetActiveScene().name);
        //shot = GetComponent<shooting>();
        //weapon = new save_load().loadw(current);
    }

    public void save()
    {
        lvl_var.player_position = this.gameObject.transform.position;
        new save_load().savemap(lvl_var);
        new save_load().save(var);
    }

    // Update is called once per frame
    void Update()
    {
        health_text.text = var.health.ToString();
        player_text[0].text = var.max_health.ToString();
        player_text[1].text = var.dash_time.ToString();
        materials_text.text = var.materials.ToString();
        lvl_text.text = Mathf.Round(lvl_var.enemy_lvl).ToString();
        if (var.health > var.max_health)
        {
            var.health = var.max_health;
            save();
        }

        if (Input.GetKeyDown(KeyCode.Alpha1) && canswitch)
        {
            set_weapon("pistol");
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2) && canswitch)
        {
            set_weapon("smg");
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3) && canswitch)
        {
            set_weapon("shotgun");
        }
        else if (Input.GetKeyDown(KeyCode.Alpha4) && canswitch)
        {
            set_weapon("rifle");
        }
        else if (Input.GetKeyDown(KeyCode.Alpha5) && canswitch)
        {
            set_weapon("sniper");
        }
        else if (Input.GetKeyDown(KeyCode.Alpha6) && canswitch)
        {
            set_weapon("lmg");
        }
        else if (var.weapon == "none")
        {
            set_weapon("none");
        }


        if (Input.GetKeyDown(KeyCode.Escape) && !menu_active)
        {
            menu(true);
            menu_active = true;
        }
        else if (Input.GetKeyDown(KeyCode.Escape) && menu_active)
        {
            menu(false);
            menu_active = false;
        }

        if (Input.GetKeyDown(KeyCode.Tab))
        {
            tab_menu.gameObject.SetActive(true);
            foreach (var item in weapons)
            {
                if (item.activeSelf)
                {
                    item.GetComponent<shooting>().canshoot = false;
                    compas.SetActive(true);
                }
            }
            Time.timeScale = 0.6f;
        }
        else if (Input.GetKeyUp(KeyCode.Tab))
        {
            tab_menu.gameObject.SetActive(false);
            foreach (var item in weapons)
            {
                if (item.activeSelf)
                {
                    item.GetComponent<shooting>().canshoot = true;
                    compas.SetActive(false);
                }
            }
            Time.timeScale = 1.0f;
        }

    }

    public void menu(bool stop)
    {
        var chr = this.gameObject.GetComponent<player_controler>();
        if (stop)
        {
            Cursor.visible = true;

            foreach (var item in weapons)
            {
                if (item.activeSelf)
                {
                    item.GetComponent<shooting>().canshoot = false;
                    compas.SetActive(false);
                }
            }
            var ui = tab_menu.transform.parent.transform;
            var uis = new Transform[ui.childCount];
            for (int i = 0; i < ui.childCount; i++)
            {
                uis[i] = ui.GetChild(i);
                if (uis[i].gameObject.name != "menu" && uis[i].gameObject.name != "EventSystem")
                {
                    uis[i].gameObject.SetActive(false);
                }
                else
                {
                    uis[i].gameObject.SetActive(true);
                }
            }
            chr.enabled = false;
            Time.timeScale = 0f;
        }
        else if (!stop)
        {
            Cursor.visible = false;
            foreach (var item in weapons)
            {
                if (item.activeSelf)
                {
                    item.GetComponent<shooting>().canshoot = true;
                    compas.SetActive(false);
                }
            }
            var ui = tab_menu.transform.parent.transform;
            var uis = new Transform[ui.childCount];
            for (int i = 0; i < ui.childCount; i++)
            {
                uis[i] = ui.GetChild(i);
                if (uis[i].gameObject.name != "menu" && uis[i].gameObject.name != "tab")
                {
                    uis[i].gameObject.SetActive(true);
                }
                else
                {
                    uis[i].gameObject.SetActive(false);
                }
            }
            chr.enabled = true;
            Time.timeScale = 1.0f;
        }
    }

    public void tab(bool stop)
    {
        var pl = this.gameObject.GetComponent<player_controler>();
        if (stop)
        {
            Time.timeScale = 0.1f;
            tab_menu.gameObject.SetActive(true);
            foreach (var item in weapons)
            {
                if (item.activeSelf)
                {
                    Cursor.visible = true;
                    item.GetComponent<shooting>().canshoot = false;
                    compas.SetActive(true);
                    canswitch = false;
                    pl.sync = false;
                }
            }
        }
        else if (!stop)
        {
            Time.timeScale = 1.0f;
            tab_menu.gameObject.SetActive(false);
            foreach (var item in weapons)
            {
                if (item.activeSelf)
                {
                    Cursor.visible = false;
                    item.GetComponent<shooting>().canshoot = true;
                    compas.SetActive(false);
                    canswitch = true;
                    pl.sync = true;
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
