using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager Instance;

    [SerializeField] private GameObject dialoguePanel;
    [SerializeField] private TextMeshProUGUI dialogueText;
    [SerializeField] private GameObject continueIcon;

    [Header("Choice System")]
    [SerializeField] private GameObject choicePanel; 
    [SerializeField] private Button[] choiceButtons;

    private Queue<DialogueLine> dialogueLines;
    private bool isDialogueActive = false;

    void Awake()
    {
        dialoguePanel.SetActive(false);
        if (Instance == null) Instance = this;
        else Destroy(gameObject);

        dialogueLines = new Queue<DialogueLine>();
    }

    public void StartDialogue(DialogueLine[] lines)
    {
        Debug.Log("Starting dialogue...");
        dialoguePanel.SetActive(true);
        choicePanel.SetActive(false);
        dialogueLines.Clear();
        isDialogueActive = true;

        foreach (DialogueLine line in lines)
        {
            Debug.Log("Enqueuing line: " + line.line);
            dialogueLines.Enqueue(line);
        }

        DisplayNextSentence();
    }

    public void DisplayNextSentence()
    {
        if (dialogueLines.Count == 0)
        {
            Debug.Log("No more sentences. Ending dialogue.");
            EndDialogue();
            return;
        }

        DialogueLine dialogueLine = dialogueLines.Dequeue();
        Debug.Log("Displaying sentence: " + dialogueLine.line);
        
        StopAllCoroutines();
        StartCoroutine(TypeSentence(dialogueLine.line));

        if (dialogueLine.choices != null && dialogueLine.choices.Length > 0)
        {
            choicePanel.SetActive(true);
            ShowChoices(dialogueLine.choices);
        }
        else
        {
            choicePanel.SetActive(false);
        }
    }

    IEnumerator TypeSentence(string sentence)
    {
        dialogueText.text = "";
        continueIcon.SetActive(false);

        foreach (char letter in sentence.ToCharArray())
        {
            dialogueText.text += letter;
            Debug.Log("Adding letter: " + letter);
            yield return new WaitForSeconds(0.02f);
        }

        continueIcon.SetActive(true);
        Debug.Log("Finished typing sentence.");
    }

    private void ShowChoices(string[] choices)
    {
        Debug.Log("Showing choices...");
        for (int i = 0; i < choiceButtons.Length; i++)
        {
            if (i < choices.Length)
            {
                choiceButtons[i].gameObject.SetActive(true);
                int choiceIndex = i; // Capture index for lambda function
                choiceButtons[i].onClick.RemoveAllListeners();
                choiceButtons[i].onClick.AddListener(() => SelectChoice(choiceIndex));
                choiceButtons[i].GetComponentInChildren<TextMeshProUGUI>().text = choices[i];
                Debug.Log("Setting choice button " + i + " text to: " + choices[i]);
            }
            else
            {
                choiceButtons[i].gameObject.SetActive(false);
            }
        }
    }

    private void SelectChoice(int choiceIndex)
    {
        Debug.Log("Player selected choice index: " + choiceIndex);

        choicePanel.SetActive(false);
        EndDialogue();
    }

    void EndDialogue()
    {
        Debug.Log("Ending dialogue...");
        dialoguePanel.SetActive(false);
        isDialogueActive = false;
    }

    void Update()
    {
        if (isDialogueActive && Input.GetKeyDown(KeyCode.Z) && choicePanel.activeSelf == false)
        {
            DisplayNextSentence();
        }
    }
}