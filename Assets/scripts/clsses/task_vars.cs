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
    public int curr_task = 0;
    public int curr_grand_task = 0;
    //public task1 grand_task;
    //public task task;
}
