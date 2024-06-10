using UnityEngine;

public class paddlecontrol : MonoBehaviour
{
    
    public float paddleSpeed = 80.0f; // Speed at which the paddle moves
    public float leftBoundary = -5.6f; // Adjust based on your game area
    public float rightBoundary = 46.0f; // Adjust based on your game area
    private Vector3 touchStartPos;
    private Vector3 paddleStartPos;
    private bool isDragging = false;

    void Update()
    {
        // Detect mouse or touch input
        if (Input.GetMouseButtonDown(0))
        {
            // Store the starting positions when the touch begins
            touchStartPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            paddleStartPos = transform.position;
            isDragging = true;
        }
        
        if (Input.GetMouseButton(0) && isDragging)
        {
            // Calculate the distance moved from the start position
            Vector3 touchCurrentPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector3 distanceMoved = touchCurrentPos - touchStartPos;
            
            // Move the paddle relative to the distance moved
            Vector3 newPaddlePos = paddleStartPos + new Vector3(distanceMoved.x, 0, 0);
            
            // Clamp the paddle position to stay within boundaries
            newPaddlePos.x = Mathf.Clamp(newPaddlePos.x, leftBoundary, rightBoundary);
            transform.position = new Vector3(newPaddlePos.x, transform.position.y, transform.position.z);
        }
        
        if (Input.GetMouseButtonUp(0))
        {
            isDragging = false;
        }
    }
}
