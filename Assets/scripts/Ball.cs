using System;
using Unity.VisualScripting;
using UnityEngine;

public class Ball : MonoBehaviour
{
    public new Rigidbody2D rigidbody { get; private set; }
    public float speed = 2600.0f;
    
    private int count = 0;
    private int maxhits = 5;
    public SpriteRenderer spriteRenderer; // Reference to the SpriteRenderer component
    public Sprite[] states;
    public float speedIncreaseFactor = 2.0f;
    public enum Level
    {
        level1,
        level2,
        level3,
        level4,
        level5
    }
    public Level currentLevel;
    private bool isBomb = false;
    private bool isRainbow = false;
    public event Action OnLifeLost;

    public void LoseLife()
    {
        OnLifeLost?.Invoke();
    }

    private void Awake()
    {
        this.rigidbody = GetComponent<Rigidbody2D>();//reference to the component attached to the game object
    }

    private void Start()
    {
        ResetBall();
        SetRandomBall();
        //PowerUps powerup = FindFirstObjectByType<PowerUps>();
        //powerup.SetBreakingBall(gameObject);

    }

    public void ResetBall()
    {
        //this.transform.position = Vector2.zero;//position of object in 3d space. vector2.zero->same as vector2(0,0)
        this.rigidbody.velocity = Vector2.zero;
        Invoke(nameof(SetRandomTrajectory), 1f);//call the fn byname with a delay of 1sec
    }

    private void SetRandomTrajectory()
    {
        Vector2 force = Vector2.zero;//creates a vector force and initializes to 0
        force.x = UnityEngine.Random.Range(-1f, 1f);//initializes x's value between -1 and 1.
        force.y = -1f;//initializes y's value to -1 to give a downward force
        this.rigidbody.AddForce(force.normalized * this.speed);//applies this force to the rigidbody component of the gameobject.
    }

     public void SetRandomBall()
    {
        // Get a random sprite from the states array
        Sprite randomSprite = states[UnityEngine.Random.Range(0, states.Length)];

        // Set the sprite for the SpriteRenderer
        spriteRenderer.sprite = randomSprite;
    }

    public Sprite GetStateBall()
    {
        return spriteRenderer.sprite;
    }

    public void increasespeed()
    {
        Vector2 currentVelocity = this.rigidbody.velocity;
        this.rigidbody.velocity = currentVelocity * speedIncreaseFactor;
    }
    public void SetAsRainbow()
    {
        isRainbow = true; // Set the flag when the ball is in Rainbow mode
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (isRainbow)
        {
            HandleRainbowCollision(collision);
            isRainbow = false; // Reset the flag after handling the collision
            return;
        }
        Bubble collidedBubble = collision.gameObject.GetComponent<Bubble>();
        if (collidedBubble != null)
        {
            gamemanager gameManager = FindFirstObjectByType<gamemanager>();
            //Debug.Log("Ball collided with bubble at position: "+ collision.gameObject.transform.position);
           
            
            
            if (collidedBubble.GetStateBubble() == GetStateBall())
            {
                
                //Debug.Log("Ball and bubble have the same state.");
                //Debug.Break();
                gameManager.BreakConnectedBubbles(collidedBubble);            
            }

            else //if (collidedBubble.GetStateBubble() != GetStateBall())
            {
              
                
                //Debug.Log("Ball and bubble have different states");
                //Debug.Break();
                gameManager.ReplaceBubbleWithBallState(collidedBubble, this);
                //Debug.Log("Bubble state changed to match ball state.");

                
            }
            if (isBomb == false) // Check the flag before calling SetBreakingBall
            {
                PowerUps powerup = FindFirstObjectByType<PowerUps>();
                powerup.SetBreakingBall(gameObject);
            }
            
        }
            
        if (collision.gameObject.CompareTag("bubble")) 
        {
            SetRandomBall();
            //Debug.Log("state of ball changed after collision");
        }

        if (collision.gameObject.CompareTag("paddle"))
        {
            increasespeed();
            count++;
            if (count >= maxhits)
            {
                gamemanager gameManager = FindFirstObjectByType<gamemanager>();
                switch (currentLevel)
                {
                    case Level.level1:
                        gameManager.CreateNewLayerOfBubbleslevel1();
                        break;
                    case Level.level2:
                        gameManager.CreateNewLayerOfBubbleslevel2();
                        break;
                    case Level.level3:
                        gameManager.CreateNewLayerOfBubbleslevel3();
                        break;
                    case Level.level4:
                        gameManager.CreateNewLayerOfBubbleslevel4();
                        break;
                    case Level.level5:
                        gameManager.CreateNewLayerOfBubbleslevel5();
                        break;
                }
                count = 0;
            }
        }
    }
    private void HandleRainbowCollision(Collision2D collision)
    {
        Bubble collidedBubble = collision.gameObject.GetComponent<Bubble>();
        if (collidedBubble != null)
        {

            // Destroy all bubbles in the same row as the collided bubble
            Vector3 bubblePosition = collidedBubble.transform.position;
            Collider2D[] bubblesInRow = Physics2D.OverlapBoxAll(
                new Vector2(bubblePosition.x, bubblePosition.y),
                new Vector2(100, 1), // Assuming the bubbles are aligned horizontally
                0);

            foreach (Collider2D collider in bubblesInRow)
            {
                Bubble bubble = collider.GetComponent<Bubble>();
                if (bubble != null)
                {
                    //gameManager.OnBubbleBroken(bubble);
                    bubble.BreakBubble();
                }
            }
        }
    }
    public void SetAsBomb()
    {
        isBomb = true; // Set the flag when the ball is transformed into a bomb
    }
    

}