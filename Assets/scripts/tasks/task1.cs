using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class task1 : MonoBehaviour
{
    public task_manager manager;
    public enemy_manager enemy_manager;
    public int curr_task;
    public int curr_kulls;
    public bool complited = false;
    public task[] tasks;

    // Start is called before the first frame update
    void Start()
    {
        manager = GameObject.Find("charecter").GetComponent<task_manager>();
        enemy_manager = GameObject.Find("enemies").GetComponent<enemy_manager>();

    }

    // Update is called once per frame
    void Update()
    {
        foreach (var task in tasks)
        {
            if (task.complited != true)
            {
                if (task.type == task.types.trigger && System.Array.IndexOf(tasks, task) == curr_task)
                {
                    task.trigger.SetActive(true);
                    if (task.trigger.GetComponent<trigger>().triggered == true)
                    {
                        curr_task++;
                        task.complited = true;
                    }
                }
                else if (task.type == task.types.kill && System.Array.IndexOf(tasks, task) == curr_task)
                {
                    curr_kulls = (curr_kulls == 0)? (int)enemy_manager.lvl_var.enemies : curr_kulls;
                    if ((curr_kulls - (int)enemy_manager.lvl_var.enemies) == task.kills)
                    {
                        curr_task++;
                        task.complited = true;
                    }
                }
                else if (task.type == task.types.pick_up && System.Array.IndexOf(tasks, task) == curr_task)
                {
                    task.trigger.SetActive(true);
                    if (task.trigger.GetComponent<trigger>().triggered == true && task.trigger.GetComponent<trigger>().item == task.item)
                    {
                        curr_task++;
                        task.complited = true;
                    }
                }

            }
        }

        if (curr_task >= (tasks.Length - 1))
        {
            complited = true;
        }
    }
}
