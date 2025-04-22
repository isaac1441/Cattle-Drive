using UnityEngine;

public class RandomRightMovement : MonoBehaviour
{
    public float speed = 1f; // Adjust speed as needed
    public float directionChangeInterval = .1f;
    public float detectionRadius = 0.1f; // 10 pixels converted to world units
    private Vector3 direction;

    void Start()
    {
        ChangeDirection();
        InvokeRepeating(nameof(ChangeDirection), directionChangeInterval, directionChangeInterval);
    }

    void Update()
    {
        transform.position += direction * speed * Time.deltaTime;

        // Check for nearby "Horse" objects
        Collider2D[] nearbyHorses = Physics2D.OverlapCircleAll(transform.position, detectionRadius);
        foreach (Collider2D horse in nearbyHorses)
        {
            if (horse.CompareTag("Horse"))
            {
                ChangeAngle(); // Adjust movement angle when close
                break;
            }
        }
    }

    void ChangeDirection()
    {
        float randomAngle = Random.Range(-30f, 30f); // Slight angle variation
        direction = Quaternion.Euler(0, 0, randomAngle) * Vector3.right; // Moves right with random angles
    }

    void ChangeAngle()
    {
        direction = Quaternion.Euler(0, 0, -Vector3.SignedAngle(Vector3.right, direction, Vector3.forward)) * Vector3.right;
    }
}

