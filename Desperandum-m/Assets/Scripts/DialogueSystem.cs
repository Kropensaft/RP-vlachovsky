using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialogueSystem : MonoBehaviour
{
    public GameObject dialogueBox;
    public TMP_Text dialogueText;
    public GameObject backgroundOverlay;

    public List<string> phase1DialogueLines;
    public List<string> phase2DialogueLines;
    public List<string> phase3DialogueLines;

    private int currentPhaseIndex = 1;
    private int currentDialogueIndex = 0;
    private bool dialogueActive = false;

    // Start is called before the first frame update
    void Start()
    {
        dialogueBox.SetActive(false);
        backgroundOverlay.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (dialogueActive)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                currentDialogueIndex++;
                if (currentDialogueIndex >= GetCurrentPhaseDialogueLines().Count)
                {
                    EndDialogue();
                }
                else
                {
                    UpdateDialogueText();
                }
            }
        }
    }

    public void StartDialogue()
    {
        if (!IsCurrentPhaseDialogueEmpty())
        {
            dialogueActive = true;
            dialogueBox.SetActive(true);
            backgroundOverlay.SetActive(true);
            currentDialogueIndex = 0;
            UpdateDialogueText();
        }
    }

    public void EndDialogue()
    {
        dialogueActive = false;
        dialogueBox.SetActive(false);
        backgroundOverlay.SetActive(false);

        if (currentPhaseIndex < 3)
        {
            currentPhaseIndex++;
            currentDialogueIndex = 0;
        }
        else
        {
            currentPhaseIndex = 1;
        }
    }

    private void UpdateDialogueText()
    {
        dialogueText.text = GetCurrentPhaseDialogueLines()[currentDialogueIndex];
    }

    private List<string> GetCurrentPhaseDialogueLines()
    {
        switch (currentPhaseIndex)
        {
            case 1:
                return phase1DialogueLines;
            case 2:
                return phase2DialogueLines;
            case 3:
                return phase3DialogueLines;
            default:
                Debug.LogError("Invalid current phase index!");
                return null;
        }
    }

    private bool IsCurrentPhaseDialogueEmpty()
    {
        switch (currentPhaseIndex)
        {
            case 1:
                return phase1DialogueLines.Count == 0;
            case 2:
                return phase2DialogueLines.Count == 0;
            case 3:
                return phase3DialogueLines.Count == 0;
            default:
                Debug.LogError("Invalid current phase index!");
                return true;
        }
    }
}
