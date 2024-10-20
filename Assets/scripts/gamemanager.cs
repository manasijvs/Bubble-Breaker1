using UnityEngine;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using TMPro;
public class gamemanager : MonoBehaviour
{
    public GameObject bubbleprefab;
    public Sprite[] states; 
    public Transform Bubbles;
    public int rows ;
    public int columns;
    public float bubbleSize = 5;
    public float spacing = 1f; 
    //public float rowOffset = 1.0f;
    public Ball ball { get; private set; }//class classname get->ball's value can be read outside the class, private set-> its's value can be modified only within the class.
    public Paddle paddle { get; private set; }
    //public int level;
    public int score = 0;
    public int lives = 3;
    public Bubble[] bubbles { get; private set; }
    private int bubbleCount;
    public TMP_Text ScoreText;
    public TMP_Text CoinText;
    public GameObject EndScreenCanvas; // End screen Canvas
    public TMP_Text EndScreenScoreText;
    public TMP_Text EndScreenCoinsText;
    private int coins; // End screen score Text
    public static gamemanager instance;
    public GameObject balls; // Reference to the ball game object
    public GameObject paddles;
    public GameObject lifebubble;
    public GameObject bomb;
    //public GameObject hammer;
    public GameObject rainbowball;
    public GameObject ballPrefab;
    public PowerUps powerUps;
    private int spentScore;

    public enum Level
    {
        level1,
        level2,
        level3,
        level4,
        level5,
        MainMenu
    }
    public Level currentLevel;
    
    private List<List<Bubble>> bubbleGrid;

    private void Awake() //when script is first initialized, it will be called. initializing variables or states before the game starts.
    {
        SceneManager.sceneLoaded += onlevelloaded;
    }

    public void AssignReferences()
    {
        // Reassign components that might be missing after scene load
        if (Bubbles == null)
        {
            Bubbles = GameObject.Find("Bubbles").transform;
        }

        if (ScoreText == null)
        {
            ScoreText = GameObject.Find("ScoreText").GetComponent<TextMeshProUGUI>();
        }
        if(balls == null)
        {
            balls = GameObject.Find("Ball").gameObject;
        }
    }

    void SetCurrentLevel(Level newlevel)
    {
        currentLevel = newlevel; 
    }
    public Level GetCurrentLevel()
    {
        return currentLevel;
    }

    
    void Start()
    {
        EndScreenCanvas.SetActive(false);
        //bubbleSize = bubbleprefab.GetComponent<SpriteRenderer>().bounds.size.x;
        if (currentLevel == Level.level1)
        {
            InitializeGridlevel1();
        }
        else if (currentLevel == Level.level2)
        {
            AssignReferences();
            InitializeGridlevel2();
        }
        else if (currentLevel == Level.level3)
        {
            InitializeGridlevel3();
        }
        else if (currentLevel == Level.level4)
        {
            InitializeGridlevel4();
        }
        else if (currentLevel == Level.level5)
        {
            InitializeGridlevel5();
        }

        UpdateScoreText();
        this.bubbles = FindObjectsByType<Bubble>(FindObjectsSortMode.None);
        bubbleCount = bubbles.Length;
    }

    public void IncreaseLife(int amount)
    {
        lives += amount;
    }

    public void CreateNewLayerOfBubbleslevel5()
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
            //bubbleGrid.Add(new List<Bubble>() { Bubble });  
        }
        bubbleGrid.Add(newRow);
        bubbleCount += newRow.Count; 
        MoveExistingLayersDown();
    }
    public void CreateNewLayerOfBubbleslevel4()
    {
        float topY = GetTopYPosition();
        List<Bubble> newRow = new List<Bubble>();
        for (int i = 0; i < columns; i++)
        {
            Vector2 position = new Vector2(i * bubbleSize * spacing, topY);
            GameObject newBubble = Instantiate(bubbleprefab, position, Quaternion.identity, Bubbles);
            Bubble Bubble = newBubble.GetComponent<Bubble>();
            Bubble.SetRandomState();
            newRow.Add(Bubble);
            //bubbleGrid.Add(new List<Bubble>() { Bubble });  
        }
        bubbleGrid.Add(newRow);
        bubbleCount += newRow.Count; 
        MoveExistingLayersDown();
    }

    public void CreateNewLayerOfBubbleslevel3()
    {
        float topY = GetTopYPosition();
        List<Bubble> newRow = new List<Bubble>();
        for (int i = 0; i < columns; i++)
        {
            Vector2 position = new Vector2(i * bubbleSize * spacing, topY);
            GameObject newBubble = Instantiate(bubbleprefab, position, Quaternion.identity, Bubbles);
            Bubble Bubble = newBubble.GetComponent<Bubble>();
            Bubble.SetRandomState();
            newRow.Add(Bubble);
            //bubbleGrid.Add(new List<Bubble>() { Bubble });  
        }
        bubbleGrid.Add(newRow);
        bubbleCount += newRow.Count; 
        MoveExistingLayersDown();
    }
    public void CreateNewLayerOfBubbleslevel2()
    {
        float topY = GetTopYPosition();
        List<Bubble> newRow = new List<Bubble>();
        for (int i = 0; i < columns; i++)
        {
            Vector2 position = new Vector2(i * bubbleSize * spacing, topY);
            GameObject newBubble = Instantiate(bubbleprefab, position, Quaternion.identity, Bubbles);
            Bubble Bubble = newBubble.GetComponent<Bubble>();
            Bubble.SetRandomState();
            newRow.Add(Bubble);
            //bubbleGrid.Add(new List<Bubble>() { Bubble });  
        }
        bubbleGrid.Add(newRow);
        bubbleCount += newRow.Count; 
        MoveExistingLayersDown();
    }

    private float GetTopYPosition()
    {
        if (Bubbles == null)
        {
            return 0f; 
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

    public void CreateNewLayerOfBubbleslevel1()
    {
        float topY = GetTopYPosition();
        List<Bubble> newRow = new List<Bubble>();
        var rowStates = GenerateRowStates(columns);
        for (int i = 0; i < columns; i++)
        {
            Vector2 position = new Vector2(i * bubbleSize * spacing, topY);
            GameObject newBubble = Instantiate(bubbleprefab, position, Quaternion.identity, Bubbles);
            Bubble Bubble = newBubble.GetComponent<Bubble>();
            Sprite stateSprite = Bubble.GetSpriteForState(rowStates[i]);
            Bubble.SetStateBubble(stateSprite);
            newRow.Add(Bubble); 
        }
        bubbleGrid.Add(newRow); 
        bubbleCount += newRow.Count; 
        MoveExistingLayersDown();
    }

    public void SetBall(GameObject cloneBall)
    {
        balls = cloneBall;
    }

    public void DecrementLife()
    {
        
        lives--;
        if (lives < 3)
        {
            switch (currentLevel)
            {
                case Level.level1:
                    CreateNewLayerOfBubbleslevel1();
                    break;
                case Level.level2:
                    CreateNewLayerOfBubbleslevel2();
                    break;
                case Level.level3:
                    CreateNewLayerOfBubbleslevel3();
                    break;
                case Level.level4:
                    CreateNewLayerOfBubbleslevel4();
                    break;
                case Level.level5:
                    CreateNewLayerOfBubbleslevel5();
                    break;
            }
        }
    }
    public void SetBallReference(GameObject newBall)
    {
        ball = newBall.GetComponent<Ball>();
    }

    public void life()
    {
        if (this.lives == 0)
        {
            resetlevel();
            NewGame();
        }
    }
    public void RegisterBall(GameObject newBall)
    {
        SetBall(newBall);
        Ball ballComponent = newBall.GetComponent<Ball>();
        if (ballComponent != null)
        {
            ballComponent.OnLifeLost += HandleLifeLost;
        }
    }

    private void HandleLifeLost()
    {
        life();
        DecrementLife();
    }

    void InitializeGridlevel5()
    {
        bubbleGrid = new List<List<Bubble>>();
        
        // Define the number of bubbles per row
        int[] bubblesPerRow = {5,7,9,7,9};
    
        for (int i = 0; i < bubblesPerRow.Length; i++)
        {
            List<Bubble> row = new List<Bubble>();
            
            for (int j = 0; j < bubblesPerRow[i]; j++)//iterates through each column in  a row
            {
                Bubble bubble = PlaceBubble(i, j, bubblesPerRow[i]);//for each bubble calls placebubble
                row.Add(bubble);
            }
            
            bubbleGrid.Add(row);
        }
        
    }
    void InitializeGridlevel4()
    {
        Debug.Log("Inside InitializeGridlevel4");
        bubbleGrid = new List<List<Bubble>>();
        
        // Define the number of bubbles per row
        int[] bubblesPerRow = {7,9,7,9};
    
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

    }
    void InitializeGridlevel3()
    {
        Debug.Log("Inside InitializeGridlevel3");
        bubbleGrid = new List<List<Bubble>>();
        
        // Define the number of bubbles per row
        int[] bubblesPerRow = {5,7,9};
    
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

    }
    


    void InitializeGridlevel2()
    {
        Debug.Log("Inside InitializeGridlevel2");
        bubbleGrid = new List<List<Bubble>>();
        
        // Define the number of bubbles per row
        int[] bubblesPerRow = {7,9};
    
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

    }
    Bubble PlaceBubble(int row, int col, int bubblesInRow)
    {
        float totalWidth = bubblesInRow * bubbleSize * spacing;
    
        // Calculate the horizontal offset to center the bubbles
        float xOffset = (columns * bubbleSize * spacing - totalWidth) * 0.5f;

        // Set the position to the top center of the grid
        Vector2 position = new Vector2((col * bubbleSize * spacing) + xOffset, (row-1) * bubbleSize * spacing);

        GameObject bubbleGO = Instantiate(bubbleprefab, position, Quaternion.identity, Bubbles);//creating a copy of bubble prefab and placing it in position
        bubbleGO.tag = "bubble";
        Bubble newbubble = bubbleGO.GetComponent<Bubble>();
        newbubble.states = states; // Assign the states array to the bubble
        newbubble.SetRandomState();
        return newbubble;
    }

    void InitializeGridlevel1()
    {
        Debug.Log("Inside InitializeGridlevel1");
        bubbleGrid = new List<List<Bubble>>();

        // Define the number of bubbles per row
        int[] bubblesPerRow = { 7, 9 };

        for (int i = 0; i < bubblesPerRow.Length; i++)
        {
            List<Bubble> row = new List<Bubble>();

            // Generate states for the row
            int[] rowStates = GenerateRowStates(bubblesPerRow[i]);

            for (int j = 0; j < bubblesPerRow[i]; j++)
            {
                Bubble bubble = PlaceBubblelevel1(i, j, bubblesPerRow[i], rowStates[j]);
                row.Add(bubble);
            }

            bubbleGrid.Add(row);
        }

        //Debug.Log("added to grid");
    }

    Bubble PlaceBubblelevel1(int row, int col, int bubblesInRow, int stateIndex)
    {
        float totalWidth = bubblesInRow * bubbleSize * spacing;

        // Calculate the horizontal offset to center the bubbles
        float xOffset = (columns * bubbleSize * spacing - totalWidth) * 0.5f;

        // Set the position to the top center of the grid
        Vector2 position = new Vector2((col * bubbleSize * spacing) + xOffset, (row - 1) * bubbleSize * spacing);

        GameObject bubbleGO = Instantiate(bubbleprefab, position, Quaternion.identity, Bubbles); // creating a copy of bubble prefab and placing it in position
        bubbleGO.tag = "bubble";
        Bubble newbubble = bubbleGO.GetComponent<Bubble>();

        // Set the state index of the bubble
        newbubble.SetStateIndex(stateIndex);

        return newbubble;
    }

    int[] GenerateRowStates(int bubblesInRow)
    {
        int[] rowStates = new int[bubblesInRow];

        // Choose two random states
        int state1 = Random.Range(0, states.Length);
        int state2;
        do
        {
            state2 = Random.Range(0, states.Length);
        } while (state2 == state1);

        int blockStartIndex1 = Random.Range(0, bubblesInRow - 3); 
        int blockStartIndex2 = Random.Range(blockStartIndex1 + 4, bubblesInRow - 1); 

        for (int i = 0; i < 4; i++)
        {
            rowStates[blockStartIndex1 + i] = state1;
        }

        for (int i = blockStartIndex2; i < bubblesInRow; i++)
        {
            rowStates[i] = state2;
        }

        for (int i = 0; i < blockStartIndex1; i++)
        {
            rowStates[i] = state1;
        }

        // Shuffle the rowStates array 
        //ShuffleArray(rowStates);

        return rowStates;
    }

    void ShuffleArray<T>(T[] array)
    {
        for (int i = array.Length - 1; i > 0; i--)
        {
            int randomIndex = Random.Range(0, i + 1);
            T temp = array[i];
            array[i] = array[randomIndex];
            array[randomIndex] = temp;
        }
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
        currentBubble.IsBroken = true; 

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

        AddAdjacentBubble(adjacentBubbles, x - bubbleSize, y);
        AddAdjacentBubble(adjacentBubbles, x + bubbleSize, y);
        AddAdjacentBubble(adjacentBubbles, x, y - bubbleSize);
        AddAdjacentBubble(adjacentBubbles, x, y + bubbleSize);

        AddAdjacentBubble(adjacentBubbles,x - bubbleSize, y - bubbleSize);
        AddAdjacentBubble(adjacentBubbles,x + bubbleSize, y - bubbleSize);
        AddAdjacentBubble(adjacentBubbles,x - bubbleSize, y + bubbleSize);
        AddAdjacentBubble(adjacentBubbles,x + bubbleSize, y + bubbleSize);


        return adjacentBubbles;
    }
    void AddAdjacentBubble(List<Bubble> adjacentBubbles, float x, float y)
    {
        Bubble bubble = GetBubbleAtPosition(x, y);
        if (bubble != null)
        {
            adjacentBubbles.Add(bubble);
        }
    }

    Bubble GetBubbleAtPosition(float x, float y)
    {
        foreach (var row in bubbleGrid)
        {
            foreach (var bubble in row)
            {
                if (bubble != null &&
                    Mathf.Approximately(bubble.transform.position.x, x)  && 
                    Mathf.Approximately(bubble.transform.position.y , y))
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
        if (currentLevel == Level.level1)
        {
            loadlevel("level1");
        }
        else if (currentLevel == Level.level2)
        {
            loadlevel("level2");
        }
        else if (currentLevel == Level.level3)
        {
            loadlevel("level3");
        }
        else if (currentLevel == Level.level4)
        {
            loadlevel("level4");
        }
        else if (currentLevel == Level.level5)
        {
            loadlevel("level5");
        }            
    }

    private void loadlevel(string sceneName)
    {
        SceneManager.LoadScene(sceneName);//loading the scene using this function. passing tha parameter scenename
    }

    private void onlevelloaded(Scene scene, LoadSceneMode mode)
    {
        AssignReferences();
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
        CalculateCoins();
    }
    private void CalculateCoins()
    {
        int availableScore = score - spentScore;
        coins = (availableScore / 50) * 20;
        CoinText.text = "Coins: " + coins;
        powerUps.UpdateCoins(coins);
    }
    public bool DecreaseCoins(int amount)
    {
        if (coins >= amount)
        {
            coins -= amount;
            spentScore += (amount / 20) * 50;
            CoinText.text = "Coins: " + coins;
            powerUps.UpdateCoins(coins);
            return true;
        }
        return false;
    }

    private void DisplayEndScreen()
    {
        Ball existingBall = FindFirstObjectByType<Ball>();
        Destroy(existingBall.gameObject);
        paddles.SetActive(false);
        lifebubble.SetActive(false);
        bomb.SetActive(false);
        rainbowball.SetActive(false);
        EndScreenCanvas.SetActive(true);
        EndScreenScoreText.text = "Total Score: " + score; 
        EndScreenCoinsText.text = "Coins: " + coins;
    }

    public void nextlevel()
    {
        if (currentLevel == (Level)999)
        {
            loadlevel("MainMenu");
            return;
        } 
        balls = Instantiate(ballPrefab, new Vector3(0, 0, 0), Quaternion.identity);
        paddles.SetActive(true);
        lifebubble.SetActive(true);
        bomb.SetActive(true);
        rainbowball.SetActive(true);
        if (currentLevel == Level.level1)
        {
            loadlevel("level1");
        }
        else if (currentLevel == Level.level2)
        {
            loadlevel("level2");
        }
        else if (currentLevel == Level.level3)
        {
            loadlevel("level3");
        }
        else if (currentLevel == Level.level4)
        {
            loadlevel("level4");
        }
        else if(currentLevel == Level.level5)
        {
            loadlevel("level5");
        }
           
    }

    public void OnBubbleBroken()
    {
        bubbleCount--;
        if (bubbleCount == 0)
        {
            if (currentLevel == Level.level1)
            {
                SetCurrentLevel(Level.level2);
            }
            else if (currentLevel == Level.level2)
            {
                SetCurrentLevel(Level.level3);
            }
            else if (currentLevel == Level.level3)
            {
                SetCurrentLevel(Level.level4);
            }
            else if (currentLevel == Level.level4)
            {
                SetCurrentLevel(Level.level5);
            }
            else if (currentLevel == Level.level5)
            {
                currentLevel = (Level)999;
            }
            DisplayEndScreen();
        }
    }

    public void breakbubble(Bubble bubble)
    {
        Destroy(bubble.gameObject);
    }
       
}