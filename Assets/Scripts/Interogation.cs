using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class Interogation : MonoBehaviour
{
    public GameObject q1;
    public GameObject q2;
    public GameObject q3;
    
    private void Start()
    {
        OnMouseDown.
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

    private void ClickCard()
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