using UnityEngine;

public class RandomRightMovement : MonoBehaviour
{
    public float speed = 2f;
    private bool movingUp;
    private float directionChangeTimer;
    public float changeInterval = 5f;

    private GameManager gameManager;

    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();

        if (gameManager != null)
        {
            gameManager.RegisterCow(gameObject);
        }

        movingUp = Random.value > 0.5f;
        directionChangeTimer = changeInterval;
    }

    void Update()
    {
        // Move up or down
        Vector3 movement = (movingUp ? Vector3.up : Vector3.down) * speed * Time.deltaTime;
        transform.position += movement;

        // Handle direction change timer
        directionChangeTimer -= Time.deltaTime;
        if (directionChangeTimer <= 0f)
        {
            movingUp = Random.value > 0.5f;
            directionChangeTimer = changeInterval;
        }

        // Avoid horse
        GameObject horse = GameObject.FindGameObjectWithTag("Horse");
        if (horse != null)
        {
            float cowToHorseY = horse.transform.position.y - transform.position.y;
            float movementDirection = movement.y;

            if (Mathf.Sign(cowToHorseY) == Mathf.Sign(movementDirection) && Mathf.Abs(cowToHorseY) < 2.0f)
            {
                movingUp = Random.value > 0.5f;
                directionChangeTimer = changeInterval;
            }
        }
    }

    void OnBecameInvisible()
    {
        Destroy(gameObject);
    }

    void OnDestroy()
    {
        if (gameManager != null)
        {
            gameManager.UnregisterCow(gameObject);
        }
    }
}
