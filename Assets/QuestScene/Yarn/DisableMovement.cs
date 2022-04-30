using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableMovement : MonoBehaviour
{

    public PlayerMovement playerMovement;

    private void Update()
    {
        if (YarnInteractable.ConversationActive) playerMovement.enabled = false;
        else playerMovement.enabled = true;
    }

}
