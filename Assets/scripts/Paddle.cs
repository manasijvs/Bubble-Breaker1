using UnityEngine;

public class Paddle : MonoBehaviour
{
    private Rigidbody2D rb;
    public Vector2 direction { get; private set; }
    public float speed = 50f;
    public float maxBounceAngle = 75f;
    Vector2 targetPosition;

    private void Awake()
    {
        this.rb = GetComponent<Rigidbody2D>();
    }

    public void ResetPaddle()
    {
        this.transform.position = new Vector2(0f, this.transform.position.y);//resets the horizonatal while maintaing the vertical position
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        rb.velocity = Vector2.zero;
    }

    private void Update()//to continuously check the player input to determine the movement direction of the object
    {
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))//a or left arrow key is pressed
        {
            this.direction = Vector2.left;//set direction to left
        }
        else if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            this.direction = Vector2.right;
        }
        else
        {
            this.direction = Vector2.zero;
        }
        
    }

    private void FixedUpdate()//for physics based calculations
    {
        this.rb.velocity = this.direction * this.speed;//to move the paddle in the specified direction and speed
    }


    private void OnCollisionEnter2D(Collision2D collision)//called when a gameobject(paddle) collides with another gameobject(ball)
    {
        Ball ball = collision.gameObject.GetComponent<Ball>();//to make sure the other thing colliding with the paddle is ball. to get the reference to the ball component from gameobject involved in the collision

        if (ball != null)
        {
            Vector2 contactPoint = collision.GetContact(0).point;
            Vector3 paddleCenter = this.transform.position; // The paddle's center position

            // Calculate the offset where on the paddle the ball hits relative to the middle point
            float offset =contactPoint.x-paddleCenter.x;
            //Debug.Log(offset);

            // Determine if the ball hits the left or right part of the paddle
            float hitDirection; //= (offset < 0) ? -1 : 1; // -1 for left, 1 for right

            // Calculate half the width of the paddle
            float width = collision.otherCollider.bounds.size.x / 2;

            // Calculate the bounce angle based on the offset and the maxBounceAngle
            
            

            // Get the current angle of the ball's velocity relative to the upward direction
            

            // Calculate the new angle by adjusting the current angle based on the bounce angle
            
            if (offset < 0)
            {
                // Ball hit the left part of the paddle
                //Debug.Log("Ball hit the left part of the paddle.");
                hitDirection=-1;
                float bounceAngle = (offset / width) *23;
                //Debug.Log("left angle is"+bounceAngle);
                //float currentAngle = Vector2.SignedAngle(Vector2.up, ball.rigidbody.velocity);
                float newAngle = bounceAngle*hitDirection  ;
                //Debug.Log("left new angle is"+newAngle);
                Quaternion rotation = Quaternion.AngleAxis(newAngle, Vector3.forward);
                ball.rigidbody.velocity = rotation * Vector2.up * ball.rigidbody.velocity.magnitude;
            }
            else
            {
                // Ball hit the right part of the paddle
                //Debug.Log("Ball hit the right part of the paddle.");
                hitDirection=-1;
                float bounceAngle = (offset / width)*23 ;
                //Debug.Log("right bounce angle is"+bounceAngle);
                //float currentAngle = Vector2.SignedAngle(Vector2.up, ball.rigidbody.velocity);
                float newAngle = bounceAngle*hitDirection;
                //Debug.Log("right new angle is"+newAngle);
                Quaternion rotation = Quaternion.AngleAxis(newAngle, Vector3.forward);
                ball.rigidbody.velocity = rotation * Vector2.up * ball.rigidbody.velocity.magnitude;
            }

            // Apply the rotation to the ball's velocity
            
            
        }
        Bomb bomb = collision.gameObject.GetComponent<Bomb>();

        if (bomb != null)
        {
            Vector2 contactPoint = collision.GetContact(0).point;
            Vector3 paddleCenter = this.transform.position; // The paddle's center position

            // Calculate the offset where on the paddle the ball hits relative to the middle point
            float offset =contactPoint.x-paddleCenter.x;
            //Debug.Log(offset);

            // Determine if the ball hits the left or right part of the paddle
            float hitDirection; //= (offset < 0) ? -1 : 1; // -1 for left, 1 for right

            // Calculate half the width of the paddle
            float width = collision.otherCollider.bounds.size.x / 2;

            // Calculate the bounce angle based on the offset and the maxBounceAngle
            
            

            // Get the current angle of the ball's velocity relative to the upward direction
            

            // Calculate the new angle by adjusting the current angle based on the bounce angle
            
            if (offset < 0)
            {
                // Ball hit the left part of the paddle
                //Debug.Log("Ball hit the left part of the paddle.");
                hitDirection=-1;
                float bounceAngle = (offset / width) *23;
                //Debug.Log("left angle is"+bounceAngle);
                //float currentAngle = Vector2.SignedAngle(Vector2.up, ball.rigidbody.velocity);
                float newAngle = bounceAngle*hitDirection  ;
                //Debug.Log("left new angle is"+newAngle);
                Quaternion rotation = Quaternion.AngleAxis(newAngle, Vector3.forward);
                bomb.rigidbody.velocity = rotation * Vector2.up * bomb.rigidbody.velocity.magnitude;
            }
            else
            {
                // Ball hit the right part of the paddle
                //Debug.Log("Ball hit the right part of the paddle.");
                hitDirection=-1;
                float bounceAngle = (offset / width)*23 ;
                //Debug.Log("right bounce angle is"+bounceAngle);
                //float currentAngle = Vector2.SignedAngle(Vector2.up, ball.rigidbody.velocity);
                float newAngle = bounceAngle*hitDirection;
                //Debug.Log("right new angle is"+newAngle);
                Quaternion rotation = Quaternion.AngleAxis(newAngle, Vector3.forward);
                bomb.rigidbody.velocity = rotation * Vector2.up * bomb.rigidbody.velocity.magnitude;
            }

            // Apply the rotation to the ball's velocity
            
            
        }
    }
}
