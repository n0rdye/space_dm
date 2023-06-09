using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class face_camera : MonoBehaviour
{
    public Camera  obj;

    // Update is called once per frame
    void Update()
    {
        //transform.LookAt(transform.position + obj.transform.rotation * Vector3.forward,obj.transform.rotation * Vector3.up);
        gameObject.transform.LookAt(obj.transform);
        gameObject.transform.rotation = Quaternion.LookRotation(obj.transform.forward);
    }
}
