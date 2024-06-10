/*using UnityEngine;

public class BallPathPrediction : MonoBehaviour
{
    public LineRenderer lineRenderer;
    public int maxReflectionCount = 100;
    public float maxStepDistance = 1000f;
    public LayerMask collisionLayer;
    private Rigidbody2D ballRigidbody;

    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        ballRigidbody = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        ShowPredictedPath();
    }
    

    void ShowPredictedPath()
    {
    Vector2 ballPosition = transform.position;
    Vector2 ballDirection = ballRigidbody.velocity;
    lineRenderer.positionCount = 1;
    lineRenderer.SetPosition(0, ballPosition);

    for (int i = 0; i < maxReflectionCount; i++)
    {
        RaycastHit2D hit = Physics2D.Raycast(ballPosition, ballDirection, maxStepDistance, collisionLayer);
        if (hit.collider != null)
        {
            ballPosition = hit.point;
            
            // Calculate the reflection angle
            Vector2 normal = hit.normal;
            //Vector2 incomingDirection = ballDirection;
            ballDirection = Vector2.Reflect(ballDirection, normal);
            
            lineRenderer.positionCount++;
            lineRenderer.SetPosition(lineRenderer.positionCount - 1, ballPosition);

            // Check if the ball is moving towards the bubble after a bounce
            if (hit.collider.CompareTag("bubble"))
            {
            
                break; 
            }
            
        }
        else
        {
            lineRenderer.positionCount++;
            lineRenderer.SetPosition(lineRenderer.positionCount - 1, ballPosition + ballDirection * maxStepDistance);
            break;
                }
        }
    }
}*/
