using UnityEngine;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using TMPro;
public class gamemanager : MonoBehaviour
{
    public GameObject bubbleprefab;
    public Sprite[] states; 
    public Transform Bubbles;
    public int rows = 2;
    public int columns = 9;
    public float bubbleSize ;
    public float spacing = 50f; 
    public float rowOffset = 1.0f;
    public Ball ball { get; private set; }//class classname get->ball's value can be read outside the class, private set-> its's value can be modified only within the class.
    public Paddle paddle { get; private set; }
    public int level = 1;
    public int score = 0;
    public int lives = 3;
    public Bubble[] bubbles { get; private set; }
    private int bubbleCount;
    
    public TMP_Text ScoreText;
    


    private List<List<Bubble>> bubbleGrid;

    private void Awake() //when script is first initialized, it will be called. initializing variables or states before the game starts.
    {
        //DontDestroyOnLoad(this.gameObject);//don't destroy the gameobject this gamemanager is attached to while loading a new scene. here the game object we created is game manager.
        SceneManager.sceneLoaded += onlevelloaded;
       
    }

    
    void Start()
    {
        bubbleSize = bubbleprefab.GetComponent<SpriteRenderer>().bounds.size.x;
        InitializeGrid();
        UpdateScoreText();
        this.bubbles = FindObjectsByType<Bubble>(FindObjectsSortMode.None);
        bubbleCount = bubbles.Length;
        /*if (Bubbles == null)
        {
            Bubbles = new GameObject("Bubbles").transform;
        }*/
        //NewGame();
    }

    public void IncreaseLife(int amount)
    {
        lives += amount;
        //Debug.Log("Player life increased. Current life: " + playerLife);
    }

    public void CreateNewLayerOfBubbles()
    {
        float topY = GetTopYPosition();
        List<Bubble> newRow = new List<Bubble>();
        for (int i = 0; i < columns; i++)
        {
            Vector2 position = new Vector2(i * bubbleSize * spacing, topY);
            GameObject newBubble = Instantiate(bubbleprefab, position, Quaternion.identity, Bubbles);
            Bubble Bubble = newBubble.GetComponent<Bubble>();
            Bubble.SetRandomState();// Set a random state for the new bubble
            newRow.Add(Bubble);
            bubbleGrid.Add(new List<Bubble>() { Bubble });  
        }
        bubbleCount += newRow.Count; 
        MoveExistingLayersDown();
    }

    private float GetTopYPosition()
    {
        if (Bubbles == null)
        {
            Debug.Log("Bubbles object is null or has been destroyed.");
            return 0f; // Return a default value or handle the case as needed
        }
        if ( Bubbles.childCount > 0)
        {
            float highestY = float.MinValue;
            foreach (Transform bubble in Bubbles)
            {
                if (bubble.position.y > highestY)
                {
                    highestY = bubble.position.y;
                }
            }
            return highestY + bubbleSize * spacing;
        }
        else
        {
            return Bubbles.position.y;
        }
    }

    private void MoveExistingLayersDown()
    {
        foreach (Transform bubble in Bubbles)
        {
            bubble.position = new Vector2(bubble.position.x, bubble.position.y - bubbleSize);
        }
    }

    public void DecrementLife()
    {
        lives--;
        if (lives < 3)
        {
            CreateNewLayerOfBubbles();
        }
    }
    public void life()
    {
        if (this.lives == 0)
        {
            resetlevel();
            NewGame();
        }
    }


    void InitializeGrid()
    {
        bubbleGrid = new List<List<Bubble>>();
        
        // Define the number of bubbles per row
        int[] bubblesPerRow = {8,9};
    
        for (int i = 0; i < bubblesPerRow.Length; i++)
        {
            List<Bubble> row = new List<Bubble>();
            
            for (int j = 0; j < bubblesPerRow[i]; j++)
            {
                Bubble bubble = PlaceBubble(i, j, bubblesPerRow[i]);
                row.Add(bubble);
            }
            
            bubbleGrid.Add(row);
        }
        
        //Debug.Log("added to grid");
    }
    Bubble PlaceBubble(int row, int col, int bubblesInRow)
    {
        float totalWidth = bubblesInRow * bubbleSize * spacing;
    
        // Calculate the horizontal offset to center the bubbles
        float xOffset = (columns * bubbleSize * spacing - totalWidth) * 0.5f;

        // Set the position to the top center of the grid
        Vector2 position = new Vector2((col * bubbleSize * spacing) + xOffset, (row - 1) * bubbleSize * spacing);

        GameObject bubbleGO = Instantiate(bubbleprefab, position, Quaternion.identity, Bubbles);//creating a copy of bubble prefab and placing it in position
        bubbleGO.tag = "bubble";
        Bubble newbubble = bubbleGO.GetComponent<Bubble>();
        newbubble.states = states; // Assign the states array to the bubble
        newbubble.SetRandomState();
        return newbubble;
    }

    public void ReplaceBubbleWithBallState(Bubble clonebubble, Ball ball)
    {
        clonebubble.SetStateBubble(ball.GetStateBall());
    }
    public void BreakConnectedBubbles(Bubble startingBubble)
    {
        Sprite targetState = startingBubble.GetStateBubble();
        BreakConnectedBubblesRecursive(startingBubble, targetState);
    }

    public void BreakConnectedBubblesRecursive(Bubble currentBubble, Sprite targetState)
    {
        if (currentBubble == null || currentBubble.GetStateBubble() != targetState || currentBubble.IsBroken)
        {
            return;
        }

        currentBubble.BreakBubble();
        //Debug.Log("Breaking bubble at position: ");
        currentBubble.IsBroken = true; // Set a flag to indicate that the bubble has been broken

        List<Bubble> adjacentBubbles = GetAdjacentBubbles(currentBubble);
        foreach (Bubble adjacentBubble in adjacentBubbles)
        {
            BreakConnectedBubblesRecursive(adjacentBubble, targetState);
        }
    }

    List<Bubble> GetAdjacentBubbles(Bubble bubble)
    {
        List<Bubble> adjacentBubbles = new List<Bubble>();
        Vector2 position = bubble.transform.position;
        float x = position.x;
        float y = position.y;

        // Adjust the tolerance value for adjacency checks
        float tolerance = 0.1f;

        // Check adjacent positions for bubbles (left, right, top, bottom)
        AddAdjacentBubble(adjacentBubbles, x - bubbleSize, y, tolerance);
        AddAdjacentBubble(adjacentBubbles, x + bubbleSize, y, tolerance);
        AddAdjacentBubble(adjacentBubbles, x, y - bubbleSize, tolerance);
        AddAdjacentBubble(adjacentBubbles, x, y + bubbleSize, tolerance);

        AddAdjacentBubble(adjacentBubbles,x - bubbleSize, y - bubbleSize, tolerance);
        AddAdjacentBubble(adjacentBubbles,x + bubbleSize, y - bubbleSize, tolerance);
        AddAdjacentBubble(adjacentBubbles,x - bubbleSize, y + bubbleSize, tolerance);
        AddAdjacentBubble(adjacentBubbles,x + bubbleSize, y + bubbleSize, tolerance);


        return adjacentBubbles;
    }
    void AddAdjacentBubble(List<Bubble> adjacentBubbles, float x, float y, float tolerance)
    {
        Bubble bubble = GetBubbleAtPosition(x, y, tolerance);
        if (bubble != null)
        {
            adjacentBubbles.Add(bubble);
        }
    }

    Bubble GetBubbleAtPosition(float x, float y, float tolerance)
    {
        foreach (var row in bubbleGrid)
        {
            foreach (var bubble in row)
            {
                if (bubble != null &&
                    Mathf.Abs(bubble.transform.position.x - x) < tolerance && 
                    Mathf.Abs(bubble.transform.position.y - y) < tolerance)
                {
                    return bubble;
                }
            }
        }
        return null;
    }



    private void NewGame()
    {
        this.score = 0;
        this.lives = 3;
        loadlevel("level1");//load the first game scene
    }

    private void loadlevel(string sceneName)
    {
        SceneManager.LoadScene(sceneName);//loading the scene using this function. passing tha parameter scenename
    }

    private void onlevelloaded(Scene scene, LoadSceneMode mode)
    {
        this.ball = FindFirstObjectByType<Ball>();//find the object of type ball and assign it to ball
        this.paddle = FindFirstObjectByType<Paddle>();
    }


    private void resetlevel()
    {
        this.ball.ResetBall();
        this.paddle.ResetPaddle();
    }

    

    public void Hit(Bubble bubblepoint)
    {
        this.score += bubblepoint.points;
        UpdateScoreText();
    }

     private void UpdateScoreText()
    {
        ScoreText.text = "Score: " + score;
    }

    public void OnBubbleBroken()
    {
        
        bubbleCount--;
        Debug.Log(bubbleCount);
        if (bubbleCount <= 1)
        {
            loadlevel("level2");
        }
    }
   

    
}
