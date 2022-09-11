using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class Interogation : MonoBehaviour
{
    public GameObject q1;
    public GameObject q2;
    public GameObject q3;

    public TextMeshProUGUI adj1;
    public TextMeshProUGUI adj2;
    public TextMeshProUGUI adj3;
    public TextMeshProUGUI question;
    //private List<ClueData> cluesPlaced;
    
    private void Start()
    {
        //cluesPlaced = DataSaver.loadData<List<ClueData>>("cluesPlaced");
        
        ClueData cd = ClueSystem.cluesPlaced.First();
        List<string> cds = new();
        cds.Add(cd.adjective);
        cds.Add(cd.adjectives.First());
        cd.adjectives.RemoveAt(0);
        cds.Add(cd.adjectives.First());
        cds = cds.OrderBy(answer => Guid.NewGuid()).ToList();
        adj1.SetText(cds.First());
        cds.RemoveAt(0);
        adj2.SetText(cds.First());
        cds.RemoveAt(0);
        adj3.SetText(cds.First());
        cds.RemoveAt(0);
        /*
        cds.Add(cd.adjective);
        cds.Add(cd.adjectives.First());
        cds.RemoveAt(0);
        cds.Add(cds.First());
        cds = cds.OrderBy(c => Guid.NewGuid()).ToList();

        adj1.SetText(cds[0]);
        question.SetText(cd.question);
        */
        question.SetText(cd.question);

        //OnMouseDown.
    }

    private void FixedUpdate()
    {
        //Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        //RaycastHit2D hit =
        //    Physics2D.Raycast(ray.origin, ray.direction, Mathf.Infinity);
        //if (hit)
        //{
        //    Debug.Log(hit.collider.GameObject().name);
        //}
    }

    private void Update()
    {
        ClickCard();
    }

    private void ClickCard()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit2D hit =
                Physics2D.Raycast(ray.origin, ray.direction, Mathf.Infinity);
            if (hit)
            {
                Debug.Log(hit.collider.GameObject().name);
            }
        }
    }
}