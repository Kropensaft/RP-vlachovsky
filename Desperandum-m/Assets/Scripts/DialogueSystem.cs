using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections.Generic;
using TMPro;




public class DialogueSystem : MonoBehaviour
{
    [SerializeField] public string firstLine;
    [SerializeField] public string secondLine;
    [SerializeField] public string thirdLine;
    [SerializeField] public string fourthLine;
    [SerializeField] public string fifthLine;
    [SerializeField] public string sixthLine;

    [SerializeField] private GameObject dialogueBox;
    [SerializeField] private TextMeshProUGUI dialogueText;
    [SerializeField] private GameObject backgroundOverlay;
    [SerializeField] private KeyCode skipButton = KeyCode.E;
    
    private bool dialogueActive = false;
    public bool dialogueEnded;
    private bool coroutineActive;

    private void Start()
    {
        
        
    }

    private void Update()
    {
        

        // Check if dialogue is active
        if (dialogueActive)
        {
            // Check if player pressed skip button
            if (!dialogueActive || !coroutineActive)
            {
                EndDialogue(); 
            }
        }
    }

    public IEnumerator<string> DisplayDialogue(List<string> dialogue, TMP_Text dialogueText, float durationPerLine)
    {
        int index = 0;
        coroutineActive = true;
        dialogueText.text = ""; // Reset the dialogue text

        while (index < dialogue.Count)
        {
            string[] lines = dialogue[index].Split('\n');

            for (int i = 0; i < lines.Length; i++)
            {
                dialogueText.text = lines[i];
                bool lineDisplayed = false;

                while (!lineDisplayed)
                {
                    float elapsedTime = 0f;
                    elapsedTime += Time.deltaTime;

                    if (elapsedTime >= durationPerLine || Input.GetKeyDown(skipButton))
                    {
                        lineDisplayed = true;
                    }

                    yield return null;
                }
            }

            index++;
        }

        coroutineActive = false;
        index = 0;
    }

    public void StartDialogue(int currentPhaseIndex)
    {
        if (currentPhaseIndex < 0 || currentPhaseIndex >= 6)
        {
            Debug.LogError("Invalid current phase index: " + currentPhaseIndex);
            return;
        }

        Debug.Log("Starting dialogue for phase " + currentPhaseIndex);

        dialogueEnded = false;
        dialogueActive = true;
        coroutineActive = true;
        dialogueBox.SetActive(true);
        backgroundOverlay.SetActive(true);

        List<string> phase1Lines = new List<string>
        {
            firstLine,
            secondLine
        };
        List<string> phase2Lines = new List<string>
        {
            thirdLine,
            fourthLine
        }; List<string> phase3Lines = new List<string>
        {
            fifthLine,
            sixthLine
        };

        if(currentPhaseIndex == 0)
        StartCoroutine(DisplayDialogue(phase1Lines, dialogueText, 5));
        if (currentPhaseIndex == 1)
            StartCoroutine(DisplayDialogue(phase2Lines, dialogueText, 5));
        if (currentPhaseIndex == 2)
            StartCoroutine(DisplayDialogue(phase3Lines, dialogueText, 5));
    }

    public void EndDialogue()
    {
        dialogueEnded = true;
        dialogueActive = false;
        dialogueBox.SetActive(false);
        backgroundOverlay.SetActive(false);

        // Go to the next phase if there is one
      
    }
}
