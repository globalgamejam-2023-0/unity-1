using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;


public class ClueSystem : MonoBehaviour
{
    public GameObject player;

    public TextMeshProUGUI cluesFoundText;
    //public GameObject camera;
    public List<GameObject> clues;
    private SpriteRenderer spriteRenderer;
    public DialogueManager dialogueManager;
    private GameObject currentClue = null;
    private List<string> colours =
        new() { "blue", "green", "red", "yellow", "black", "purple", "orange" };

    private List<string> notz =
        new() { "high treason", "corruption", "mutiny" };

    private List<string> guns =
        new() { "Glock", "AWP", "M4A1-S", "Desert Eagle", "UPS", "Bazooka" };

    private List<string> persons =
        new() { "unknown woman", "mafia boss", "Jacket Rick", "Evil Morty" };

    private List<string> shows =
        new()
        {
            "Stranger Things", "Rings of Power", "Game of Thrones",
            "House of the Dragons"
        };
    
    private List<string> clueTextures = new()
    {
        "Sprites/IMG_2562",
        "Sprites/IMG_2564",
        "Sprites/IMG_2566"
    };

    public List<ClueData> clueDatas      = new();
    public static List<ClueData> cluesPlaced    = new();
    public List<ClueData> clueDatasFound = new();

    // Phone?
    
    void Start()
    {
        clueDatas = createStory();
        spriteRenderer = GetComponent<SpriteRenderer>();
        clues = new();
        GameObject[] spawnPoints =
            GameObject.FindGameObjectsWithTag("clue_spawn_point");

        List<GameObject> spawnPoints2 = new();

        //foreach (var sp in spawnPoints)
        //{
        //    spawnPoints2.Add(sp);
        //}

        spawnPoints2 = spawnPoints.OrderBy(sp => Guid.NewGuid()).ToList();
        
        //int i = 0;
        foreach (ClueData cd in clueDatas)
        {
            //(int, int) pos = cluePositions.First();
            //cluePositions.RemoveAt(0);
            GameObject go = spawnPoints2.First();
            spawnPoints2.RemoveAt(0);
            SpawnClue(go.transform.position.x, go.transform.position.y, cd);
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
        //spriteRenderer.sprite = Resources.Load<Sprite>("Sprites/IMG_2564");
        spriteRenderer.sprite = Resources.Load<Sprite>("Sprites/" + clueData.graphic.Item1);
        //spriteRenderer.sprite = Resources.Load<Sprite>(randomClueTexture());

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
        cluesFoundText.SetText($"Clues found: {clueDatasFound.Count()}/{cluesPlaced.Count()}");
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

        if (Input.GetKeyDown(KeyCode.Escape) && dialogueManager.animator.GetBool("IsOpen"))
        {
            dialogueManager.EndDialogue();
        }

        if (Input.GetKeyDown(KeyCode.O))
        {
            Debug.Log("Questioning scene");
            //Debug.Log($"Size of cluesPlaced: {cluesPlaced.Count()}");
            //DataSaver.saveData(cluesPlaced, "cluesPlaced");
            SceneManager.LoadScene("questions");
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

    private List<string> randomizedList(List<string> list)
    {
        return list.OrderBy(item => Guid.NewGuid()).ToList();
    }
    
    
    
    private List<ClueData> createStory()
    {
        List<ClueData> clueData = new();

        ClueData cd0 = createClueData(
            colours,
            "President's car",
            "The president was seen in a {0} car",
            "What colour was the president's car?",
            3,
            true);
        clueData.Add(cd0);

        ClueData cd1 = createClueData(
            notz,
            "A corrupt president",
            "Mr. President, you are corrupt, you're convicted of {0}",
            "What was the president convicted of?",
            3,
            true);
        clueData.Add(cd1);

        ClueData cd2 = createClueData(
            guns,
            "A trigger happy president",
            "The president aimed a {0} at a person",
            "Which gun was the president wielding?",
            3,
            true);
        clueData.Add(cd2);
        
        ClueData cd3 = createClueData(
            persons,
            "The president's cabal",
            "{0} was seen with the president in a dark alley",
            "Who was the president seen with?",
            3,
            true);
        clueData.Add(cd3);

        ClueData cd4 = createClueData(
            shows,
            "Strange preferences",
            "{0} is the President's favourite show.",
            "Which show does the President like?",
            3,
            true);
        clueData.Add(cd4);
        
        return clueData.OrderBy(cd => Guid.NewGuid()).ToList();
    }

    private ClueData createClueData(List<string> list, string clueName,
        string clueText, string clueQuestion, int answers, bool truthiness)
    {
        ClueData cd = new();
        cd.adjectives = randomizedList(list);
        cd.adjective = cd.adjectives.First();
        cd.adjectives.RemoveAt(0);
        cd.clueName = clueName;
        cd.clueText = clueText;
        cd.question = clueQuestion;
        cd.answers = answers;
        cd.graphic = Statics.randomClueGraphics();
        cd.truthiness = truthiness;
        return cd;
    }
    
    public string clueName()
    {
        int tmp = Random.Range(100000, 999999);
        return "clue" + tmp;
    }

    private string randomClueTexture()
    {
        return clueTextures.OrderBy(t => Guid.NewGuid()).ToList().First();
    }
}
