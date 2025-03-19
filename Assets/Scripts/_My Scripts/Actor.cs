using UnityEngine;

public class Actor : MonoBehaviour
{
    public string Name;
    public Dialogue Dialogue;
    public Animator npcAnimator; // Reference to the NPC's Animator

    // Trigger dialogue for this actor
    public void SpeakTo()
    {
        //DialogueManager.Instance.StartDialogue(Name, Dialogue.RootNode, this);
    }

    // Method to trigger the "hoodUp" anima
    public void TriggerHoodUpAnimation()
    {
        if (npcAnimator != null)
        {
            npcAnimator.SetTrigger("hoodUp");
        }
    }
}
