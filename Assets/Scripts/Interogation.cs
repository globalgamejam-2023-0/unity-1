using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Interogation : MonoBehaviour
{
    public TextMeshProUGUI adj1;
    public TextMeshProUGUI adj2;
    public TextMeshProUGUI adj3;
    public TextMeshProUGUI question;
    public Button button1;
    public Button button2;
    public Button button3;
    
    private void Start()
    {
        setupQuestion();
    }

    private void setupQuestion()
    {
        ClueData cd = ClueSystem.cluesPlaced.First();
        List<(bool, string)> cds = new();
        cds.Add((true, cd.adjective));
        cds.Add((false, cd.adjectives.First()));
        cd.adjectives.RemoveAt(0);
        cds.Add((false, cd.adjectives.First()));
        cds = cds.OrderBy(answer => Guid.NewGuid()).ToList();
        
        adj1.SetText(cds.First().Item2);
        setupButton(cds.First(), button1);
        cds.RemoveAt(0);
        
        adj2.SetText(cds.First().Item2);
        setupButton(cds.First(), button2);
        cds.RemoveAt(0);
        
        adj3.SetText(cds.First().Item2);
        setupButton(cds.First(), button3);
        cds.RemoveAt(0);
        
        question.SetText(cd.question);
    }

    private void setupButton((bool, string) answer, Button button)
    {
        if (answer.Item1)
        {
            button.onClick.AddListener(() =>
            {
                Debug.Log($"The answer was correct");
                SceneManager.LoadScene("Scenes/end_scenes/jailed");
            });
            return;
        }
        
        button.onClick.AddListener(() =>
        {
            Debug.Log($"The answer was incorrect");
            SceneManager.LoadScene("Scenes/end_scenes/jailed");
        });
    }
}