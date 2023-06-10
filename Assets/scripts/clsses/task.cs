using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;

[System.Serializable]
public class task
{
    public bool complited = false;
    public int tasks_left;
    public Scene map;
    public string name;
    public string description;
}
