using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


//this script is added to the object who speaks.

public class Interactable : MonoBehaviour
{

    [SerializeField] private BoxCollider interactableBox;
    public UnityEvent OnInteracted = new UnityEvent();
    
    public bool isInteractable;


    private void Update()
    {
        if(isInteractable && Input.GetKeyDown(KeyCode.E))
        {
            OnInteracted.Invoke();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isInteractable = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player")) isInteractable = false;
    }



}
