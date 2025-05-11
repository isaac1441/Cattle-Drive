using UnityEngine;

public class WolfAI : MonoBehaviour
{
    public float moveSpeed = 1f; // Slow movement speed
    public string cowTag = "Cow";
    private Transform targetCow;
    private bool facingRight = true; // Track current facing direction

    void Update()
    {
        FindClosestCow();

        if (targetCow != null)
        {
            // Move slowly toward the cow
            Vector3 direction = (targetCow.position - transform.position).normalized;
            transform.position += direction * moveSpeed * Time.deltaTime;

            // Flip the wolf only between left and right based on movement direction
            if (direction.x > 0 && !facingRight) // Moving right but facing left
            {
                Flip();
            }
            else if (direction.x < 0 && facingRight) // Moving left but facing right
            {
                Flip();
            }
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

        if (cows.Length == 0)
        {
            targetCow = null;
            return;
        }

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
}
