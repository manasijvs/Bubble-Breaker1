using UnityEngine;
using UnityEngine.UI;
public class PowerUps : MonoBehaviour
{
    public GameObject bombPrefab;
    public GameObject rainbowPrefab;
    private GameObject breakingBall;
    public Button bombButton; // Reference to the Bomb button
    public Button rainbowButton; 
    //public GameObject ballPrefab;
    private int coins;
    private gamemanager gameManager;

    private void Start()
    {
        gameManager = FindFirstObjectByType<gamemanager>();
        UpdatePowerUpButtons();
    }

    public void UpdateCoins(int newCoinCount)
    {
        coins = newCoinCount;
        UpdatePowerUpButtons();
    }

    private void UpdatePowerUpButtons()
    {
        bool canUsePowerUp = coins >= 20;
        bombButton.interactable = canUsePowerUp;
        rainbowButton.interactable = canUsePowerUp;
    }

    public void ChangeBreakingBallToBomb()
    {
        // Make sure there's a breaking ball to replace
        if (breakingBall != null)
        {
            gameManager.DecreaseCoins(20);
            UpdatePowerUpButtons();
            Ball ball = FindFirstObjectByType<Ball>();
            ball.SetAsBomb(); // Set the flag
            
            // Get the position and rotation of the breaking ball
            Vector3 position = breakingBall.transform.position;
            Quaternion rotation = breakingBall.transform.rotation;

            // Destroy the breaking ball
            Destroy(breakingBall);
            Debug.Log("destroyed ball");
            Debug.Log("Bomb created");
            Instantiate(bombPrefab, position, rotation);
            
    
        }
    }

    public void SetBreakingBall(GameObject ball)
    {
        Debug.Log("set breaking ball as ball");
        breakingBall = ball;
    }


    public void SpawnRainbow()
    {
        //Instantiate(rainbowPrefab, new Vector3(0, 0, 0), Quaternion.identity);
        if (breakingBall != null)
        {
            gameManager.DecreaseCoins(20);
            UpdatePowerUpButtons();
            Ball ball = breakingBall.GetComponent<Ball>();
            ball.SetAsRainbow(); // Set the ball to Rainbow mode
        }
    }
}
