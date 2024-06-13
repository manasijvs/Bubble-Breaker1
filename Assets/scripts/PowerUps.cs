using UnityEngine;

public class PowerUps : MonoBehaviour
{
    public GameObject bombPrefab;
    public GameObject hammerPrefab;
    public GameObject bubblePrefab;
    private GameObject breakingBall;
    //public GameObject ballPrefab;
    

    public void ChangeBreakingBallToBomb()
    {
        // Make sure there's a breaking ball to replace
        if (breakingBall != null)
        {
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

    /*private void HandleBombExplosion(Vector3 position)
    {
        // Instantiate a new ball at the bomb's position after the explosion
        Debug.Log("instantiating new ball");
        GameObject newBall = Instantiate(ballPrefab, position, Quaternion.identity);
        SetBreakingBall(newBall);
    }*/

    public void SetBreakingBall(GameObject ball)
    {
        Debug.Log("set breaking ball as ball");
        breakingBall = ball;
    }

    public void SpawnHammer()
    {
        Instantiate(hammerPrefab, new Vector3(0, 0, 0), Quaternion.identity);
    }

    public void SpawnBubble()
    {
        Instantiate(bubblePrefab, new Vector3(0, 0, 0), Quaternion.identity);
    }
}
