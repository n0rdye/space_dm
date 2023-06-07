using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class upgrade : MonoBehaviour
{
    void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "player")
        {
            if (Input.GetKey(KeyCode.E))
            {
                inventory inv = other.gameObject.GetComponent<inventory>();
                inv.var.materials += 10;
                inv.save();
                inv.load();
                Debug.Log("ammo added");
                Destroy(this.gameObject);
            }
        }
        if (other.gameObject.tag == "weapon")
        {
            if (Input.GetKey(KeyCode.E))
            {
                shooting wp = other.gameObject.GetComponent<shooting>();
                wp.stats.inv_ammo += wp.stats.max_ammo;
                wp.save();
                //new save_load().add_ammo(wp.name, wp.max_ammo);
                wp.load();
                Debug.Log("ammo added");
                Destroy(this.gameObject);
            }
        }
    }
}
