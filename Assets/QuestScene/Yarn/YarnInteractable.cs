using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Yarn.Unity;

//this script is added to the object who speaks.

public class YarnInteractable : MonoBehaviour
{
    //if a conversation is currently active & running.
    public static bool ConversationActive;

    private DialogueRunner dialogueRunner;
    private LineView lineView;
    public string conversationStartNode;

    private bool isCurrentConversation;

    public void Start()
    {
        dialogueRunner = FindObjectOfType<Yarn.Unity.DialogueRunner>();
        dialogueRunner.onDialogueComplete.AddListener(EndConversation);

        lineView = FindObjectOfType<Yarn.Unity.LineView>();
    }

    public void StartConversation()
    {
        if (QuestTracker.CurrentQuest != null)
        {
            return;
        }
        
        isCurrentConversation = true;
        dialogueRunner.StartDialogue(conversationStartNode);
        ConversationActive = true;
    }

    private void EndConversation()
    {
        if (isCurrentConversation)
        {
            isCurrentConversation = false;
        }

        if (ConversationActive) ConversationActive = false;
    }

    private void Update()
    {
        if(ConversationActive && Input.GetKeyDown(KeyCode.Space))
        {
            lineView.OnContinueClicked();
        }
    }

}
