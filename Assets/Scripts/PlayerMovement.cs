using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private CharacterController _characterController;
    private Transform mainCamera;


    public float speed = 6.0f;
    public float jumpSpeed = 8.0f;
    public float gravity = 20.0f;

    private float xInput, yInput;

    private Vector3 moveDirection;

    private void Awake()
    {
        _characterController = GetComponent<CharacterController>();
        mainCamera = Camera.main.transform;
    }

    void Update()
    {
        GetInputs();
        ApplyMovement();
    }

    private void GetInputs()
    {
        xInput = Input.GetAxis("Horizontal");
        yInput = Input.GetAxis("Vertical");

        if (Input.GetButtonDown("Jump") && _characterController.isGrounded)
        {
            DoJump();
        }
    }

    private void ApplyMovement()
    {
        moveDirection = new Vector3(xInput, -gravity * Time.deltaTime, yInput);
        moveDirection *= speed;

        _characterController.Move(moveDirection * Time.deltaTime);
    }

    private void RotatePlayerToCamera()
    {

    }

    private void DoJump()
    {
        moveDirection.y = jumpSpeed;
    }
}
