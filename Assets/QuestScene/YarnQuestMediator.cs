using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;

public static class YarnQuestMediator 
{

    [YarnCommand("startQuest")]
    public static void StartQuest(string questName)
    {
        QuestManager.Instance.StartQuest(questName);
    }

}
