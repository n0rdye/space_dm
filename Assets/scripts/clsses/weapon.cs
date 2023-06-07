using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class weapon
{
    public int cur_ammo = 10;
    public int max_ammo = 10;
    public int inv_ammo = 10;
    public int damage = 10;
    public float reload_speed = 1f;
    public float firerate = 1f;
    public bool unlock = false;
}
