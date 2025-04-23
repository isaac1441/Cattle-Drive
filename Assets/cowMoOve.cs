using UnityEngine;

public class RandomRightMovement : MonoBehaviour
{
    public float speed = 2f;
    private bool movingUp; // Determines if the cow moves up or down
    private float directionChangeTimer; // Tracks time before changing direction
    public float changeInterval = 5f; // How often the cow should randomly change direction

    void Start()
    {
        movingUp = Random.value > 0.5f; // Randomly start moving up or down
        directionChangeTimer = changeInterval; // Set initial time until change
    }

    void Update()
    {
        // Move up or down continuously
        Vector3 movement = (movingUp ? Vector3.up : Vector3.down) * speed * Time.deltaTime;
        transform.position += movement;

        // Reduce the timer over time
        directionChangeTimer -= Time.deltaTime;

        // Change direction less frequently, only when the timer reaches zero
        if (directionChangeTimer <= 0f)
        {
            movingUp = Random.value > 0.5f; // Randomly switch direction
            directionChangeTimer = changeInterval; // Reset timer
        }

        // Check if the cow is heading toward an object tagged "horse"
        GameObject horse = GameObject.FindGameObjectWithTag("Horse");
        if (horse != null)
        {
            float cowToHorseY = horse.transform.position.y - transform.position.y;
            float movementDirection = movement.y;

            // Reverse direction if moving toward the horse
            if (Mathf.Sign(cowToHorseY) == Mathf.Sign(movementDirection) && Mathf.Abs(cowToHorseY) < 2.0f)
            {
                movingUp = Random.value > 0.5f; // Randomly switch direction
                directionChangeTimer = changeInterval; // Reset timer to prevent immediate re-changes
            }
        }
    }
}

