using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;

[System.Serializable]
public class tasker : MonoBehaviour
{
    public task_manager manager;
    public enemy_manager enemy_manager;
    public bool complited = false;
    //public bool auto_give = false;
    public task[] tasks;
    public GameObject ui;
    public Text prog_ui;
    public face_task compas;

    // Start is called before the first frame update
    void Start()
    {
        manager = GameObject.Find("charecter").GetComponent<task_manager>();
        enemy_manager = GameObject.Find("enemies").GetComponent<enemy_manager>();
        ui = GameObject.FindGameObjectsWithTag("ui")[0].gameObject;
        prog_ui = ui.transform.Find("tasks/progress/value").gameObject.GetComponent<Text>();
        if (enemy_manager.kills != manager.task_var.curr_kills) { enemy_manager.kills = manager.task_var.curr_kills; }
    }

    // Update is called once per frame
    void Update()
    {
        foreach (var task in tasks)
        {
            if (task.complited != true)
            {
                if (task.type == task.types.trigger && System.Array.IndexOf(tasks, task) == manager.task_var.curr_task)
                {
                    manager.set_task(task.name, task.description);
                    task.trigger.SetActive(true);
                    if (task.trigger.GetComponent<trigger>().triggered == true)
                    {
                        manager.task_var.curr_task++;
                        task.complited = true;
                        Destroy(task.trigger.gameObject);
                    }
                    prog_ui.text = ((int)Vector3.Distance(manager.transform.position, task.trigger.transform.position)/2).ToString() + " m";
                    try
                    {
                        compas = GameObject.FindGameObjectsWithTag("compas")[0].GetComponent<face_task>();
                        compas.obj = task.trigger.transform;
                    }
                    catch { }
                }
                else if (task.type == task.types.kill && System.Array.IndexOf(tasks, task) == manager.task_var.curr_task)
                {
                    if (enemy_manager.kills < 0)
                    {
                        enemy_manager.kills = task.kills;
                    }
                    if (task.spawn_on_player)
                    {
                        enemy_manager.force_spawn(manager.gameObject.transform.position, task.kills);
                        task.spawn_on_player = false;
                    }
                    manager.task_var.curr_kills = enemy_manager.kills;
                    manager.set_task(task.name, task.description);
                    if (enemy_manager.kills == 0)
                    {
                        task.complited = true;
                        manager.task_var.curr_task++;
                        manager.task_var.curr_kills = -1;
                    }
                    prog_ui.text = (enemy_manager.kills).ToString();

                    try
                    {
                        enemy_manager.set_stats();
                        compas = GameObject.FindGameObjectsWithTag("compas")[0].GetComponent<face_task>();
                        //var random_enemy = (int)Random.Range(0, enemy_manager.enemies.Length - 1);
                        //Debug.Log(random_enemy);
                        var min = enemy_manager.enemies[0].transform;
                        var chr = manager.gameObject.transform.position;
                        foreach (var enemy in enemy_manager.enemies)
                        {
                            if (Vector3.Distance(chr, min.position) > Vector3.Distance(chr, enemy.transform.position))
                            {
                                min = enemy.transform;
                            }
                        }
                    
                        compas.obj = min;

                    }catch { }
                }
                else if (task.type == task.types.pick_up && System.Array.IndexOf(tasks, task) == manager.task_var.curr_task)
                {
                    manager.set_task(task.name, task.description);
                    task.trigger.SetActive(true);
                    if (task.trigger.GetComponent<trigger>().triggered == true && task.trigger.GetComponent<trigger>().item == task.item)
                    {
                        manager.task_var.curr_task++;
                        task.complited = true;
                        Destroy(task.trigger.gameObject);
                    }
                    prog_ui.text = ((int)Vector3.Distance(manager.transform.position, task.trigger.transform.position)/2).ToString()+" m";
                    try
                    {
                        compas = GameObject.FindGameObjectsWithTag("compas")[0].GetComponent<face_task>();
                        compas.obj = task.trigger.transform;
                    }
                    catch { }
                }

            }
            else if (System.Array.IndexOf(tasks, task) <= manager.task_var.curr_task)
            {
                task.complited = true;
            }
        }

        if (manager.task_var.curr_task > (tasks.Length -1))
        {
            complited = true;
            manager.get_task();
        }
    }
}
