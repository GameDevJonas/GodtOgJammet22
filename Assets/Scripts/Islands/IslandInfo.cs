using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IslandInfo : MonoBehaviour
{
    [SerializeField] private string islandName;

    private CruisingUI cruisingUI;

    private bool activated;

    private void Awake()
    {
        activated = false;
        cruisingUI = FindObjectOfType<CruisingUI>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) EnterExitMe();
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player")) EnterExitMe();
    }

    private void EnterExitMe()
    {
        activated = !activated;
        if (activated) cruisingUI.IslandNameIn(islandName);
    }
}
