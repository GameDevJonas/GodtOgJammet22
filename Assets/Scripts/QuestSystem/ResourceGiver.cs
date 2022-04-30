using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

public class ResourceGiver : MonoBehaviour
{
    [SerializeField] private string resource;
    [SerializeField] private int amount;

    [Button]
    public void GiveResource()
    {
        if (QuestTracker.CurrentQuest != null && QuestTracker.CurrentQuest.HasRequirementType(resource))
        {
            QuestTracker.CurrentQuest.UpdateProgress(resource, amount);
        }
    }
}
