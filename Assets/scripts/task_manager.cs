using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class task_manager : MonoBehaviour
{
    public Text message_text;
    public task_vars tasks;
    // Start is called before the first frame update
    void Start()
    {
        tasks = new save_load().loadt();
        set_message("");
    }

    public void set_message(string text)
    {
        tasks.message = text;
        message_text.text = tasks.message;
        new save_load().savet(tasks);
    }

    // Update is called once per frame
    void Update()
    {
        //message_text.text = tasks.message;
    }
}
