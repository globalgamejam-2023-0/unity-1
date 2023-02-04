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

    void Awake()
    {
        width = (float)Screen.width / 2.0f;
        height = (float)Screen.height / 2.0f;

        // Position used for the cube.
        position = new Vector3(0.0f, 0.0f, 0.0f);
    }

    void OnGUI()
    {
        // Compute a fontSize based on the size of the screen width.
        GUI.skin.label.fontSize = (int)(Screen.width / 25.0f);

        GUI.Label(new Rect(20, 20, width, height * 0.25f),
            "x = " + position.x.ToString("f2") +
            ", y = " + position.y.ToString("f2"));
    }
    
    // Start is called before the first frame update
    void Start()
    {
        body = gameObject.GetComponent<Rigidbody2D>();
        Debug.Log("test");
    }

    // Update is called once per frame
    void Update()
    {
        // Handle screen touches.
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            // Move the cube if the screen has the finger moving.
            if (touch.phase == TouchPhase.Moved)
            {
                Vector2 pos = touch.position;
                pos.x = (pos.x - width) / width;
                pos.y = (pos.y - height) / height;
                position = new Vector3(-pos.x, pos.y, 0.0f);

                // Position the cube.
                transform.position = position;
            }

            if (Input.touchCount == 2)
            {
                touch = Input.GetTouch(1);

                if (touch.phase == TouchPhase.Began)
                {
                    // Halve the size of the cube.
                    transform.localScale = new Vector3(0.75f, 0.75f, 0.75f);
                }

                if (touch.phase == TouchPhase.Ended)
                {
                    // Restore the regular size of the cube.
                    transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
                }
            }
        }

        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        animator.SetFloat("Horizontal", movement.x);
        animator.SetFloat("Vertical", movement.y);
        animator.SetFloat("Speed", movement.sqrMagnitude);
    }

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
