using UnityEngine;

public class misslives : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name == "Ball") 
        {
            FindAnyObjectByType<gamemanager>().life();
            FindAnyObjectByType<gamemanager>().DecrementLife();
        }
        
        if(collision.gameObject.CompareTag("lifebubble"))
        {
            Destroy(collision.gameObject);
        }
    }
}
