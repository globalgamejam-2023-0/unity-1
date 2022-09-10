using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class DialogueManager : MonoBehaviour
{
    public Clue clue;
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI dialogueText;

    public Animator animator;


    private Queue<string> clues; // first in first out

    private void Start()
    {
        clues = new Queue<string>();
        
        
    }

    public void StartDialogue (Dialogue dialogue)
    {
        //Debug.Log("Starting dialogue");

        animator.SetBool("IsOpen",true);

        nameText.text = dialogue.clue;

        clues.Clear();

        foreach (string clue in dialogue.cluesForQueue)
        {
            clues.Enqueue(clue);

        }

        DisplayNextClue(); 

    }

    public void DisplayNextClue()
    {
        if (clues.Count == 0)
        {
            EndDialogue();
            return;
        }

        string clue = clues.Dequeue();
        dialogueText.text = clue;

        Debug.Log(clue);

    }

    public void EndDialogue()
    {
        Debug.Log("remove Dialogue");
        animator.SetBool("IsOpen",false);
    }

}
