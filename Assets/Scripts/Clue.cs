using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Clue : MonoBehaviour
{
    public uint cid;

    public GameObject clue;
    public Sprite sprite;
    public SpriteRenderer spriteRenderer;
    
    public string ClueName;
  
    [TextArea(3, 10)]
    public string ClueText;

    public bool Truthiness;

    public GameObject player;

    public Clue(string clueName, string clueText, bool truthiness)
    {
        ClueName   = clueName;
        ClueText   = clueText;
        Truthiness = truthiness;
        clue.transform.position = Vector3.zero;
        clue.sp
        Instantiate(clue);
    }

    private void Update()
    {
        // Check if player is in range, if so open dialog displaying clue
    }
}
