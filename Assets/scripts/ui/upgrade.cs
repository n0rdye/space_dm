using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class upgrade : MonoBehaviour
{
    public weapon_ups up_vars;
    public player_ups pl_ups;
    private Button[] weapon_stats_button;
    private Text[] weapon_stats_text;
    private Text[] weapon_lvl_text;

    private Button[] player_stats_button;
    private Text[] player_stats_text;
    private Text[] player_lvl_text;

    public GameObject ui_canvas;
    public GameObject pl_canvas;
    private inventory inv;
    public task_manager task;
    public enemy_manager enemy_manager;

    void Start()
    {
        weapon_stats_button = new Button[3];
        weapon_stats_text = new Text[3];
        weapon_lvl_text = new Text[3];

        player_stats_button = new Button[2];
        player_stats_text = new Text[2];
        player_lvl_text = new Text[2];

        //ui_canvas = GameObject.FindGameObjectsWithTag("upgrade_ui")[0];
        weapon_stats_button[0] = ui_canvas.transform.Find("buy_max_ammo").gameObject.GetComponent<Button>();
        weapon_stats_button[1] = ui_canvas.transform.Find("buy_damage").gameObject.GetComponent<Button>();
        weapon_stats_button[2] = ui_canvas.transform.Find("buy_reload_speed").gameObject.GetComponent<Button>();
        weapon_stats_text[0] = ui_canvas.transform.Find("buy_max_ammo/text").gameObject.GetComponent<Text>();
        weapon_stats_text[1] = ui_canvas.transform.Find("buy_damage/text").gameObject.GetComponent<Text>();
        weapon_stats_text[2] = ui_canvas.transform.Find("buy_reload_speed/text").gameObject.GetComponent<Text>();

        player_stats_button[0] = pl_canvas.transform.Find("buy_dash_time").gameObject.GetComponent<Button>();
        player_stats_button[1] = pl_canvas.transform.Find("buy_health").gameObject.GetComponent<Button>();
        player_stats_text[0] = pl_canvas.transform.Find("buy_dash_time/text").gameObject.GetComponent<Text>();
        player_stats_text[1] = pl_canvas.transform.Find("buy_health/text").gameObject.GetComponent<Text>();



        weapon_lvl_text[0] = ui_canvas.transform.Find("lvl_max_ammo/text").gameObject.GetComponent<Text>();
        weapon_lvl_text[1] = ui_canvas.transform.Find("lvl_damage/text").gameObject.GetComponent<Text>();
        weapon_lvl_text[2] = ui_canvas.transform.Find("lvl_reload_speed/text").gameObject.GetComponent<Text>();

        player_lvl_text[0] = pl_canvas.transform.Find("lvl_dash_time/text").gameObject.GetComponent<Text>();
        player_lvl_text[1] = pl_canvas.transform.Find("lvl_health/text").gameObject.GetComponent<Text>();

        weapon_stats_button[0].onClick.AddListener(() => ammo_upgrade());
        weapon_stats_button[1].onClick.AddListener(() => damage_upgrade());
        weapon_stats_button[2].onClick.AddListener(() => rspeed_upgrade());

        player_stats_button[0].onClick.AddListener(() => dt_upgrade());
        player_stats_button[1].onClick.AddListener(() => health_upgrade());

        try { enemy_manager = GameObject.Find("enemies").gameObject.GetComponent<enemy_manager>(); } catch { }

    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "player")
        {
            task.set_message("");
            inv.tab(false);
            ui_canvas.SetActive(false);
            pl_canvas.SetActive(false);
        }
    }


    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "player")
        {
            inv = other.gameObject.GetComponent<inventory>();
            task = inv.gameObject.GetComponent<task_manager>();
            task.set_message("hold tab to open upgrade menu");
        }
    }

    void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "player")
        {
            if (inv.var.weapon != "none")
            {
                up_vars = new save_load().loadup_w(inv.var.weapon);
                pl_ups = new save_load().loadup_pl();

                if (Input.GetKey(KeyCode.Tab))
                {
                    inv.tab(true);
                    ui_canvas.SetActive(true);
                    pl_canvas.SetActive(true);
                    task.set_message("");
                }
                else 
                {
                    inv.tab(false);
                    ui_canvas.SetActive(false);
                    pl_canvas.SetActive(false);
                    task.set_message("press e to open upgrade menu");
                }


                try
                {
                    weapon_stats_text[0].text = up_vars.max_ammo.cost + " materials \n + " + up_vars.max_ammo.up;
                    weapon_stats_text[1].text = up_vars.damage.cost + " materials \n + " + up_vars.damage.up;
                    weapon_stats_text[2].text = up_vars.reload_speed.cost + " materials \n " + up_vars.reload_speed.up;

                    player_stats_text[0].text = pl_ups.dash_time.cost + " materials \n - " + pl_ups.dash_time.up;
                    player_stats_text[1].text = pl_ups.health.cost + " materials \n + " + pl_ups.health.up;


                    weapon_lvl_text[0].text = up_vars.max_ammo.lvl + " lvl";
                    weapon_lvl_text[1].text = up_vars.damage.lvl + " lvl";
                    weapon_lvl_text[2].text = up_vars.reload_speed.lvl + " lvl";

                    player_lvl_text[0].text = pl_ups.dash_time.lvl + " lvl";
                    player_lvl_text[1].text = pl_ups.health.lvl + " lvl";

                    //if (new save_load().loadw(inv.var.weapon).reload_speed <= 0.1f)
                    //{
                    //    weapon_stats_button[2].gameObject.SetActive(false);
                    //}
                    if (up_vars.max_ammo.lvl >= up_vars.max_ammo.max_lvl)
                    {
                        weapon_stats_button[0].gameObject.SetActive(false);
                    }
                    if (up_vars.damage.lvl >= up_vars.damage.max_lvl)
                    {
                        weapon_stats_button[1].gameObject.SetActive(false);
                    }
                    if (up_vars.reload_speed.lvl >= up_vars.reload_speed.max_lvl)
                    {
                        weapon_stats_button[2].gameObject.SetActive(false);
                    }

                    if (pl_ups.dash_time.lvl >= pl_ups.dash_time.max_lvl)
                    {
                        player_stats_button[0].gameObject.SetActive(false);
                    }
                    if (pl_ups.health.lvl >= pl_ups.health.max_lvl)
                    {
                        player_stats_button[1].gameObject.SetActive(false);
                    }
                }
                catch { }
            }
        }
    }

    void ammo_upgrade()
    {
        var shooting = inv.transform.Find("weapons/"+ inv.var.weapon).gameObject.GetComponent<shooting>();
        var weapon = new save_load().loadw(inv.var.weapon);
        inv.var.materials -= up_vars.max_ammo.cost;
        weapon.max_ammo += (int)up_vars.max_ammo.up;
        up_vars.max_ammo.lvl++;
        up_vars.max_ammo.cost += up_vars.max_ammo.up_cost;

        inv.lvl_var.enemy_lvl += 0.2f;
        inv.var.player_lvl += 0.2f;
        inv.lvl_var.max_enemies += 0.03f;

        inv.save();
        new save_load().savew(inv.var.weapon, weapon);
        inv.load();
        shooting.load();
        new save_load().saveup_w(inv.var.weapon,up_vars);
        try { enemy_manager.set_stats(); enemy_manager.save(); } catch { }
    }
    void damage_upgrade()
    {
        var shooting = inv.transform.Find("weapons/" + inv.var.weapon).gameObject.GetComponent<shooting>();
        var weapon = new save_load().loadw(inv.var.weapon);
        inv.var.materials -= up_vars.damage.cost;
        weapon.damage += (int)up_vars.damage.up;
        up_vars.damage.lvl++;
        up_vars.damage.cost += up_vars.damage.up_cost;

        inv.lvl_var.enemy_lvl += 0.2f;
        inv.var.player_lvl += 0.2f;
        inv.lvl_var.max_enemies += 0.03f;

        inv.save();
        new save_load().savew(inv.var.weapon, weapon);
        inv.load();
        shooting.load();
        new save_load().saveup_w(inv.var.weapon,up_vars);
        try { enemy_manager.set_stats(); enemy_manager.save(); } catch { }
    }
    void rspeed_upgrade()
    {
        var shooting = inv.transform.Find("weapons/" + inv.var.weapon).gameObject.GetComponent<shooting>();
        var weapon = new save_load().loadw(inv.var.weapon);
        inv.var.materials -= up_vars.reload_speed.cost;
        weapon.reload_speed -= up_vars.reload_speed.up;
        up_vars.reload_speed.lvl++;
        up_vars.reload_speed.cost += up_vars.reload_speed.max_lvl;

        weapon.reload_speed = (float)System.Math.Round(weapon.reload_speed, 2);
        //up_vars.reload_speed.up = (float)System.Math.Round(up_vars.reload_speed.up, 2);

        inv.lvl_var.enemy_lvl += 0.2f;
        inv.var.player_lvl += 0.2f;
        inv.lvl_var.max_enemies += 0.03f;

        inv.save();
        new save_load().savew(inv.var.weapon, weapon);
        inv.load();
        shooting.load();
        new save_load().saveup_w(inv.var.weapon,up_vars);
        try { enemy_manager.set_stats(); enemy_manager.save(); } catch { }
    }

    //player
    void dt_upgrade()
    {
        inv.var.materials -= (int)pl_ups.dash_time.cost;
        inv.var.dash_time -= pl_ups.dash_time.up;
        pl_ups.dash_time.lvl++;
        pl_ups.dash_time.cost += pl_ups.dash_time.up_cost;

        inv.var.dash_time = (float)System.Math.Round(inv.var.dash_time, 2);

        inv.lvl_var.enemy_lvl += 0.2f;
        inv.var.player_lvl += 0.2f;
        inv.lvl_var.max_enemies += 0.03f;

        inv.save();
        new save_load().saveup_pl(pl_ups);
        inv.load();
        try { enemy_manager.set_stats(); enemy_manager.save(); } catch { }
    }

    void health_upgrade()
    {
        inv.var.materials -= pl_ups.health.cost;
        inv.var.max_health += (int)pl_ups.health.up;
        pl_ups.health.lvl++;
        pl_ups.health.cost += pl_ups.health.up_cost;

        inv.lvl_var.enemy_lvl += 0.2f;
        inv.var.player_lvl += 0.2f;
        inv.lvl_var.max_enemies += 0.03f;

        inv.save();
        new save_load().saveup_pl(pl_ups);
        inv.load();
        try { enemy_manager.set_stats(); enemy_manager.save(); } catch { }
    }

}
