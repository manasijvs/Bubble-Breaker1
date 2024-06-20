using UnityEngine;
using System;

public class Bomb : MonoBehaviour
{
    public new Rigidbody2D rigidbody { get; private set; }
    public float speed = 2800.0f;
    public GameObject ballPrefab;
    public event Action<Vector3> OnExplode;

    private void Awake()
    {
        this.rigidbody = GetComponent<Rigidbody2D>();//reference to the component attached to the game object
    }


    private void SetRandomTrajectory()
    {
        Vector2 force = Vector2.zero;//creates a vector force and initializes to 0
        //force.x = UnityEngine.Random.Range(-1f, 1f);//initializes x's value between -1 and 1.
        force.y = -1f;//initializes y's value to -1 to give a downward force
        this.rigidbody.AddForce(force.normalized * this.speed);//applies this force to the rigidbody component of the gameobject.
    }

    private void Start()
    {
        SetRandomTrajectory();
    }

    public float explosionRadius = 10.0f; // Radius for detecting surrounding bubbles
    
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Check if the bomb collided with a bubble
        if (collision.gameObject.CompareTag("bubble"))
        {
            Debug.Log("bomb collided with bubble");
            Explode(collision.gameObject.transform.position);
        }
    }
    
    
    private void Explode(Vector2 explosionPoint)
    {
        // Find all game objects with the tag "bubble"
        GameObject[] bubbles = GameObject.FindGameObjectsWithTag("bubble");

        // Iterate through each bubble and check if it's within the explosion radius
        foreach (GameObject bubbleObject in bubbles)
        {
            float distance = Vector2.Distance(explosionPoint, bubbleObject.transform.position);
            if (distance <= explosionRadius)
            {
                // Get the Bubble component from the GameObject
                Bubble bubble = bubbleObject.GetComponent<Bubble>();
                if (bubble != null)
                {
                    // Handle breaking the bubble
                    
                    bubble.BreakBubble();
                }
            }
        }

        // Destroy the bomb object after the explosion
        Debug.Log("bomb object destroyed");
        Destroy(this.gameObject);
        Ball existingBall = FindFirstObjectByType<Ball>();
        gamemanager gameManager = FindFirstObjectByType<gamemanager>();
        if (existingBall == null)
        {
            Debug.Log("no existing ball, new ball added");
            // Instantiate a new ball at the bomb's position if no ball exists
            GameObject newBall = Instantiate(ballPrefab, transform.position, Quaternion.identity);
            newBall.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            gameManager.SetBallReference(newBall);
            Ball clone = newBall.GetComponent<Ball>();
            switch(gameManager.GetCurrentLevel())
            {
                case gamemanager.Level.level1:
                    clone.currentLevel=Ball.Level.level1;
                    break;
                case gamemanager.Level.level2:
                    clone.currentLevel=Ball.Level.level2;
                    break;
                case gamemanager.Level.level3:
                    clone.currentLevel=Ball.Level.level3;
                    break;
                case gamemanager.Level.level4:
                    clone.currentLevel=Ball.Level.level4;
                    break;
                case gamemanager.Level.level5:
                    clone.currentLevel=Ball.Level.level5;
                    break;
            }
            
            // Set the breaking ball reference in PowerUps
            PowerUps powerup =FindFirstObjectByType<PowerUps>();
            if (powerup != null)
            {
                powerup.SetBreakingBall(newBall);
            }
            
        }
        else
        {
            Debug.Log("has existing ball");
            // Move the existing ball to the bomb's position and reset its state
            existingBall.transform.position = transform.position;
            existingBall.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            //existingBall.ResetBall();
            // Set the breaking ball reference in PowerUps
            PowerUps powerup = FindFirstObjectByType<PowerUps>();
            if (powerup != null)
            {
                powerup.SetBreakingBall(existingBall.gameObject);
            }
        }
        //gamemanager gameManager = FindFirstObjectByType<gamemanager>();
        gameManager.AssignReferences();
    }

     
}

