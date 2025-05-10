using UnityEngine;

public class WolfAI : MonoBehaviour
{
    public float moveSpeed = 0.5f; // Slow movement speed
    public string cowTag = "Cow";
    private Transform targetCow;

    void Update()
    {
        FindClosestCow();

        if (targetCow != null)
        {
            // Move slowly toward the cow
            transform.position = Vector3.Lerp(transform.position, targetCow.position, moveSpeed * Time.deltaTime);
        }
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
