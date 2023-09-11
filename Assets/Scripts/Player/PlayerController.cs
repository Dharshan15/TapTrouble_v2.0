using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    private GameManager gameManager;
    private PlayerHealth playerHealth;
    private Tutorial tutorial;

    private int currentLane = 1;

    private float leftLaneX = -1.6f;
    private float middleLaneX = 0f;
    private float rightLaneX = 1.6f;

    // Variables to handle swiping
    private Vector2 touchStart;
    private bool isSwiping = false;

    private void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        playerHealth = FindObjectOfType<PlayerHealth>();
        tutorial = FindObjectOfType<Tutorial>();
    }

    private void Update()
    {
        // Check for user input (mobile touch or mouse click)
        if (Input.GetMouseButtonDown(0))
        {
            touchStart = Input.mousePosition;
            isSwiping = true;
        }
        else if (Input.GetMouseButtonUp(0))
        {
            float swipeDistance = (touchStart - (Vector2)Input.mousePosition).magnitude;
            if (isSwiping && swipeDistance > 50f) // Minimum swipe distance to detect a valid swipe
            {
                // Detect the direction of swipe
                Vector2 swipeDirection = (Vector2)Input.mousePosition - touchStart;
                if (swipeDirection.x > 0) // Swipe right
                {
                    MoveToLane(currentLane + 1);
                }
                else if (swipeDirection.x < 0) // Swipe left
                {
                    MoveToLane(currentLane - 1);
                }
            }
            isSwiping = false;
        }
    }
    void MoveToLane(int targetLane)
    {
        // Clamp the targetLane value to ensure it stays within the valid lane indices (0, 1, 2)
        targetLane = Mathf.Clamp(targetLane, 0, 2);

        // Calculate the target position based on the selected lane
        float targetX = middleLaneX; // Default to the middle lane
        if (targetLane == 0)
        {
            targetX = leftLaneX;
        }
        else if (targetLane == 2)
        {
            targetX = rightLaneX;
        }

        Vector3 targetPosition = new Vector3(targetX, transform.position.y, transform.position.z);
        transform.position = targetPosition;

        currentLane = targetLane;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (gameObject.tag == "CirclePlayer")
        {
            if (collision.gameObject.tag == "SquareEnemy")
            {
                playerHealth.lives -= 1;
                Destroy(collision.gameObject);
            }
            else if (collision.gameObject.tag == "CircleEnemy")
            {
                gameManager.score += 1;
                Destroy(collision.gameObject);
            }
        }
    }
}
