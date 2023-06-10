using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.SceneManagement;

public class save_load : MonoBehaviour
{
    public void Start()
    {
    }

    public string get_path()
    {
        var path = Application.persistentDataPath;
        return path;
    }

    public vars load()
    {
        try
        {
            string savefile = get_path() + "/vars.json";
            string varfile = File.ReadAllText(savefile);
            vars outp = JsonUtility.FromJson<vars>(varfile);
            //Debug.Log("loaded");
            return outp;
        }
        catch (FileNotFoundException)
        {
            save(new vars());
            return new vars();
        }
    }

    public void save(vars input)
    {
        string savefile = get_path() + "/vars.json";
        string jsonString = JsonUtility.ToJson(input);
        File.WriteAllText(savefile, jsonString);
        //Debug.Log("saved");
    }


    public void savew(string name, weapon input)
    {
        string savefile = get_path() + "/weapon/" + name + ".json";
        string jsonString = JsonUtility.ToJson(input);
        File.WriteAllText(savefile, jsonString);
        //Debug.Log("weapon saved");
    }

    public void savewn(int cur_ammo,int max_ammo, int inv_ammo, int damage, float reload_speed,float firerate, bool unlock,string name)
    {
        string savefile = get_path() + "/weapon/"+name+".json";
        weapon wp = new weapon();
        wp.damage = damage;
        wp.max_ammo = max_ammo;
        wp.inv_ammo = inv_ammo;
        wp.cur_ammo = cur_ammo;
        wp.reload_speed = reload_speed;
        wp.firerate = firerate;
        wp.unlock = unlock;
        string jsonString = JsonUtility.ToJson(wp);
        File.WriteAllText(savefile, jsonString);
        //Debug.Log("weapon saved");
    }
    //public void add_ammo(string name, int ammo)
    //{
    //    string savefile = get_path() + "/weapon/" + name + ".json";
    //    weapon wp = loadw(name);
    //    wp.inv_ammo += ammo;
    //    string jsonString = JsonUtility.ToJson(wp);
    //    File.WriteAllText(savefile, jsonString);
    //    //Debug.Log("weapon saved");
    //}

    public void saveup_w(string name, weapon_ups input)
    {
        string savefile = get_path() + "/weapon/"+name+ "_upgrades.json";
        string jsonString = JsonUtility.ToJson(input);
        File.WriteAllText(savefile, jsonString);
        //Debug.Log("weapon saved");
    }

    public void saveupn_w(string name, ups reload_speed, ups damage, ups max_ammo)
    {
        string savefile = get_path() + "/weapon/" + name + "_upgrades.json";
        var upgrade = new weapon_ups();
        upgrade.damage = damage;
        upgrade.max_ammo = max_ammo;
        upgrade.reload_speed = reload_speed;
        string jsonString = JsonUtility.ToJson(upgrade);
        File.WriteAllText(savefile, jsonString);
        //Debug.Log("weapon saved");
    }

    public weapon_ups loadup_w(string name)
    {
        try
        {
            string savefile = get_path() + "/weapon/"+name+ "_upgrades.json";
            string varfile = File.ReadAllText(savefile);
            weapon_ups outp = JsonUtility.FromJson<weapon_ups>(varfile);
            //Debug.Log("weapon loaded");
            return outp;
        }
        catch (FileNotFoundException)
        {
            //uprestat();
            uprestat_w(name);
            //Debug.Log("weapon loaded");
            return new weapon_ups();
        }
    }

    public void saveup_pl(player_ups input)
    {
        string savefile = get_path() + "/upgrade.json";
        string jsonString = JsonUtility.ToJson(input);
        File.WriteAllText(savefile, jsonString);
        //Debug.Log("weapon saved");
    }

    public void saveupn_pl( ups dash_time, ups health)
    {
        string savefile = get_path() + "/upgrade.json";
        var upgrade = new player_ups();
        upgrade.health = health;
        upgrade.dash_time = dash_time;
        string jsonString = JsonUtility.ToJson(upgrade);
        File.WriteAllText(savefile, jsonString);
        //Debug.Log("weapon saved");
    }

    public player_ups loadup_pl()
    {
        try
        {
            string savefile = get_path() + "/upgrade.json";
            string varfile = File.ReadAllText(savefile);
            player_ups outp = JsonUtility.FromJson<player_ups>(varfile);
            //Debug.Log("weapon loaded");
            return outp;
        }
        catch (FileNotFoundException)
        {
            uprestat_pl();
            //Debug.Log("weapon loaded");
            return new player_ups();
        }
    }

    public void savet(task_vars input)
    {
        string savefile = get_path() + "/tasks.json";
        string jsonString = JsonUtility.ToJson(input);
        File.WriteAllText(savefile, jsonString);
        //Debug.Log("weapon saved");
    }

    public task_vars loadt()
    {
        try
        {
            string savefile = get_path() + "/tasks.json";
            string varfile = File.ReadAllText(savefile);
            task_vars outp = JsonUtility.FromJson<task_vars>(varfile);
            //Debug.Log("weapon loaded");
            return outp;
        }
        catch (FileNotFoundException)
        {
            savet(new task_vars());
            //Debug.Log("weapon loaded");
            return new task_vars();
        }
    }

    public weapon loadw(string name)
    {
        try
        {
            string savefile = get_path() + "/weapon/" + name + ".json";
            string varfile = File.ReadAllText(savefile);
            weapon outp = JsonUtility.FromJson<weapon>(varfile);
            //Debug.Log("weapon loaded");
            return outp;
        }
        catch (FileNotFoundException)
        {
            wrestat();
            string savefile = get_path() + "/weapon/" + name + ".json";
            string varfile = File.ReadAllText(savefile);
            weapon outp = JsonUtility.FromJson<weapon>(varfile);
            //Debug.Log("weapon loaded");
            return outp;
        }
        catch (DirectoryNotFoundException)
        {
            var dir = get_path() + "/weapon/";
            var folder = Directory.CreateDirectory(dir);
            wrestat();
            string savefile = get_path() + "/weapon/" + name + ".json";
            string varfile = File.ReadAllText(savefile);
            weapon outp = JsonUtility.FromJson<weapon>(varfile);
            //Debug.Log("weapon loaded");
            return outp;
        }
    }

    public void savemap(map_lvl input)
    {
        string savefile = get_path() +"/maps/"+ SceneManager.GetActiveScene().name + ".json";
        string jsonString = JsonUtility.ToJson(input);
        File.WriteAllText(savefile, jsonString);
        //Debug.Log("weapon saved");
    }

    public map_lvl loadmap(string name)
    {
        try
        {
            string savefile = get_path() + "/maps/" + name + ".json";
            string varfile = File.ReadAllText(savefile);
            map_lvl outp = JsonUtility.FromJson<map_lvl>(varfile);
            //Debug.Log("weapon loaded");
            return outp;
        }
        catch (FileNotFoundException)
        {
            savemap(new map_lvl());
            //Debug.Log("weapon loaded");
            return new map_lvl();
        }
        catch (DirectoryNotFoundException)
        {
            var dir = get_path() + "/maps/";
            var folder = Directory.CreateDirectory(dir);
            string savefile = get_path() + "/maps/" + name + ".json";
            savemap(new map_lvl());
            return new map_lvl();
        }
    }

    public void wrestat()
    {
        savewn(0,0,0,0,0,0,true,"none");
        savewn(12,12,12,10,2,4,false,"pistol");
        savewn(24,24,24,4,3,15,false,"smg");
        savewn(6,6,6,40,4,1,false,"shotgun");
        savewn(30,30,30,12,4,8,false,"rifle");
        savewn(4,4,4,80,4,0.5f,false,"sniper");
        savewn(40,40,40,10,5,7,false,"lmg");
    }

    public void uprestat_w(string name)
    {
        //reload_speed, damage, max_ammo
        //materials , upgrade , current lvl, max lvl, up cost
        //saveupn("none", new float[] { 0, 0, 0, 0, 0 }, new float[] { 0, 0, 0, 0, 0 }, new float[] { 0, 0, 0, 0, 0 });
        //saveupn("pistol", new float[] { 15, 0.1f, 1, 10, 0.2f }, new float[] { 20, 1, 1, 20, 0.2f }, new float[] { 10, 1, 1, 10, 0.1f });
        var w_pistol_rs = new ups();
        w_pistol_rs.cost = 15;
        w_pistol_rs.up = 0.1f;
        w_pistol_rs.max_lvl = 10;
        w_pistol_rs.up_cost = 10;
        var w_pistol_dm = new ups();
        w_pistol_dm.cost = 20;
        w_pistol_dm.up = 3;
        w_pistol_dm.max_lvl = 20;
        w_pistol_dm.up_cost = 6;
        var w_pistol_ma = new ups();
        w_pistol_ma.cost = 15;
        w_pistol_ma.up = 2;
        w_pistol_ma.max_lvl = 20;
        w_pistol_ma.up_cost = 5;

        saveupn_w(name,w_pistol_rs,w_pistol_dm,w_pistol_ma);
    }

    public void uprestat_pl()
    {
        Debug.Log("pl stats restart");
        var pl_dt = new ups();
        pl_dt.cost = 25;
        pl_dt.up = 0.4f;
        pl_dt.max_lvl = 10;
        pl_dt.up_cost = 3;
        var pl_hp = new ups();
        pl_hp.cost = 30;
        pl_hp.up = 3;
        pl_hp.max_lvl = 50;
        pl_hp.up_cost = 6;
        saveupn_pl(pl_dt, pl_hp);
    }

}
