using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class pick_up : MonoBehaviour
{
    public drop_stats drop_stats;

    void OnTriggerStay(Collider other)
    {

        if (drop_stats.num <= 0)
        {
            Destroy(this.gameObject);
        }

        if (drop_stats.type == drop_stats.types.ammo && drop_stats.num > 0)
        {
            if (other.gameObject.tag == "weapon")
            {
                if (Input.GetKey(KeyCode.E) && drop_stats.weapon != drop_stats.weapons.all && drop_stats.weapon == drop_stats.weapons.current)
                {
                    shooting wp = other.gameObject.GetComponent<shooting>();
                    wp.stats.inv_ammo += wp.stats.max_ammo;
                    wp.save();
                    //new save_load().add_ammo(wp.name, wp.max_ammo);
                    wp.load();
                    Debug.Log("ammo added");
                    drop_stats.num--;
                }
                else if (Input.GetKey(KeyCode.E) && drop_stats.weapon == drop_stats.weapons.all)
                {
                    vars var = new save_load().load();
                    foreach (var cw in var.weapons)
                    {
                        weapon wp = new save_load().loadw(cw);
                        wp.inv_ammo += wp.max_ammo;
                        new save_load().savew(cw, wp);
                        shooting shot = other.gameObject.GetComponent<shooting>();
                        shot.load();
                        Debug.Log("ammo added for all");
                    }
                    drop_stats.num--;
                }
                else if (Input.GetKey(KeyCode.E)){
                    weapon wp = new save_load().loadw(drop_stats.weapon.ToString());
                    wp.inv_ammo += wp.max_ammo;
                    //new save_load().add_ammo(wp.name, wp.max_ammo);
                    new save_load().savew(drop_stats.weapon.ToString(),wp);
                    Debug.Log("ammo added for " + drop_stats.weapon.ToString());
                    drop_stats.num--;
                }
            }
        }
        else if (drop_stats.type == drop_stats.types.materials && drop_stats.num > 0)
        {
            if (other.gameObject.tag == "player")
            {
                if (Input.GetKey(KeyCode.E))
                {
                    inventory inv = other.gameObject.GetComponent<inventory>();
                    inv.var.materials +=drop_stats.num * 10;
                    inv.save();
                    inv.load();
                    Debug.Log("mats added");
                    drop_stats.num = 0;
                }
            }
        }
        else if (drop_stats.type == drop_stats.types.unlock_weapon &&drop_stats.num > 0)
        {
            if (other.gameObject.tag == "player")
            {
                string cw = drop_stats.weapon.ToString();
                if (drop_stats.weapon != drop_stats.weapons.all && new save_load().loadw(cw).unlock == false)
                {
                    drop_stats.num = 1;
                    Debug.Log("weapon");
                    Debug.Log(cw);

                    if (Input.GetKey(KeyCode.E))
                    {
                        inventory inv = other.gameObject.GetComponent<inventory>();
                        weapon wp = new save_load().loadw(cw);
                        wp.unlock = true;
                        new save_load().savew(cw, wp);
                        inv.var.weapon = cw;
                        inv.save();
                        inv.load();
                        inv.set_weapon(cw);
                        Debug.Log("weapon unlocked");
                        drop_stats.num--;
                    }

                }
                else if (drop_stats.weapon == drop_stats.weapons.all)
                {
                    if (Input.GetKey(KeyCode.E))
                    {
                        inventory inv = other.gameObject.GetComponent<inventory>();
                        foreach (var cwa in inv.var.weapons)
                        {
                            weapon wp = new save_load().loadw(cwa);
                            if (wp.unlock == false)
                            {
                                wp.unlock = true;
                                new save_load().savew(cwa, wp);
                                inv.var.weapon = (inv.var.weapon != "none") ? inv.var.weapon : "none";
                                inv.save();
                                inv.load();
                                inv.set_weapon(inv.var.weapon);
                                Debug.Log("all unlocked");
                            }
                            else
                            {
                                wp.inv_ammo += wp.max_ammo;
                                new save_load().savew(cwa, wp);
                                foreach (var item in inv.weapons)
                                {
                                    if(item.activeSelf)
                                    {
                                        var shot = item.gameObject.GetComponent<shooting>();
                                        shot.load();
                                    }
                                }
                                inv.load();
                                Debug.Log("all unlocked ammo");
                            }
                        }
                        drop_stats.num = 0;
                    }
                }
                else
                {
                    drop_stats.type = drop_stats.types.ammo;
                }
            }
        }
        else if (drop_stats.type == drop_stats.types.lock_weapon && drop_stats.num > 0)
        {
            string cw = drop_stats.weapon.ToString();
            drop_stats.num = 1;
            Debug.Log("weapon");
            Debug.Log(cw);

            if (other.gameObject.tag == "player")
            {
                if (Input.GetKey(KeyCode.E) && drop_stats.weapon == drop_stats.weapons.all)
                {
                    inventory inv = other.gameObject.GetComponent<inventory>();
                    foreach (var cwa in inv.var.weapons)
                    {
                        weapon wp = new save_load().loadw(cwa);
                        wp.unlock = false;
                        new save_load().savew(cwa, wp);
                        inv.var.weapon = "none";
                        inv.save();
                        inv.load();
                        inv.set_weapon("none");
                        Debug.Log("all locked");
                    }
                    drop_stats.num = 0;
                }
                else if (Input.GetKey(KeyCode.E))
                {
                    inventory inv = other.gameObject.GetComponent<inventory>();
                    weapon wp = new save_load().loadw(cw);
                    wp.unlock = false;
                    new save_load().savew(cw, wp);
                    inv.var.weapon = "none";
                    inv.save();
                    inv.load();
                    inv.set_weapon("none");
                    Debug.Log("weapon locked");
                    drop_stats.num--;
                }
            }
        }
        else if (drop_stats.type == drop_stats.types.health && drop_stats.num > 0)
        {
            if (other.gameObject.tag == "player")
            {
                inventory inv = other.gameObject.GetComponent<inventory>();
                if (Input.GetKey(KeyCode.E))
                {
                    inv.var.health += drop_stats.num;
                    inv.save();
                    inv.load();
                    drop_stats.num = 0;
                }
                //if (Input.GetKey(KeyCode.E) && inv.var.health != 100)
                //{
                //    var tmp = 100 - inv.var.health;
                //    inv.var.health += num;
                //    inv.save();
                //    inv.load();
                //    num = tmp;
                //    //Debug.Log(100 - tmp);
                //}
            }
        }
    }
}
