using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class DialogueNode : ScriptableObject
{
    public string dialogueText;
    public List<DialogueResponse> responses = new List<DialogueResponse>();
    [SerializeField] public UnityEvent onNodeProcessed; // UnityEvent to be executed when this node is processed
    public bool runEventOnNodeProcessed; // Flag to indicate whether the event should be run

    internal bool IsLastNode()
    {
        return responses.Count <= 0;
    }
}