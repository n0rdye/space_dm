using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class face_task : MonoBehaviour
{
    public Transform obj;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        gameObject.transform.LookAt(obj);
        //gameObject.transform.rotation = Quaternion.LookRotation(obj.forward);
    }
}
