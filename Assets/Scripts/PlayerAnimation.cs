using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    private PlayerMovement movement;
    private Animator anim;
    private CharacterController controller;

    private void Awake()
    {
        movement = GetComponent<PlayerMovement>();
        anim = GetComponentInChildren<Animator>();
        controller = GetComponent<CharacterController>();
    }

    private void Update()
    {
        CheckForMoving();

        anim.SetBool("CruiseMode", !controller.enabled);
    }

    private void CheckForMoving()
    {
        if (movement.enabled) anim.SetBool("IsMoving", movement.isMoving);
        else anim.SetBool("IsMoving", false);
    }
}
