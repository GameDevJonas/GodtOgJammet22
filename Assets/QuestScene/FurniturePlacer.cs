using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FurniturePlacer : MonoBehaviour
{
    private void Awake()
    {
        QuestManager.OnQuestComplete.AddListener(OnQuestComplete);
    }

    private void OnQuestComplete(QuestDefinition quest)
    {
        //var furniture = Instantiate(quest.furnitureItem, quest.furnitureSpawnLocation.position, Quaternion.identity, quest.furnitureSpawnLocation);
        quest.furnitureItem.SetActive(true);
    }
}
