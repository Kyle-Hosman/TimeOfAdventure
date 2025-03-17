using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    public GameObject chatPanel;
    private bool canInteract = false;
    private GameObject currentNPC; // Stores the NPC the player is near

    void Update()
    {
        if (canInteract && Input.GetKeyDown(KeyCode.E)) // Press 'E' to interact
        {
            if (currentNPC != null)
            {
                chatPanel.SetActive(true);
            }
        }

        // Close the chat with Z key
        if (chatPanel.activeSelf && Input.GetKeyDown(KeyCode.Z))
        {
            chatPanel.SetActive(false);
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
