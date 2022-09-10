using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    //public Clue clue;
    //private Dialogue dialogue;
    public Dialogue dialogue;

    public void TriggerDialogue()
    {
        //dialogue.clue = clue.ClueName;
        //dialogue.cluesForQueue[0] = clue.ClueText;
        FindObjectOfType<DialogueManager>().StartDialogue(dialogue);
    }

    public void TriggerDialogue2(Clue clue)
    {
        Dialogue d = new Dialogue();
        d.clue = clue.ClueName;
        d.cluesForQueue = new[] { clue.ClueText };
        FindObjectOfType<DialogueManager>().StartDialogue(d);
    }


}
