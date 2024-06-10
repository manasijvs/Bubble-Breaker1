using UnityEngine;

public class LifeBubble : MonoBehaviour
{

    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }
   private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("paddle"))
        {
            // Increase the player's life
            gamemanager gameManager = FindFirstObjectByType<gamemanager>();
            gameManager.IncreaseLife(1);

            // Destroy the life bubble
            Destroy(gameObject);
        }
    }
}
