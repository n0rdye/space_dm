using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

[System.Serializable]
public class vars
{
    //public string savefile = new save_load().get_path() + "/vars.json";
    //public string weapon_savefile = new save_load().get_path() + "/weapon";
    public float dash_time = 4.0f;
    public float speed = 6f;
    public int health = 100;
    public int max_health = 100;

    public int aim = 2;

    public string weapon = "none";
    public string[] weapons = { "pistol","smg","shotgun","rifle","sniper","lmg"};

    public int materials = 0;

    //public float pd = 10;
    //public int pma = 12;
    //public float prs = 2;
    //
    //public float smd = 6;
    //public int smma = 24;
    //public float smrs = 3;
    //
    //public float sd = 40;
    //public int sma = 6;
    //public float srs = 4;
    //
    //public float rd = 12;
    //public int rma = 30;
    //public float rrs = 4;
    //
    //public float snd = 80;
    //public int snma = 4;
    //public float snrs = 4;
    //
    //public float lmgd = 10;
    //public int lmgma = 40;
    //public float lmgrs = 5;
}
