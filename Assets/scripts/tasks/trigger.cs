using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class trigger : MonoBehaviour
{
    public bool triggered = false;
    public bool press_key = false;
    private inventory inv;
    private task_manager task;
    public string pop_up;
    public string item;
    public float time;
    public float timer;
    public string key_button;
    // Start is called before the first frame update
    void Start()
    {
        this.gameObject.SetActive(false);
        pop_up = pop_up.Replace("/n", " \n ");
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "player")
        {
            //triggered = false;
            task.set_message("");
        }
    }


    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "player" && !triggered)
        {
            timer = 0;
            inv = other.gameObject.GetComponent<inventory>();
            task = inv.gameObject.GetComponent<task_manager>();
            if (!press_key)
            {
                if (pop_up != "") { task.set_message(pop_up); }
                if (time == 0)
                {
                    if(timer >= time)
                    {
                        triggered = true;
                        task.set_message("");
                    }
                }
                else if (time != 0)
                {
                    triggered = true;
                    task.set_message("");
                }
            }
        }
    }

    void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "player" && !triggered)
        {
            timer += Time.deltaTime;
            if (press_key)
            {
                if (pop_up != "") { task.set_message(pop_up); }
                if (Input.GetKeyDown(key_button))
                {
                    timer = 0;
                }
                if (Input.GetKey(key_button))
                {
                    if (time == 0)
                    {
                        if (timer >= time)
                        {
                            triggered = true;
                            task.set_message("");
                        }
                    }
                    else if (time != 0)
                    {
                        triggered = true;
                        task.set_message("");
                    }
                }
                else if (Input.GetKeyUp(key_button))
                {
                    timer = 0;
                }
            }
        }
    }


}
