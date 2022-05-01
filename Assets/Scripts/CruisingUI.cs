using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class CruisingUI : MonoBehaviour
{
    [SerializeField] private Image speedometerArrow;
    [SerializeField] private TextMeshProUGUI boatSpeedText;

    [SerializeField] private GameObject minimapObject;
    [SerializeField] private GameObject speedometerObject;

    [SerializeField] private Transform boatPlayer;
    [SerializeField] private Transform boatUI;

    [SerializeField] private Animator islandNameAnim;

    private BoatMovement boatMovement;

    bool tabKeyDown;

    private void Awake()
    {
        boatMovement = FindObjectOfType<BoatMovement>();
    }

    private void Update()
    {
        UpdateSpeedometer();
        CheckForUI();

        if (Input.GetKeyDown(KeyCode.Tab))
        {
            tabKeyDown = !tabKeyDown;
        }
    }

    private void CheckForUI()
    {
        boatUI.position = new Vector3(boatPlayer.position.x, boatUI.position.y, boatPlayer.position.z);
        boatUI.rotation = boatPlayer.GetChild(0).rotation;

        //bool tabKeyDown = Input.GetKey(KeyCode.Tab);
        minimapObject.SetActive(tabKeyDown);
        speedometerObject.SetActive(tabKeyDown);

    }

    private void UpdateSpeedometer()
    {
        boatSpeedText.text = boatMovement.SpeedDouble.ToString();
        speedometerArrow.transform.rotation = Quaternion.Euler(0, 0, boatMovement.RotationAngle);
    }

    public void IslandNameIn(string islandName)
    {
        islandNameAnim.GetComponentInChildren<TMPro.TextMeshProUGUI>().text = islandName;
        islandNameAnim.SetTrigger("NameIn");
    }
}
