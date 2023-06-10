using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

[System.Serializable]
public class task_vars
{
    public string message = " ";
    public task[] tasks;
    public int tasks_left;
    public int curr_left;
    public task current_task;
}
