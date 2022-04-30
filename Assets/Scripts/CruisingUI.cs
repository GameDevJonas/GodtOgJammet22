using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class CruisingUI : MonoBehaviour
{
    [SerializeField] private Image speedometerArrow;
    [SerializeField] private TextMeshProUGUI boatSpeedText;

    [SerializeField] private Animator islandNameAnim;

    private BoatMovement boatMovement;

    private void Awake()
    {
        boatMovement = FindObjectOfType<BoatMovement>();
    }

    private void Update()
    {
        UpdateSpeedometer();
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
