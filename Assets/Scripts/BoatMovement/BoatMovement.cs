using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using TMPro;
using UnityEngine.UI;

public class BoatMovement : MonoBehaviour
{
    public float BoatSpeedValue;

    [SerializeField] private float turnSpeed;
    [SerializeField] private float boatSpeed;
    [SerializeField] private Image speedometerArrow;

    [SerializeField] private TextMeshProUGUI boatSpeedText;

    private Rigidbody _rb;
    private float _xInput, _yInput;

    private Vector3 _lastPos;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
    }


    private void Update()
    {
        GetInputs();
        CalculateSpeed();
    }

    private void FixedUpdate()
    {
        RotateBoat();
        MoveBoat();
    }

    private void GetInputs()
    {
        _xInput = Input.GetAxisRaw("Horizontal");
        _yInput = Input.GetAxisRaw("Vertical");
    }

    private void CalculateSpeed()
    {
        float speedValue;
        double speedText;
        speedValue = _rb.velocity.magnitude;
        speedValue *= 10;
        speedText = System.Math.Round(speedValue, 0);
        boatSpeedText.text = speedText.ToString();

        float currentSpeedLerp = BoatSpeedValue / 2.5f;
        float rotationAngle = Mathf.Lerp(0, -240f, currentSpeedLerp);
        speedometerArrow.transform.rotation = Quaternion.Euler(0, 0, rotationAngle);
    }

    private void RotateBoat()
    {
        if (_xInput != 0)
        {
            transform.Rotate(new Vector3(0, _xInput * turnSpeed, 0));
        }
    }

    private void MoveBoat()
    {
        if (_yInput > 0 && BoatSpeedValue < 2.5f)
        {
            BoatSpeedValue += Time.deltaTime;
        }
        else if (_yInput < 0 && BoatSpeedValue > 0)
        {
            BoatSpeedValue -= Time.deltaTime;
        }

        if (BoatSpeedValue > 0) _rb.velocity = transform.forward * boatSpeed * BoatSpeedValue;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Obstacle"))
        {
            //BoatSpeedValue = .9f;
        }
    }
}
