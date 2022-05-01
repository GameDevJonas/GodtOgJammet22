using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicChanger : MonoBehaviour
{
    [SerializeField] private AudioSource phaseOne, phaseTwo, phaseThree;

    private QuestUI questUI;

    private void Awake()
    {
        questUI = FindObjectOfType<QuestUI>();
    }

    private void Update()
    {
        switch (questUI.completedQuests)
        {
            case 0:
                if (phaseOne.volume != 1) phaseOne.volume = 1;
                if (phaseTwo.volume != 0) phaseTwo.volume = 0;
                if (phaseThree.volume != 0) phaseThree.volume = 0;
                break;
            case 5:
                if (phaseOne.volume != 0) phaseOne.volume = 0;
                if (phaseTwo.volume != 1) phaseTwo.volume = 1;
                if (phaseThree.volume != 0) phaseThree.volume = 0;
                break;
            case 8:
                if (phaseOne.volume != 0) phaseOne.volume = 0;
                if (phaseTwo.volume != 0) phaseTwo.volume = 0;
                if (phaseThree.volume != 1) phaseThree.volume = 1;
                this.enabled = false;
                break;
        }
    }

}
