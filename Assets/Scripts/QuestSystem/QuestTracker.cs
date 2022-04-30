using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class QuestTracker
{
    public static Quest CurrentQuest { get; set; }

    private static List<Quest> completedQuests = new List<Quest>();

    public static void RegisterQuestAsComplete(Quest quest)
    {
        completedQuests.Add(quest);
    }
}
