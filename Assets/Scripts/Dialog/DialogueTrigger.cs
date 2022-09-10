using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    public Clue clue;
    private Dialogue dialogue;

    public void TriggerDialogue()
    {
        dialogue.clue = clue.ClueName;
        dialogue.cluesForQueue[0] = clue.ClueText;
        FindObjectOfType<DialogueManager>().StartDialogue(dialogue);


    }


}
