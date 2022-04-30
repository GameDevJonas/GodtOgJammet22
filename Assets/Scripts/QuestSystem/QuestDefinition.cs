using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Definition of a single quest
/// </summary>
[Serializable]
public class QuestDefinition
{
    public string questNameId;
    public GameObject furnitureItem;
    //public Transform furnitureSpawnLocation;

    public List<QuestStartRequirement> questStartRequirements = new List<QuestStartRequirement>();
    public List<QuestResourceRequirement> questCompletionRequirements = new List<QuestResourceRequirement>();
}

/// <summary>
/// Class defining the requirement for a resource
/// </summary>
[Serializable]
public class QuestResourceRequirement
{
    public string resourceId;
    public int requiredAmount;
}

/// <summary>
/// Class defining the requirement for starting a quest
/// </summary>
public class QuestStartRequirement
{
    public string questToBeenCompleted;
}

/// <summary>
/// Single item to track the progress of one resource
/// </summary>
[Serializable]
public class QuestResourceProgressItem
{
    public QuestResourceRequirement requirement;
    public int currentAmount;

    public bool IsRequirementMet()
    {
        return requirement.requiredAmount <= currentAmount;
    }

    public QuestResourceProgressItem(QuestResourceRequirement requirement)
    {
        this.requirement = requirement;
    }
}

/// <summary>
/// Quest object used for an active quest
/// </summary>
[Serializable]
public class Quest
{
    public QuestDefinition questDefinition;
    public List<QuestResourceProgressItem> questProgress = new List<QuestResourceProgressItem>();

    public UnityEvent<QuestResourceProgressItem> OnProgressItemUpdated = new UnityEvent<QuestResourceProgressItem>();
    public UnityEvent<string> OnProgressItemCompleted = new UnityEvent<string>();
    public UnityEvent OnQuestComplete = new UnityEvent();

    public bool HasRequirementType(string resource)
    {
        return questProgress.Any(e => e.requirement.resourceId.Equals(resource));
    }
    
    public void UpdateProgress(string resource, int amount)
    {
        // Find and sanity check
        var progressItemToUpdate = questProgress.FirstOrDefault(e => e.requirement.resourceId.Equals(resource));
        if (progressItemToUpdate == null)
        {
            Debug.LogError($"\"{resource}\" is not a requirement in this {nameof(Quest)}");
            return;
        }

        // Update item and invoke event
        progressItemToUpdate.currentAmount += amount;
        Debug.Log($"Added {amount} amount of {resource} to quest \"{questDefinition.questNameId}\".");
        OnProgressItemUpdated.Invoke(progressItemToUpdate);
        
        // Check if this item is complete
        if (progressItemToUpdate.IsRequirementMet())
        {
            Debug.Log($"Requirement met for resource \"{progressItemToUpdate.requirement.resourceId}\"!");
            OnProgressItemCompleted.Invoke(resource);
        }

        // Check if quest is complete
        if (questProgress.All(e => e.IsRequirementMet()))
        {
            Debug.Log($"Quest \"{questDefinition.questNameId}\" completed!");
            OnQuestComplete.Invoke();
        }
        
    }

    public Quest(QuestDefinition questDefinition)
    {
        this.questDefinition = questDefinition;
        foreach (var requirement in questDefinition.questCompletionRequirements)
        {
            questProgress.Add(new QuestResourceProgressItem(requirement));
        }
    }
}