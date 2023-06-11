using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class trigger : MonoBehaviour
{
    public bool triggered = false;
    public bool press_key = false;
    public inventory inv;
    public task_manager task;
    public string pop_up;
    public string item;
    public bool wait;
    public float time;
    public float timer;
    // Start is called before the first frame update
    void Start()
    {
        this.gameObject.SetActive(false);
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
                if (wait)
                {
                    if(timer >= time)
                    {
                        triggered = true;
                        task.set_message("");
                    }
                }
                else if (!wait)
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
                task.set_message(pop_up);
                if (Input.GetKeyDown(KeyCode.E))
                {
                    timer = 0;
                }
                if (Input.GetKey(KeyCode.E))
                {
                    if (wait)
                    {
                        if (timer >= time)
                        {
                            triggered = true;
                            task.set_message("");
                        }
                    }
                    else if (!wait)
                    {
                        triggered = true;
                        task.set_message("");
                    }
                }
                else if (Input.GetKeyUp(KeyCode.E))
                {
                    timer = 0;
                }
            }
        }
    }


}
