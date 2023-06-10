using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;

[System.Serializable]
public class task
{
    public bool complited = false;
    public Scene map;
    public enum types { trigger,kill,pick_up };
    public int kills;
    public string item;
    public GameObject trigger;
    public types type;
    public string name;
    public string description;
}
