using System.Collections.Generic;
using System.Linq;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;

public class QuestManager : MonoBehaviour
{
    // Singleton pattern although it doesnt ensure only one instance whoops
    public static QuestManager Instance;
    
    // Quests the player can get, aka all quests
    [SerializeField] private List<QuestDefinitionObject> quests = new List<QuestDefinitionObject>();

    // Events
    public static UnityEvent<Quest> OnQuestStarted = new UnityEvent<Quest>();
    public static UnityEvent<QuestDefinition> OnQuestComplete = new UnityEvent<QuestDefinition>();

    public void Awake()
    {
        Instance = this;
    }

    [Button]
    public void StartQuest(string questName)
    {

        // Sanity check
        if (QuestTracker.CurrentQuest != null)
        {
            Debug.LogError("Can't start a new quest because the current quest has not been completed.");
            return;
        }
        
        // Attempt to get quest and a sanity check
        var questToStart = quests.FirstOrDefault(e => e.questDefinition.questNameId.Equals(questName));
        if (questToStart == null)
        {
            Debug.LogError($"The Quest \"{questName}\" does not exist in the quest list!");
            return;
        }
        
        if(QuestTracker.CompletedQuests.Any(q => q.questDefinition.questNameId.Equals(questName)))
        {
            Debug.LogError($"Quest \"{questName}\" already completed.");
            return;
        }


        // Set new quest as current quest
        QuestTracker.CurrentQuest = new Quest(questToStart.questDefinition);
        
        // Invoke on quest started event
        OnQuestStarted.Invoke(QuestTracker.CurrentQuest);
        Debug.Log($"Started Quest \"{QuestTracker.CurrentQuest.questDefinition.questNameId}\"");
        
        // Invoke and clean up when the quest is complete
        QuestTracker.CurrentQuest.OnQuestComplete.AddOneTimeListener(() =>
        {
            OnQuestComplete.Invoke(QuestTracker.CurrentQuest.questDefinition);
            QuestTracker.RegisterQuestAsComplete(QuestTracker.CurrentQuest);
            QuestTracker.CurrentQuest = null;
        });
    }
}
