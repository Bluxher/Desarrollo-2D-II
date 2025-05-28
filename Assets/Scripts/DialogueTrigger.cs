using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    public GameObject keyPromptUI;
    public GameObject dialoguePanel;
    public TMP_Text dialogueTextUI;

    [TextArea(2, 5)]
    public List<string> dialogueLines;

    private bool playerInRange = false;
    private bool dialogueActive = false;
    private int currentLine = 0;

    void Start()
    {
        keyPromptUI.SetActive(false);
        dialoguePanel.SetActive(false);
    }

    void Update()
    {
        if (playerInRange && !dialogueActive && Input.GetKeyDown(KeyCode.E))
        {
            StartDialogue();
        }

        if (dialogueActive && Input.GetMouseButtonDown(0))
        {
            AdvanceDialogue();
        }
    }

    void StartDialogue()
    {
        dialogueActive = true;
        currentLine = 0;
        keyPromptUI.SetActive(false);
        dialoguePanel.SetActive(true);
        dialogueTextUI.text = dialogueLines[currentLine];
    }

    void AdvanceDialogue()
    {
        currentLine++;
        if (currentLine < dialogueLines.Count)
        {
            dialogueTextUI.text = dialogueLines[currentLine];
        }
        else
        {
            EndDialogue();
        }
    }

    void EndDialogue()
    {
        dialogueActive = false;
        dialoguePanel.SetActive(false);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            keyPromptUI.SetActive(true);
            playerInRange = true;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            keyPromptUI.SetActive(false);
            EndDialogue();
            playerInRange = false;
        }
    }
}
