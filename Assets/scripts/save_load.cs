using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Collections;
using System.Collections.Generic;

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

    public void saveup(string name, weapon_ups input)
    {
        string savefile = get_path() + "/weapon/"+name+ "_upgrades.json";
        string jsonString = JsonUtility.ToJson(input);
        File.WriteAllText(savefile, jsonString);
        //Debug.Log("weapon saved");
    }

    public void saveupn(string name, float[] reload_speed, float[] damage, float[] max_ammo)
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

    public weapon_ups loadup(string name)
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
            uprestat();
            //Debug.Log("weapon loaded");
            return new weapon_ups();
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

    public void wrestat()
    {

        savewn(0,0,0,0,0,0,true,"none");
        savewn(12,12,12,10,2,4,false,"pistol");
        savewn(24,24,24,6,3,15,false,"smg");
        savewn(6,6,6,40,4,1,false,"shotgun");
        savewn(30,30,30,12,4,8,false,"rifle");
        savewn(4,4,4,80,4,0.5f,false,"sniper");
        savewn(40,40,40,10,5,7,false,"lmg");
    }

    public void uprestat()
    {
        //reload_speed, damage, max_ammo
        //materials , upgrade , current lvl, max lvl, up cost
        saveupn("none", new float[] { 0, 0, 0, 0, 0 }, new float[] { 0, 0, 0, 0, 0 }, new float[] { 0, 0, 0, 0, 0 });
        saveupn("pistol", new float[] { 15, 0.1f, 1, 10, 0.2f }, new float[] { 20, 1, 1, 20, 0.2f }, new float[] { 10, 1, 1, 10, 0.1f });
    }

}
