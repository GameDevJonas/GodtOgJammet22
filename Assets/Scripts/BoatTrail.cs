using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoatTrail : MonoBehaviour
{
    public Transform targetPos;


    void Update()
    {
        transform.position = targetPos.position;
    }
}
