using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.AI;

public class BoatMovement : MonoBehaviour
{
    [HideInInspector] public float BoatSpeedValue;
    [HideInInspector] public float RotationAngle;
    [HideInInspector] public double SpeedDouble;

    public bool canMove;

    [SerializeField] private float turnSpeed;
    [SerializeField] private float boatSpeed;

    private Rigidbody _rb;
    private float _xInput, _yInput;

    private Vector3 _lastPos;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
    }


    private void Update()
    {
        if (canMove)
        {
            GetInputs();
            CalculateSpeed();
        }
    }

    private void FixedUpdate()
    {
        RotateBoat();
        MoveBoat();
    }

    public void SwitchMode(bool cruiseModeTrue)
    {
        if (cruiseModeTrue)
        {
            canMove = true;
            TurnOnOffNavMeshAgents(false);
            _rb.constraints = RigidbodyConstraints.FreezeRotation;
        }
        else
        {
            BoatSpeedValue = 0;
            _rb.velocity = Vector3.zero;
            _rb.constraints = RigidbodyConstraints.FreezeAll;
            canMove = false;
            TurnOnOffNavMeshAgents(true);
            _xInput = 0;
            _yInput = 0;
        }
    }

    void TurnOnOffNavMeshAgents(bool turnOn)
    {
        foreach(NavMeshAgent agent in FindObjectsOfType<NavMeshAgent>())
        {
            agent.enabled = turnOn;
            agent.GetComponent<Patrol>().myAnimator.SetBool("IsMoving", turnOn);
        }
    }

    private void GetInputs()
    {
        _xInput = Input.GetAxisRaw("Horizontal");
        _yInput = Input.GetAxisRaw("Vertical");
    }

    private void CalculateSpeed()
    {
        float speedValue;
        speedValue = _rb.velocity.magnitude;
        speedValue *= 10;
        SpeedDouble = System.Math.Round(speedValue, 0);

        float currentSpeedLerp = BoatSpeedValue / 2.5f;
        RotationAngle = Mathf.Lerp(0, -240f, currentSpeedLerp);
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
}
