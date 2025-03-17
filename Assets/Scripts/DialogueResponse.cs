using System;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class DialogueResponse : ScriptableObject
{
    public string responseText;
    public DialogueNode nextNode;
    [SerializeField] public UnityEvent onResponseSelected; // UnityEvent to be executed when this response is selected
    public bool runEventOnResponseSelected; // Flag to indicate whether the event should be run
}