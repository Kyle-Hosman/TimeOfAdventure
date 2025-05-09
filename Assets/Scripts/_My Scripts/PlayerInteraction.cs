using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    public GameObject chatPanel;
    private bool canInteract = false;
    private GameObject currentNPC; // Stores the NPC the player is near

    void Update()
    {
        if (canInteract && Input.GetKeyDown(KeyCode.E)) 
        {
            // Check if the player is near an NPC and trigger the dialogue
            if (currentNPC != null)
            {
                Actor actor = currentNPC.GetComponent<Actor>();
                if (actor != null)
                {
                    actor.SpeakTo();
                }
            }
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("NPC"))
        {
            canInteract = true;
            currentNPC = other.gameObject;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("NPC"))
        {
            canInteract = false;
            currentNPC = null;
        }
    }
}
