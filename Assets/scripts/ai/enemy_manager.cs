using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy_manager : MonoBehaviour
{
    public Transform player;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("charecter").transform;

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void hit(GameObject main)
    {
        var enemies = GameObject.FindGameObjectsWithTag("enemy");
        foreach (var item in enemies)
        {
                Debug.Log(Vector3.Distance(main.transform.position, item.transform.position));
            if (Vector3.Distance(main.transform.position, item.transform.position) <= 15 && item.name != main.name)
            {
                Debug.Log(item.gameObject.name);
                var script = item.gameObject.GetComponent<enemy>();
                script.last_seen = player;
                //Debug.Log("em hit");
            }
        }
    }

}
