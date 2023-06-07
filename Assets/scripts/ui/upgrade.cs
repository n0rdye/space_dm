using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class upgrade : MonoBehaviour
{
    public Button button;

    void Start()
    {
        button.onClick.AddListener(() => ButtonClicked("hello"));
    }

    void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "player")
        {
            if (Input.GetKey(KeyCode.E))
            {
                
            }
        }
    }

    void ButtonClicked(string buttonNo)
    {
        //Output this to console when the Button3 is clicked
        Debug.Log("Button clicked = " + buttonNo);
    }

}
