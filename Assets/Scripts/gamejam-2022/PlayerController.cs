using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Direction {
    UP,
    DOWN,
    LEFT,
    RIGHT,
}

public class PlayerController : MonoBehaviour
{
    [SerializeField] private int walkSpeed = 7;
    private Rigidbody2D body;
    //private float inputH;
    //private float inputV;
    public Animator animator;

    public AudioSource audio;
    public AudioClip audioWalk;

    public Vector2 movement;

    private Vector3 position;
    private float width;
    private float height;

    public void MoveVec(float x, float y) {
        if (movement.y == -1 && y == 1) {
            y = -1;
        }
        else if (movement.y == 1 && y == -1) {
            y = 1;
        }

        if (movement.x == -1 && x == 1) {
            x = -1;
        }
        else if (movement.x == 1 && x == -1) {
            x = 1;
        }

        movement.x = x;
        movement.y = y;

        animator.SetFloat("Horizontal", movement.x);
        animator.SetFloat("Vertical", movement.y);
        animator.SetFloat("Speed", movement.sqrMagnitude);
    }

    public void Move(Direction direction) {
        if (direction == Direction.UP) {
            MoveVec(0, 1);
        }
        else if (direction == Direction.DOWN) {
            MoveVec(0, -1);
        }
        else if (direction == Direction.LEFT) {
            MoveVec(-1, 0);
        }
        else if (direction == Direction.RIGHT) {
            MoveVec(1, 0);
        }
    }
    
    // Start is called before the first frame update
    void Start()
    {
        body = gameObject.GetComponent<Rigidbody2D>();

        Move(Direction.DOWN);
    }

    // Update is called once per frame
    void Update()
    {
        // movement.x = Input.GetAxisRaw("Horizontal");
        // movement.y = Input.GetAxisRaw("Vertical");

        // if (movement.sqrMagnitude != 0) {
        //     animator.SetFloat("Horizontal", movement.x);
        //     animator.SetFloat("Vertical", movement.y);
        //     animator.SetFloat("Speed", movement.sqrMagnitude);
        // }
    }

    void FixedUpdate()
    {
        body.MovePosition(body.position + movement * walkSpeed * Time.fixedDeltaTime);

        // if (movement.sqrMagnitude != 0)
        // {
        //     body.MovePosition(body.position + movement * walkSpeed * Time.fixedDeltaTime);

        //     if (!audio.isPlaying) {
        //         audio.clip = audioWalk;
        //         audio.Play();
        //     }
        // }

        // if (movement.x != 0 || movement.y != 0)
        // {
        //     body.MovePosition(body.position + movement * walkSpeed * Time.fixedDeltaTime);

        //     if (!audio.isPlaying) {
        //         audio.clip = audioWalk;
        //         audio.Play();
        //     }
        // }
        // else
        // {
        //     body.velocity = new Vector2(0f, 0f);

        //     if (audio.isPlaying) {
        //         audio.Stop();
        //     }
        // }
    }
}
