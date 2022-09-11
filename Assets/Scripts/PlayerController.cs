using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private int walkSpeed = 4;
    private Rigidbody2D body;
    private float inputH;
    private float inputV;

    public AudioSource audio;
    public AudioClip audioWalk;
    
    // Start is called before the first frame update
    void Start()
    {
        body = gameObject.GetComponent<Rigidbody2D>();
        Debug.Log("test");
    }

    // Update is called once per frame
    void Update()
    {
        inputH = Input.GetAxisRaw("Horizontal");
        inputV = Input.GetAxisRaw("Vertical");
    }

    void FixedUpdate()
    {
        if (inputH != 0 || inputV != 0)
        {
            body.velocity = new Vector2(inputH * walkSpeed, inputV * walkSpeed);

            if (!audio.isPlaying) {
                audio.clip = audioWalk;
                audio.Play();
            }
        }
        else
        {
            body.velocity = new Vector2(0f, 0f);

            if (audio.isPlaying) {
                audio.Stop();
            }
        }
        
    }
}
