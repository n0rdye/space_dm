using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class upgrade : MonoBehaviour
{
    public weapon_ups up_vars;
    public Button[] weapon_stats_button;
    public Text[] weapon_stats_text;
    public Text[] weapon_lvl_text;
    public GameObject ui_canvas;
    public inventory inv;
    public task task;

    void Start()
    {
        weapon_stats_button = new Button[3];
        weapon_stats_text = new Text[3];
        weapon_lvl_text = new Text[3];
        //ui_canvas = GameObject.FindGameObjectsWithTag("upgrade_ui")[0];
        weapon_stats_button[0] = ui_canvas.transform.Find("buy_max_ammo").gameObject.GetComponent<Button>();
        weapon_stats_button[1] = ui_canvas.transform.Find("buy_damage").gameObject.GetComponent<Button>();
        weapon_stats_button[2] = ui_canvas.transform.Find("buy_reload_speed").gameObject.GetComponent<Button>();
        weapon_stats_text[0] = ui_canvas.transform.Find("buy_max_ammo/text").gameObject.GetComponent<Text>();
        weapon_stats_text[1] = ui_canvas.transform.Find("buy_damage/text").gameObject.GetComponent<Text>();
        weapon_stats_text[2] = ui_canvas.transform.Find("buy_reload_speed/text").gameObject.GetComponent<Text>();


        weapon_lvl_text[0] = ui_canvas.transform.Find("lvl_max_ammo/text").gameObject.GetComponent<Text>();
        weapon_lvl_text[1] = ui_canvas.transform.Find("lvl_damage/text").gameObject.GetComponent<Text>();
        weapon_lvl_text[2] = ui_canvas.transform.Find("lvl_reload_speed/text").gameObject.GetComponent<Text>();
        weapon_stats_button[0].onClick.AddListener(() => ammo_upgrade());
        weapon_stats_button[1].onClick.AddListener(() => damage_upgrade());
        weapon_stats_button[2].onClick.AddListener(() => rspeed_upgrade());
    }

    void OnTriggerExit()
    {
        task.set_message("");
    }

    void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "player")
        {
            inv = other.gameObject.GetComponent<inventory>();
            task = inv.gameObject.GetComponent<task>();
            if (inv.var.weapon != "none")
            {
                up_vars = new save_load().loadup(inv.var.weapon);
                task.set_message("press e to open upgrade menu");


                weapon_stats_text[0].text = up_vars.max_ammo[0] + " materials \n + " + up_vars.max_ammo[1];
                weapon_stats_text[1].text = up_vars.damage[0] + " materials \n + " + up_vars.damage[1];
                weapon_stats_text[2].text = up_vars.reload_speed[0] + " materials \n " + up_vars.reload_speed[1];

                weapon_lvl_text[0].text = up_vars.max_ammo[2] + " lvl";
                weapon_lvl_text[1].text = up_vars.damage[2] + " lvl";
                weapon_lvl_text[2].text = up_vars.reload_speed[2] + " lvl";

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

                //if (new save_load().loadw(inv.var.weapon).reload_speed <= 0.1f)
                //{
                //    weapon_stats_button[2].gameObject.SetActive(false);
                //}
                if (up_vars.max_ammo[2] >= up_vars.max_ammo[3])
                {
                    weapon_stats_button[0].gameObject.SetActive(false);
                }
                if (up_vars.damage[2] >= up_vars.damage[3])
                {
                    weapon_stats_button[1].gameObject.SetActive(false);
                }
                if (up_vars.reload_speed[2] >= up_vars.reload_speed[3])
                {
                    weapon_stats_button[2].gameObject.SetActive(false);
                }
            }
        }
    }

    void ammo_upgrade()
    {
        var shooting = inv.transform.Find("weapons/"+ inv.var.weapon).gameObject.GetComponent<shooting>();
        var weapon = new save_load().loadw(inv.var.weapon);
        inv.var.materials -= (int)up_vars.max_ammo[0];
        weapon.max_ammo += (int)up_vars.max_ammo[1];
        up_vars.max_ammo[2]++;
        up_vars.max_ammo[0] += up_vars.max_ammo[4];
        up_vars.max_ammo[0] = (float)System.Math.Round(up_vars.max_ammo[0], 2);

        inv.save();
        new save_load().savew(inv.var.weapon, weapon);
        inv.load();
        shooting.load();
        new save_load().saveup(inv.var.weapon,up_vars);
    }
    void damage_upgrade()
    {
        var shooting = inv.transform.Find("weapons/" + inv.var.weapon).gameObject.GetComponent<shooting>();
        var weapon = new save_load().loadw(inv.var.weapon);
        inv.var.materials -= (int)up_vars.damage[0];
        weapon.damage += (int)up_vars.damage[1];
        up_vars.damage[2]++;
        up_vars.damage[0] += up_vars.damage[4];
        up_vars.damage[0] = (float)System.Math.Round(up_vars.damage[0], 2);

        inv.save();
        new save_load().savew(inv.var.weapon, weapon);
        inv.load();
        shooting.load();
        new save_load().saveup(inv.var.weapon,up_vars);
    }
    void rspeed_upgrade()
    {
        var shooting = inv.transform.Find("weapons/" + inv.var.weapon).gameObject.GetComponent<shooting>();
        var weapon = new save_load().loadw(inv.var.weapon);
        inv.var.materials -= (int)up_vars.reload_speed[0];
        weapon.reload_speed -= up_vars.reload_speed[1];
        weapon.reload_speed = (float)System.Math.Round(weapon.reload_speed, 2);
        up_vars.reload_speed[1] = (float)System.Math.Round(up_vars.reload_speed[1], 2);
        up_vars.reload_speed[2]++;
        up_vars.reload_speed[0] += up_vars.reload_speed[4];
        up_vars.reload_speed[0] = (float)System.Math.Round(up_vars.reload_speed[0], 2);

        inv.save();
        new save_load().savew(inv.var.weapon, weapon);
        inv.load();
        shooting.load();
        new save_load().saveup(inv.var.weapon,up_vars);
    }

}
