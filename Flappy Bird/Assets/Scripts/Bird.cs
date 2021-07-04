using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bird : MonoBehaviour
{
    //references
    public Score score;
    public GameManager gameManager;
    public Sprite birdDied;
    public ColumnSpawner columnSpawner;

    SpriteRenderer sp;
    Animator anim;
    Rigidbody2D rb;
    public float speed;


    int angle;
    int maxAngle = 20;
    int minAngle = -90;

    bool touchedGround;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sp = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();     
        
        rb.gravityScale = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && 
            GameManager.gameOver == false)
        {
            if(GameManager.gameHasStarted == false)
            {
                rb.gravityScale = 0.8f;
                Flap();
                //column spawner
                columnSpawner.InstantiateColumn();
                gameManager.GameHasStarted();

            }
            else
            {
                Flap();
            }
            
        }

        BirdRotation();
        
    }

    void Flap()
    {
        rb.velocity = Vector2.zero;
        rb.velocity = new Vector2(rb.velocity.x, speed);
    }

    void BirdRotation()
    {
        if (rb.velocity.y > 0)
        {
            if (angle <= maxAngle)
            {
                angle += 4;
            }
        }
        else if (rb.velocity.y < -1.3f)
        {
            if (angle >= minAngle)
            {
                angle -= 3;
            }
        }

        if(touchedGround == false)
        {
        transform.rotation = Quaternion.Euler(0, 0, angle);

        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Column"))
        {
            print("We have scored!");
            score.Scored();
        }else if (collision.CompareTag("Pipe"))
        {
            //game over
            gameManager.GameOver();

        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            if(GameManager.gameOver == false)
            {
                //game over
                gameManager.GameOver();
                GameOver();
            }
            else
            {
                
                GameOver();
            }
        }
    }

    void GameOver()
    {
        touchedGround = true;
        anim.enabled = false;
        sp.sprite = birdDied;
        transform.rotation = Quaternion.Euler(0, 0, -90);
    }
}
