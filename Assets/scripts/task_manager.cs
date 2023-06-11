using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class task_manager : MonoBehaviour
{
    public Text message_text;
    public Text task_name;
    public Text task_description;
    public task1[] grand_tasks;
    public task_vars task_var;

    // Start is called before the first frame update
    void Start()
    {
        task_var = new save_load().loadt();
        set_message("");
        get_task();
    }

    public void get_task()
    {
        for (int i = 0; i <= grand_tasks.Length - 1; i++)
        {
            //Debug.Log(System.Array.IndexOf(grand_tasks, grand_tasks[i]));
            if ((System.Array.IndexOf(grand_tasks, grand_tasks[i])) == task_var.curr_grand_task)
            {
                grand_tasks[i].gameObject.SetActive(true);
            }
            else
            {
                grand_tasks[i].gameObject.SetActive(false);
            }
        }
    }

    public void set_message(string text)
    {
        task_var.message = text;
        message_text.text = task_var.message;
        new save_load().savet(task_var);
    }

    public void set_task(string name, string description)
    {
        task_name.text = name;
        task_description.text = description;
    }

    // Update is called once per frame
    void Update()
    {
        if (grand_tasks[task_var.curr_grand_task].complited == true)
        {
            task_var.curr_task = 0;
            task_var.curr_grand_task++;
            get_task();
            new save_load().savet(task_var);
        }
    }
}
