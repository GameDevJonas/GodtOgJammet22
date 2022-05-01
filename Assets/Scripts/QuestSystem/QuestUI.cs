using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class QuestUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI questTitletext;
    [SerializeField] private TextMeshProUGUI questResourcetext;

    public int completedQuests;

    private void Start()
    {
        completedQuests = 0;
        QuestManager.OnQuestStart.AddListener(TurnOnQuestIndicator);
        QuestManager.OnQuestDone.AddListener(TurnOffQuestIndicator);
    }

    public void TurnOnQuestIndicator()
    {
        questTitletext.text = "Furniture: " + QuestTracker.CurrentQuest.questDefinition.questNameId;
        questResourcetext.text = "Needs: " + QuestTracker.CurrentQuest.questDefinition.questCompletionRequirements[0].resourceId;
    }

    public void TurnOffQuestIndicator()
    {
        questTitletext.text = "";
        questResourcetext.text = "";
        completedQuests++;
        if (completedQuests == 10)
        {
            CompletedAllQuests();
        }
    }

    public void CompletedAllQuests()
    {
        questTitletext.text = "Wow all quests done!";
    }
}
