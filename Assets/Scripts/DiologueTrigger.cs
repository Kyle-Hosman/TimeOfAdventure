using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    public DialogueLine[] dialogueLines;

    private bool playerInRange;

    void Update()
    {
        if (playerInRange && Input.GetKeyDown(KeyCode.E))
        {
            DialogueManager.Instance.StartDialogue(dialogueLines);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerInRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerInRange = false;
        }
    }
}