using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FurniturePlacer : MonoBehaviour
{
    public GameObject[] furnitures;

    private void Awake()
    {
        QuestManager.OnQuestComplete.AddListener(OnQuestComplete);
        foreach(GameObject obj in furnitures)
        {
            obj.SetActive(false);
        }
    }

    private void OnQuestComplete(QuestDefinition quest)
    {
        //var furniture = Instantiate(quest.furnitureItem, quest.furnitureSpawnLocation.position, Quaternion.identity, quest.furnitureSpawnLocation);
        quest.furnitureItem.SetActive(true);
    }
}
