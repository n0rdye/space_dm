using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Collections;
using System.Collections.Generic;

public class save_load : MonoBehaviour
{
    //public string savefile;

    public void Start()
    {
        //savefile = Application.persistentDataPath + "/vars.json";
    }

    public vars load()
    {
        try
        {
            string savefile = Application.persistentDataPath + "/vars.json";
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
        string savefile = Application.persistentDataPath + "/vars.json";
        string jsonString = JsonUtility.ToJson(input);
        File.WriteAllText(savefile, jsonString);
        //Debug.Log("saved");
    }


    public void savew(string name, weapon input)
    {
        string savefile = Application.persistentDataPath + "/weapon/" + name + ".json";
        string jsonString = JsonUtility.ToJson(input);
        File.WriteAllText(savefile, jsonString);
        //Debug.Log("weapon saved");
    }

    public void savewn(int cur_ammo,int max_ammo, int inv_ammo, int damage, float reload_speed,float firerate, bool unlock,string name)
    {
        string savefile = Application.persistentDataPath + "/weapon/"+name+".json";
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
    //    string savefile = Application.persistentDataPath + "/weapon/" + name + ".json";
    //    weapon wp = loadw(name);
    //    wp.inv_ammo += ammo;
    //    string jsonString = JsonUtility.ToJson(wp);
    //    File.WriteAllText(savefile, jsonString);
    //    //Debug.Log("weapon saved");
    //}

    public void saveup(upgrade_vars input)
    {
        string savefile = Application.persistentDataPath + "/upgrade_vars.json";
        string jsonString = JsonUtility.ToJson(input);
        File.WriteAllText(savefile, jsonString);
        //Debug.Log("weapon saved");
    }

    public upgrade_vars loadup()
    {
        try
        {
            string savefile = Application.persistentDataPath + "/upgrade_vars.json";
            string varfile = File.ReadAllText(savefile);
            upgrade_vars outp = JsonUtility.FromJson<upgrade_vars>(varfile);
            //Debug.Log("weapon loaded");
            return outp;
        }
        catch (FileNotFoundException)
        {
            saveup(new upgrade_vars());
            //Debug.Log("weapon loaded");
            return new upgrade_vars();
        }
    }

    public void savet(task_vars input)
    {
        string savefile = Application.persistentDataPath + "/tasks.json";
        string jsonString = JsonUtility.ToJson(input);
        File.WriteAllText(savefile, jsonString);
        //Debug.Log("weapon saved");
    }

    public task_vars loadt()
    {
        try
        {
            string savefile = Application.persistentDataPath + "/tasks.json";
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
            string savefile = Application.persistentDataPath + "/weapon/" + name + ".json";
            string varfile = File.ReadAllText(savefile);
            weapon outp = JsonUtility.FromJson<weapon>(varfile);
            //Debug.Log("weapon loaded");
            return outp;
        }
        catch (FileNotFoundException)
        {
            wrestat();
            string savefile = Application.persistentDataPath + "/weapon/" + name + ".json";
            string varfile = File.ReadAllText(savefile);
            weapon outp = JsonUtility.FromJson<weapon>(varfile);
            //Debug.Log("weapon loaded");
            return outp;
        }
        catch (DirectoryNotFoundException)
        {
            var dir = Application.persistentDataPath + "/weapon/";
            var folder = Directory.CreateDirectory(dir);
            wrestat();
            string savefile = Application.persistentDataPath + "/weapon/" + name + ".json";
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
}
