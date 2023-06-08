using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

[System.Serializable]
public class upgrade_vars
{
    public float[] reload_speed = new float[] { 10, -0.1f }; 
    public float[] damage = new float[] { 10, 1f }; 
    public float[] max_ammo = new float[] { 10, 2f };

    public float[] health = new float[] { 10, 3f };
    public float[] dash_time = new float[] { 10, 0.1f };

}
