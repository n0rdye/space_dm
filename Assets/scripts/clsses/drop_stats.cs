using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

[System.Serializable]
public class drop_stats
{
    public int num = 1;
    public enum types { ammo, materials, health, unlock_weapon, lock_weapon };
    public types type;
    public enum weapons { none, pistol, smg, shotgun, rifle, sniper, lmg, all, current };
    public weapons weapon;
}
