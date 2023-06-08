using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class upgrade : MonoBehaviour
{
    public upgrade_vars up_vars;
    public Button[] weapon_stats_button;
    public Text[] weapon_stats_text;
    public GameObject ui_canvas;
    public inventory inv;

    void Start()
    {
        up_vars = new save_load().loadup();
        weapon_stats_button[0].onClick.AddListener(ammo_upgrade);
        weapon_stats_button[1].onClick.AddListener(() => rspeed_upgrade());
        weapon_stats_button[2].onClick.AddListener(() => damage_upgrade());
    }

    void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "player")
        {
            weapon_stats_text[0].text = up_vars.max_ammo[0] + " materials \n + " + up_vars.max_ammo[1];
            weapon_stats_text[1].text = up_vars.damage[0] + " materials \n + " + up_vars.damage[1];
            weapon_stats_text[2].text = up_vars.reload_speed[0] +" materials \n - "+ up_vars.reload_speed[1];
            inv = other.gameObject.GetComponent<inventory>();
            if (Input.GetKey(KeyCode.E))
            {
                inv.tab(true);
                ui_canvas.SetActive(true);
            }
            else
            {
                inv.tab(false);
                ui_canvas.SetActive(false);
            }
        }
    }

    void ammo_upgrade()
    {
        Debug.Log("up");
        var shooting = inv.transform.Find("weapons/"+ inv.var.weapon).gameObject.GetComponent<shooting>();
        var weapon = new save_load().loadw(inv.var.weapon);
        inv.var.materials -= (int)up_vars.max_ammo[0];
        weapon.max_ammo += (int)up_vars.max_ammo[1];
        inv.save();
        new save_load().savew(inv.var.weapon, weapon);
        inv.load();
        shooting.load();
    }
    void damage_upgrade()
    {
        var shooting = inv.transform.Find("weapons/" + inv.var.weapon).gameObject.GetComponent<shooting>();
        var weapon = new save_load().loadw(inv.var.weapon);
        inv.var.materials -= (int)up_vars.damage[0];
        weapon.damage += (int)up_vars.damage[1];
        inv.save();
        new save_load().savew(inv.var.weapon, weapon);
        inv.load();
        shooting.load();
    }
    void rspeed_upgrade()
    {
        var shooting = inv.transform.Find("weapons/" + inv.var.weapon).gameObject.GetComponent<shooting>();
        var weapon = new save_load().loadw(inv.var.weapon);
        inv.var.materials -= (int)up_vars.reload_speed[0];
        Debug.Log(weapon.reload_speed);
        weapon.reload_speed = weapon.reload_speed - up_vars.reload_speed[1];
        Debug.Log(weapon.reload_speed);
        inv.save();
        new save_load().savew(inv.var.weapon, weapon);
        inv.load();
        shooting.load();
    }

}
