using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ClueSystem : MonoBehaviour
{
    public List<GameObject> fakeClues;
    public List<GameObject> trueClues;
    public List<Clue> foundFakeClues;
    public List<Clue> foundTrueClues;

    public List<Clue> clues;
    // Phone?
    
    
    
    void Start()
    {
        //fakeClues = new(Resources.LoadAll<GameObject>("FakeClues"));
        //trueClues = new(Resources.LoadAll<GameObject>("TrueClues"));
        foundFakeClues = new();
        foundTrueClues = new();
    }
}
