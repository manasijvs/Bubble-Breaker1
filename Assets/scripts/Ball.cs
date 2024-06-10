using Unity.VisualScripting;
using UnityEngine;

public class Ball : MonoBehaviour
{
    public new Rigidbody2D rigidbody { get; private set; }
    public float speed = 2800.0f;
    
    private int count = 0;
    private int maxhits = 5000;
    public SpriteRenderer spriteRenderer; // Reference to the SpriteRenderer component
    public Sprite[] states;
    public float speedIncreaseFactor = 1.0f;

    private void Awake()
    {
        this.rigidbody = GetComponent<Rigidbody2D>();//reference to the component attached to the game object
    }

    private void Start()
    {
        ResetBall();
        SetRandomBall();

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
        force.x = Random.Range(-1f, 1f);//initializes x's value between -1 and 1.
        force.y = -1f;//initializes y's value to -1 to give a downward force
        this.rigidbody.AddForce(force.normalized * this.speed);//applies this force to the rigidbody component of the gameobject.
    }

     public void SetRandomBall()
    {
        // Get a random sprite from the states array
        Sprite randomSprite = states[Random.Range(0, states.Length)];

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

    

    private void OnCollisionEnter2D(Collision2D collision)
    {
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
                gameManager.CreateNewLayerOfBubbles();
                count = 0;
            }
        }
    }

}