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
        if (controller.enabled) CheckForMoving();
        else anim.SetBool("IsMoving", false);
    }

    private void CheckForMoving()
    {
        anim.SetBool("IsMoving", movement.isMoving);
    }
}