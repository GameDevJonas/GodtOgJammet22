using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRotateToCamera : MonoBehaviour
{
    private PlayerMovement movement;

    [SerializeField] private float rotationSpeed;

    [SerializeField] private Transform rotationPoint;
    [SerializeField] private Transform avatar;

    private Transform mainCamera;
    private CharacterController controller;

    private void Awake()
    {
        mainCamera = Camera.main.transform;
        movement = GetComponent<PlayerMovement>();
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        RotatePlayerToCamera();
    }

    private void RotatePlayerToCamera()
    {
        //rotationPoint.position = avatar.position;

        Vector3 movementDirection = new Vector3(movement.movementDir.x, 0, movement.movementDir.z);
        rotationPoint.localPosition = movementDirection;

        if (movementDirection != Vector3.zero && controller.enabled)
        {
            avatar.LookAt(rotationPoint, Vector3.up);
            avatar.rotation = Quaternion.Euler(0, avatar.rotation.eulerAngles.y, 0);
        }

        if ((transform.rotation.y != mainCamera.rotation.y) && controller.velocity != Vector3.zero)
        {
            //float yRotation = Quaternion.Slerp(transform.rotation, mainCamera.rotation, rotationSpeed * Time.deltaTime).eulerAngles.y;
            //transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, yRotation, transform.rotation.eulerAngles.z);
        }
    }

    public void ResetRotation()
    {
        transform.parent = null;
        transform.rotation = Quaternion.Euler(0, 0, 0);
    }
}
