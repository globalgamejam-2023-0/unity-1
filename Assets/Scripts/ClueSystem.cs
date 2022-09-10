using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


public class ClueSystem : MonoBehaviour
{
    public GameObject player;
    public List<Clue> clues;
    // Phone?
    
    void Start()
    {
        clues = new();
        clues.Add(new Clue("TestClue", "This clue is a test", true));
    }

    private void FixedUpdate()
    {
        foreach (var clue in clues)
        {
            if ((clue.clue.transform.position - player.transform.position).magnitude <
                10.0f)
            {
                // Do something here
                Debug.Log($"Player is close to: {clue.ClueName}");
            }
        }
    }
}
