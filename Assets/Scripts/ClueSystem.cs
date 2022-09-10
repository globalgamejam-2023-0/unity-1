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
    public Sprite circle;
    private SpriteRenderer spriteRenderer;

    // Phone?
    
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        clues = new();
        SpawnClue(2, 3);
    }

    void SpawnClue(float x, float y)
    {
        GameObject clueGo = new GameObject();
        clueGo.transform.position = Vector3.zero + new Vector3(x, y, 0);
        
        Clue clue = clueGo.AddComponent<Clue>();
        clue.go = clueGo;
        
        SpriteRenderer spriteRenderer = clueGo.AddComponent<SpriteRenderer>();
        spriteRenderer.color = Color.green;
        spriteRenderer.size = new Vector2(2, 2);
        spriteRenderer.enabled = true;
        spriteRenderer.sprite = circle;
        
        clues.Add(clueGo);
    }
    
    private void FixedUpdate()
    {
        spriteRenderer.enabled = false;
        foreach (GameObject clue in clues)
        {
            if ((clue.transform.position - player.transform.position).magnitude <
                1.0f)
            {
                // Do something here
                //Debug.Log($"Player is close to: ???");
                //TakeClue = alse;
                spriteRenderer.enabled = true;
                return;
            }
        }
    }

    private void createStory()
    {
        //string clue_vehicle = "Pres"
    }
}
