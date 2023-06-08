using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

[System.Serializable]
public class weapon_ups
{
    //materials , upgrade , current lvl, max lvl, up cost
    public float[] reload_speed = new float[] { 10, 0.1f,1,20,2 }; 
    public float[] damage = new float[] { 10, 1f,1,20,2 }; 
    public float[] max_ammo = new float[] { 10, 2f,1,20,2 };
}
