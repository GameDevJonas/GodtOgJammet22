using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//this script is irrelevant.

public class StartConversation : MonoBehaviour
{

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Talk")
        {

            if (Input.GetKeyDown(KeyCode.E)) 
            {
                other.gameObject.GetComponent<YarnInteractable>().StartConversation();

            }
        }
    }

}
