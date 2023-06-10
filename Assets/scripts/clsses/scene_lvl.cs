using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

[System.Serializable]
public class map_lvl
{
    public float enemy_lvl = 0;
    public float max_enemies = -1;
    public Vector3[] enemies_pos;
    public Vector3 player_position;
    public bool past = false;
    //public string task;
}
