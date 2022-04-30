using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRotateToCamera : MonoBehaviour
{
    [SerializeField] private float rotationSpeed;

    private Transform mainCamera;
    private CharacterController controller;

    private void Awake()
    {
        mainCamera = Camera.main.transform;
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        RotatePlayerToCamera();
    }

    private void RotatePlayerToCamera()
    {
        if ((transform.rotation.y != mainCamera.rotation.y) && controller.velocity != Vector3.zero)
        {
            float yRotation = Quaternion.Slerp(transform.rotation, mainCamera.rotation, rotationSpeed * Time.deltaTime).eulerAngles.y;
            transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, yRotation, transform.rotation.eulerAngles.z);
        }
    }
}
