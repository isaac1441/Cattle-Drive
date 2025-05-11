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
        MoveVertically();
        HandleDirectionChange();
        AvoidHorse();
    }

    void MoveVertically()
    {
        Vector3 movement = (movingUp ? Vector3.up : Vector3.down) * speed * Time.deltaTime;
        transform.position += movement;
    }

    void HandleDirectionChange()
    {
        directionChangeTimer -= Time.deltaTime;
        if (directionChangeTimer <= 0f)
        {
            movingUp = Random.value > 0.5f;
            directionChangeTimer = changeInterval;
        }
    }

    void AvoidHorse()
    {
        GameObject horse = GameObject.FindGameObjectWithTag("Horse");
        if (horse != null)
        {
            float cowToHorseY = horse.transform.position.y - transform.position.y;

            // Ensure cow changes direction ONLY when the horse is truly close and moving toward it
            if (Mathf.Abs(cowToHorseY) < 2.0f && Mathf.Sign(cowToHorseY) == Mathf.Sign(movingUp ? 1 : -1))
            {
                movingUp = !movingUp; // Flip movement direction to avoid the horse
                directionChangeTimer = changeInterval; // Reset the change timer
            }
        }
    }

    void OnBecameInvisible()
    {
        DestroyCow();
    }

    void DestroyCow()
    {
        if (gameManager != null)
        {
            gameManager.CowDespawned(gameObject);
            gameManager.UnregisterCow(gameObject); // Ensure it is properly removed from tracking
        }
        Destroy(gameObject);
    }

    void OnDestroy()
    {
        DestroyCow(); // Centralized destruction logic
    }
}
