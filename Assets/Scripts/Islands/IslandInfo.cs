using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IslandInfo : MonoBehaviour
{
    [SerializeField] private string islandName;
    [SerializeField] private GameObject myMapName;
    private CruisingUI cruisingUI;

    private bool activated;

    private void Awake()
    {
        activated = false;
        cruisingUI = FindObjectOfType<CruisingUI>();
    }

    private void Start()
    {
        myMapName.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && activated && BoatModes.myBoatMode == BoatModes.BoatMode.cruise)
        {
            GetComponent<ResourceGiver>().GiveResource();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) EnterExitMe();
        if (!myMapName.activeSelf) myMapName.SetActive(true);
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
