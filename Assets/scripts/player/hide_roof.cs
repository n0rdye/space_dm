using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hide_roof : MonoBehaviour
{

    public GameObject hit_obj;
    public Color hit_obj_color;
    public Color tmp_color;
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
            var isHitting = Physics.Raycast(transform.position, (Camera.main.transform.position - transform.position), out hit, Mathf.Infinity);
            if (isHitting)
            {
                if (hit.collider.gameObject.tag == "roof")
                {
                    Debug.DrawRay(transform.position, (Camera.main.transform.position - transform.position), Color.yellow);
                    hit_obj = hit.collider.gameObject;
                    //if (hit_obj_color!= hit.collider.gameObject.GetComponent<Renderer>().material.color)
                    //{
                    //    hit_obj_color = hit.collider.gameObject.GetComponent<Renderer>().material.color;
                    //}
                    tmp_color = hit.collider.gameObject.GetComponent<Renderer>().material.color;
                    tmp_color.a = 0.42f;
                    //hit.collider.gameObject.GetComponent<Renderer>().material.ChangeAlpha(0.5);
                    hit.collider.gameObject.GetComponent<Renderer>().material.color = tmp_color;
                    //hit_obj = hit.collider.gameObject;
                    //var MeshRenderer tmp = hit_obj.transform.GetComponent<MeshRenderer>();
                    //tmp.material.color = Color.black;
                    //Debug.Log("Did Hit");
                }

            }
            if (!isHitting)
            {
                //hit_obj.GetComponent<Renderer>().enabled = true;
                tmp_color.a = 1f;
                hit_obj.GetComponent<Renderer>().material.color = tmp_color;
               // Debug.Log("Did Hit");
            }
        }
        catch
        {

        }
    }
}
