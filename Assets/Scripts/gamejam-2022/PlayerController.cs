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
    [SerializeField] private int walkSpeed = 100;
    private Rigidbody2D body;
    private BoxCollider2D boxCollider;
    //private float inputH;
    //private float inputV;
    public Animator animator;

    public AudioSource audio;
    public AudioClip audioWalk;

    public Vector2 movement;

    private Vector3 position;
    private float width;
    private float height;

    private Coroutine coroutine;

    private Direction currentDirection;
    private Direction lastDirection;

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

        currentDirection = direction;
    }
    
    void SpawnTrail(float x, float y)
    {
        // GameObject myLine = new GameObject();
        // myLine.transform.position = start;
        // myLine.AddComponent<LineRenderer>();
        // LineRenderer lr = myLine.GetComponent<LineRenderer>();
        // lr.material = new Material(Shader.Find("Particles/Alpha Blended Premultiply"));
        // lr.SetColors(color, color);
        // lr.SetWidth(0.1f, 0.1f);
        // lr.SetPosition(0, start);
        // lr.SetPosition(1, end);
        // GameObject.Destroy(myLine, duration);

        GameObject clueGo = new GameObject();
        clueGo.name = "Trail";
        clueGo.transform.position = Vector3.zero + new Vector3(x, y, 1f);
        clueGo.transform.localScale = new Vector3(0.2777778f, 0.2777778f, 1f);
        
        // var clue = clueGo.AddComponent<GameObject>();
        // clue.go = clueGo;
        // clue.clueData = clueData;
        
        var spriteRenderer = clueGo.AddComponent<SpriteRenderer>();
        spriteRenderer.enabled = true;

        if (currentDirection == Direction.UP) {
            if (lastDirection == Direction.LEFT) {
                spriteRenderer.sprite = Resources.Load<Sprite>("Sprites/ggj-2023/Root/7");
            }
            else if (lastDirection == Direction.RIGHT) {
                spriteRenderer.sprite = Resources.Load<Sprite>("Sprites/ggj-2023/Root/5");
            }
            else {
                spriteRenderer.sprite = Resources.Load<Sprite>("Sprites/ggj-2023/Root/8");
            }
        }
        else if (currentDirection == Direction.DOWN) {
            if (lastDirection == Direction.LEFT) {
                spriteRenderer.sprite = Resources.Load<Sprite>("Sprites/ggj-2023/Root/1");
            }
            else if (lastDirection == Direction.RIGHT) {
                spriteRenderer.sprite = Resources.Load<Sprite>("Sprites/ggj-2023/Root/3");
            }
            else {
                spriteRenderer.sprite = Resources.Load<Sprite>("Sprites/ggj-2023/Root/4");
            }
        }
        else if (currentDirection == Direction.LEFT) {
            if (lastDirection == Direction.UP) {
                spriteRenderer.sprite = Resources.Load<Sprite>("Sprites/ggj-2023/Root/3");
            }
            else if (lastDirection == Direction.DOWN) {
                spriteRenderer.sprite = Resources.Load<Sprite>("Sprites/ggj-2023/Root/5");
            }
            else {
               spriteRenderer.sprite = Resources.Load<Sprite>("Sprites/ggj-2023/Root/6");
            }
        }
        else if (currentDirection == Direction.RIGHT) {
            if (lastDirection == Direction.UP) {
                spriteRenderer.sprite = Resources.Load<Sprite>("Sprites/ggj-2023/Root/1");
            }
            else if (lastDirection == Direction.DOWN) {
                spriteRenderer.sprite = Resources.Load<Sprite>("Sprites/ggj-2023/Root/7");
            }
            else {
                spriteRenderer.sprite = Resources.Load<Sprite>("Sprites/ggj-2023/Root/2");
            }
        }
        
        //spriteRenderer.sprite = Resources.Load<Sprite>("Sprites/IMG_2564");
        // spriteRenderer.sprite = Resources.Load<Sprite>("Sprites/" + clueData.graphic.Item1);
        //spriteRenderer.sprite = Resources.Load<Sprite>(randomClueTexture());
        spriteRenderer.sortingOrder = 1;

        var boxCollider = clueGo.AddComponent<BoxCollider2D>();
        // boxCollider.size = new Vector2(4, 4);

        // var rigidBody = clueGo.AddComponent<Rigidbody2D>();
        // rigidBody.isKinematic = true;

        // clues.Add(clueGo);

        lastDirection = currentDirection;
    }

    void OnCollisionEnter2D(Collision2D other) {
        Debug.Log(other.gameObject.name);
        if (other.gameObject.name.Contains("item-")) {
            Destroy(other.gameObject);
        }
    }
    
    // Start is called before the first frame update
    void Start()
    {
        body = gameObject.GetComponent<Rigidbody2D>();
        boxCollider = gameObject.GetComponent<BoxCollider2D>();

        Move(Direction.DOWN);

        coroutine = StartCoroutine(LimitedUpdate());
    }

    private IEnumerator LimitedUpdate() {
        while(true) {
            var prevPos = body.position;

            // var tempPos = body.position + movement * walkSpeed * Time.fixedDeltaTime;
            var tempPos = body.position + movement * boxCollider.size;
            tempPos.x = (int)tempPos.x;
            tempPos.y = (int)tempPos.y;
            body.MovePosition(tempPos);

            animator.SetFloat("Horizontal", movement.x);
            animator.SetFloat("Vertical", movement.y);
            animator.SetFloat("Speed", movement.sqrMagnitude);

            Debug.Log(tempPos);
            Debug.Log(boxCollider.size);

            yield return new WaitForSeconds(0.013f);

            SpawnTrail(prevPos.x, prevPos.y);

            yield return new WaitForSeconds(0.500f);
        }
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
        // body.MovePosition(body.position + movement * walkSpeed * Time.fixedDeltaTime);

        // if ((int)body.position.y % 1 == 0) {
        //     SpawnTrail(body.position.x, body.position.y);
        // }

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