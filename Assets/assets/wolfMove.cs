using UnityEngine;

public class WolfAI : MonoBehaviour
{
    public float moveSpeed = 1f; // Slow movement speed
    public string cowTag = "Cow";
    public string bulletTag = "Bullet"; // Tag for bullets
    private Transform targetCow;
    private bool facingRight = true; // Track current facing direction
    private float searchCooldown = 1f; // How often wolves look for cows
    private float nextSearchTime = 0f;

    public event System.Action OnWolfKilled; // Event to notify WolfSpawner

    void Update()
    {
        if (Time.time >= nextSearchTime)
        {
            FindClosestCow();
            nextSearchTime = Time.time + searchCooldown; // Search every second
        }

        if (targetCow != null)
        {
            MoveTowardsCow();
        }
    }

    void MoveTowardsCow()
    {
        Vector3 direction = (targetCow.position - transform.position).normalized;
        transform.position = Vector3.MoveTowards(transform.position, targetCow.position, moveSpeed * Time.deltaTime);

        // Flip the wolf based on movement direction
        if (direction.x > 0 && !facingRight)
        {
            Flip();
        }
        else if (direction.x < 0 && facingRight)
        {
            Flip();
        }
    }

    void Flip()
    {
        facingRight = !facingRight;
        Vector3 newScale = transform.localScale;
        newScale.x *= -1; // Flip the wolf by inverting X scale
        transform.localScale = newScale;
    }

    void FindClosestCow()
    {
        GameObject[] cows = GameObject.FindGameObjectsWithTag(cowTag);
        targetCow = null;
        if (cows.Length == 0) return;

        float closestDistance = Mathf.Infinity;
        foreach (GameObject cow in cows)
        {
            float distance = Vector3.Distance(transform.position, cow.transform.position);
            if (distance < closestDistance)
            {
                closestDistance = distance;
                targetCow = cow.transform;
            }
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // If hit by a bullet, destroy the wolf
        if (other.CompareTag(bulletTag))
        {
            KillWolf();
            Destroy(other.gameObject); // Remove bullet on impact
        }

        // If collides with a cow, destroy the cow and the wolf
        if (other.CompareTag(cowTag))
        {
            KillWolf();
            Destroy(other.gameObject); // Remove cow
        }
    }

    void KillWolf()
    {
        OnWolfKilled?.Invoke(); // Notify spawner
        Destroy(gameObject); // Destroy the wolf
    }
}
