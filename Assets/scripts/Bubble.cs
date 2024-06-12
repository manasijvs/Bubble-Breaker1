using UnityEngine;
using System.Collections.Generic;

public class Bubble : MonoBehaviour
{
    public SpriteRenderer spriteRenderer; // Reference to the SpriteRenderer component
    public Sprite[] states; // Array to hold different sprite states
    public int points = 10;
     private int stateIndex; // Points value for the bubble
    public int health { get; private set; }

    public void Start()
    {
        if (spriteRenderer == null)
        {
            spriteRenderer = GetComponent<SpriteRenderer>(); // Get the SpriteRenderer component if not assigned
        }

        //SetRandomState(); // Assign a random sprite to the bubble
    }

    
    public void SetRandomState()
    {
        // Get a random sprite from the states array
        Sprite randomSprite = states[Random.Range(0, states.Length)];

        // Set the sprite for the SpriteRenderer
          spriteRenderer.sprite = randomSprite;
    }

    private void Hit()
    {
        
        //this.health --;

        //if (this.health <= 0) 
        //{
        //    this.gameObject.SetActive(false);
       // }
        //this.gameObject.SetActive(false);
        FindAnyObjectByType<gamemanager>().Hit(this);

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name == "Ball") 
        {
            Hit();
            //Ball ball = collision.gameObject.GetComponent<Ball>();
            //if(GetStateBubble() == ball.GetStateBall())
            //{
             //   OnBroken();
            //} 
        }
        
    }
    public Sprite GetSpriteForState(int stateIndex)
    {
        return states[stateIndex];
    }

    public void SetStateIndex(int index)
    {
        stateIndex = index;
        // Set the sprite based on the state index
        if (stateIndex >= 0 && stateIndex < states.Length)
        {
            spriteRenderer.sprite = states[stateIndex];
        }
        else
        {
            Debug.LogError("Invalid state index for bubble sprite.");
        }
    }
    public Sprite GetStateBubble()
    {
        return spriteRenderer.sprite;
    }

    public void SetStateBubble(Sprite newState)
    {
        spriteRenderer.sprite = newState;
    }
    public bool IsBroken { get;set; }

    public void BreakBubble()
    {
        // Additional logic to handle breaking the bubble
        gamemanager gameManager = FindFirstObjectByType<gamemanager>();
        gameManager.OnBubbleBroken();
        IsBroken = true; // Set IsBroken flag to true when the bubble is broken
        Destroy(gameObject); // Destroy the bubble GameObject
    }
    
    /*public void OnBroken()
    {
        gamemanager gameManager = FindFirstObjectByType<gamemanager>();
        gameManager.OnBubbleBroken();
        Destroy(gameObject);
    }*/

}
