using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Clue : MonoBehaviour
{
    public uint cid;

    public GameObject go { get; set; }
    
    public string ClueName;
  
    [TextArea(3, 10)]
    public string ClueText;

    public bool Truthiness;

    public Clue() : this("TestClue", "This is a test clue", true) {}
    
    public Clue(string clueName, string clueText, bool truthiness)
    {
        ClueName   = clueName;
        ClueText   = clueText;
        Truthiness = truthiness;
        //go.transform.position = Vector3.zero;
        Debug.Log("I WAS CREATED!!");
    }

    public void Init(GameObject go)
    {
        this.go = go;
    }
}
