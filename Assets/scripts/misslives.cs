using UnityEngine;

public class misslives : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Ball ballComponent = collision.gameObject.GetComponent<Ball>();
        if (ballComponent != null) 
        {
            ballComponent.LoseLife(); // Trigger the event
            gamemanager gameManager = FindFirstObjectByType<gamemanager>();
            gameManager.life();
            gameManager.DecrementLife();
        }
        
        if(collision.gameObject.CompareTag("lifebubble"))
        {
            Destroy(collision.gameObject);
        }
    }
}
