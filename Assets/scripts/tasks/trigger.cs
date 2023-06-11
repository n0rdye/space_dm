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
            inv = other.gameObject.GetComponent<inventory>();
            task = inv.gameObject.GetComponent<task_manager>();
            if (!press_key)
            {
                triggered = true;
                task.set_message("");
            }
        }
    }

    void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "player" && !triggered)
        {
            if (press_key)
            {
                task.set_message(pop_up);
                if (Input.GetKey(KeyCode.E))
                {
                    triggered = true;
                    task.set_message("");
                }
            }
        }
    }


}
