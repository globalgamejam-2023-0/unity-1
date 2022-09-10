using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


public class ClueSystem : MonoBehaviour
{
    public GameObject player;
    //public GameObject camera;
    public List<GameObject> clues;
    private SpriteRenderer spriteRenderer;
    public DialogueManager dialogueManager;
    private GameObject currentClue = null;

    // Phone?
    
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        clues = new();
        SpawnClue(300, 170);
        
    }

    void SpawnClue(float x, float y)
    {
        GameObject clueGo = new GameObject();
        clueGo.transform.position = Vector3.zero + new Vector3(x, y, 1);
        clueGo.transform.localScale = new Vector3(0.5f, 0.5f, 1);
        
        Clue clue = clueGo.AddComponent<Clue>();
        clue.go = clueGo;
        
        SpriteRenderer spriteRenderer = clueGo.AddComponent<SpriteRenderer>();
        spriteRenderer.enabled = true;
        spriteRenderer.sprite = Resources.Load<Sprite>("Sprites/IMG_2564");

        clues.Add(clueGo);
    }
    
    private void FixedUpdate()
    {
        spriteRenderer.enabled = false;
        foreach (GameObject clue in clues)
        {
            if ((clue) && ((clue.transform.position - player.transform.position).magnitude < 3.0f))
            {
                // Do something here
                //Debug.Log("Player is close to: ???");
                //TakeClue = alse;
                spriteRenderer.enabled = true;
                currentClue = clue;
                return;
            }
        }

    }

    private void Update()
    {
        if ((Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.E)) && spriteRenderer.enabled)
        {
            Debug.Log("X");
            Clue clue = currentClue.GetComponent<Clue>();
            if (clue != null)
            {
                dialogueManager.TriggerDialogue(clue);
                Destroy(currentClue);
            }

        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            dialogueManager.EndDialogue();
        }
    }

    private void createStory()
    {
        //string clue_vehicle = "Pres"
    }
}
