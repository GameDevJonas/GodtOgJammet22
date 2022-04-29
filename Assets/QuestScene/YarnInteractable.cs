using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Yarn.Unity;


public class YarnInteractable : MonoBehaviour
{
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
        isCurrentConversation = true;
        dialogueRunner.StartDialogue(conversationStartNode);
        ConversationActive = true;
    }

    private void EndConversation()
    {

    }

    private void Update()
    {
        if(ConversationActive && Input.GetKeyDown(KeyCode.Space))
        {
            lineView.OnContinueClicked();
        }
    }

}
