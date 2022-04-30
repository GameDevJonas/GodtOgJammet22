using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestSubscriber : MonoBehaviour
{
    private void Awake()
    {
        QuestManager.OnQuestComplete.AddListener(e => Debug.Log("complete!!!!!!!! " + e.questNameId));
    }
}
