using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private int walkSpeed = 4;
    private Rigidbody2D body;
    //private float inputH;
    //private float inputV;
    public Animator animator;

    public AudioSource audio;
    public AudioClip audioWalk;

    Vector2 movement;

    private Vector3 position;
    private float width;
    private float height;
    
    // Start is called before the first frame update
    void Start()
    {
        body = gameObject.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    // void Update()
    // {
    //     movement.x = Input.GetAxisRaw("Horizontal");
    //     movement.y = Input.GetAxisRaw("Vertical");

    //     Debug.Log(movement);

    //     animator.SetFloat("Horizontal", movement.x);
    //     animator.SetFloat("Vertical", movement.y);
    //     animator.SetFloat("Speed", movement.sqrMagnitude);
    // }

    void FixedUpdate()
    {
        if (movement.x != 0 || movement.y != 0)
        {
            body.MovePosition(body.position + movement * walkSpeed * Time.fixedDeltaTime);

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
