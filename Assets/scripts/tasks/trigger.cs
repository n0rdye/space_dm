using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class trigger : MonoBehaviour
{
    public bool triggered = false;
    public bool key = false;
    public inventory inv;
    public task_manager task;
    public string item;
    // Start is called before the first frame update
    void Start()
    {
        this.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (triggered)
        {
            Destroy(gameObject);
            task.set_message("");
        }
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
            if (!key)
            {
                triggered = true;
            }
        }
    }

    void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "player" && !triggered)
        {
            if (key)
            {
                task.set_message("press e to pick up "+item);
                if (Input.GetKey(KeyCode.E))
                {
                    triggered = true;
                }
            }
        }
    }


}
