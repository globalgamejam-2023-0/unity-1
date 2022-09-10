using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;


public class ClueSystem : MonoBehaviour
{
    public GameObject player;
    //public GameObject camera;
    public List<GameObject> clues;
    private SpriteRenderer spriteRenderer;
    public DialogueManager dialogueManager;
    private GameObject currentClue = null;
    private List<string> colours =
        new() { "blue", "green", "red", "yellow", "black", "purple", "orange" };

    private List<string> notz =
        new() { "not", "" };

    private List<(int, int)> cluePositions =
        new() { (300, 170), (330, 140) };

    public List<ClueData> clueDatas      = new();
    public static List<ClueData> cluesPlaced    = new();
    public List<ClueData> clueDatasFound = new();

    // Phone?
    
    void Start()
    {
        clueDatas = createStory();
        spriteRenderer = GetComponent<SpriteRenderer>();
        clues = new();
        foreach (ClueData cd in clueDatas)
        {
            (int, int) pos = cluePositions.First();
            cluePositions.RemoveAt(0);
            SpawnClue(pos.Item1, pos.Item2, cd);
            cluesPlaced.Add(cd);
        }
        //SpawnClue(300, 170);
        
    }

    void SpawnClue(float x, float y, ClueData clueData)
    {
        GameObject clueGo = new GameObject();
        clueGo.name = clueName();
        clueGo.transform.position = Vector3.zero + new Vector3(x, y, 1);
        clueGo.transform.localScale = new Vector3(0.5f, 0.5f, 1);
        
        Clue clue = clueGo.AddComponent<Clue>();
        clue.go = clueGo;
        clue.clueData = clueData;
        
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
                clueDatasFound.Add(clue.clueData);
                Destroy(currentClue);
            }

        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            dialogueManager.EndDialogue();
        }

        if (Input.GetKeyDown(KeyCode.O))
        {
            Debug.Log("Questioning scene");
            //Debug.Log($"Size of cluesPlaced: {cluesPlaced.Count()}");
            //DataSaver.saveData(cluesPlaced, "cluesPlaced");
            SceneManager.LoadScene("QuestioningScene_dev");
        }
    }

    private List<string> randomizedColours()
    {
        return colours.OrderBy(i => Guid.NewGuid()).ToList();
    }

    private List<string> randomizedNotz()
    {
        return notz.OrderBy(i => Guid.NewGuid()).ToList();
    }
    
    private List<ClueData> createStory()
    {
        List<ClueData> clueData = new();

        ClueData cd0 = new();
        cd0.adjectives = randomizedColours();
        cd0.adjective = cd0.adjectives.First();
        cd0.adjectives.RemoveAt(0);
        cd0.clueName = "President's car.";
        cd0.clueText = "The president was seen in a {0} car.";
        cd0.question = "What colour was the president's car?";
        cd0.answers = 3;
        clueData.Add(cd0);

        ClueData cd1 = new();
        cd1.adjectives = randomizedNotz();
        cd1.adjective = cd1.adjectives.First();
        cd1.adjectives.RemoveAt(0);
        cd1.clueName = "A corrupt president.";
        cd1.clueText = "Mr. President, you are {0} corrupt!";
        cd1.question = "Is the president guilty?";
        cd1.answers = 2;
        clueData.Add(cd1);

        return clueData.OrderBy(cd => Guid.NewGuid()).ToList();
    }
    
    public string clueName()
    {
        int tmp = Random.Range(100000, 999999);
        return "clue" + tmp;
    }
}
