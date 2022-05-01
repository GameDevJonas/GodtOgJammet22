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
        furnitures[quest.furnitureID].SetActive(true);
    }
}
