using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hide_roof : MonoBehaviour
{

    public Color hit_obj_color;
    public Color tmp_color;
    //public Material tmp_color;
    public GameObject top;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        try
        {
            Debug.DrawRay(transform.position, (Camera.main.transform.position - transform.position), Color.yellow);
            RaycastHit hit;
            // Does the ray intersect any objects excluding the player layer
            RaycastHit[] hitting;
            hitting = Physics.RaycastAll(transform.position, (Camera.main.transform.position - transform.position), Mathf.Infinity);
            var other = GameObject.FindGameObjectsWithTag("hide");

            foreach (var item in other)
            {
                tmp_color.a = 1f;
                item.transform.gameObject.GetComponent<Renderer>().enabled = true;
                item.GetComponent<Renderer>().material.color = tmp_color;
            }
            foreach (var item in hitting)
            {
                if (item.transform.gameObject.tag == "hide")
                {
                    Debug.Log(item.transform.gameObject.GetComponent<Renderer>().material.name);
                    if (item.transform.gameObject.GetComponent<Renderer>().material.name == "roof (Instance)")
                    {
                        Debug.DrawRay(transform.position, (Camera.main.transform.position - transform.position), Color.yellow);
                        //if (hit_obj_color!= hit.collider.gameObject.GetComponent<Renderer>().material.color)
                        //{
                        //    hit_obj_color = hit.collider.gameObject.GetComponent<Renderer>().material.color;
                        //}
                        item.transform.gameObject.GetComponent<Renderer>().enabled = false;
                        //tmp_color.a = 0;
                        //hit.collider.gameObject.GetComponent<Renderer>().material.ChangeAlpha(0.5);
                        //item.transform.gameObject.GetComponent<Renderer>().material.color = tmp_color;
                        //hit_obj = hit.collider.gameObject;
                        //var MeshRenderer tmp = hit_obj.transform.GetComponent<MeshRenderer>();
                        //tmp.material.color = Color.black;
                        //Debug.Log("Did Hit");
                    }
                    else if (item.transform.gameObject.GetComponent<Renderer>().material.name == "wall (Instance)")
                    {
                        Debug.DrawRay(transform.position, (Camera.main.transform.position - transform.position), Color.yellow);
                        //if (hit_obj_color!= hit.collider.gameObject.GetComponent<Renderer>().material.color)
                        //{
                        //    hit_obj_color = hit.collider.gameObject.GetComponent<Renderer>().material.color;
                        //}
                        tmp_color = item.transform.gameObject.GetComponent<Renderer>().material.color;
                        tmp_color.a = 0.4f;
                        //hit.collider.gameObject.GetComponent<Renderer>().material.ChangeAlpha(0.5);
                        item.transform.gameObject.GetComponent<Renderer>().material.color = tmp_color;
                        //hit_obj = hit.collider.gameObject;
                        //var MeshRenderer tmp = hit_obj.transform.GetComponent<MeshRenderer>();
                        //tmp.material.color = Color.black;
                        //Debug.Log("Did Hit");
                    }
                }
            }
            
        }
        catch
        {

        }
    }
}
