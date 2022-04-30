using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BoatModes : MonoBehaviour
{
    public enum BoatMode { cruise, deck};
    [SerializeField] BoatMode myBoatMode;

    public UnityEvent cruiseEvents, deckEvents;

    private void Start()
    {
        SwitchModes();
    }

    private void Update()
    {
        //if (Input.GetKeyDown(KeyCode.Space)) SwitchModes();
    }

    public void SwitchModes()
    {
        if(myBoatMode == BoatMode.cruise)
        {
            myBoatMode = BoatMode.deck;
        }
        else
        {
            myBoatMode = BoatMode.cruise;
        }
        SwitchModeEvents(myBoatMode);
    }

    private void SwitchModeEvents(BoatMode mode)
    {
        switch (mode)
        {
            case BoatMode.cruise:
                cruiseEvents.Invoke();
                break;
            case BoatMode.deck:
                deckEvents.Invoke();
                break;
        }
    }
}
