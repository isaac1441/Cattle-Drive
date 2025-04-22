using UnityEngine;

public class move : MonoBehaviour
{
    public float speed = 2f;
    private Vector3 target;

    void Start()
    {
        target = transform.position; // Start at the current position
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0)) // Left-click to set a new target position
        {
            target = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            target.z = transform.position.z; // Keep the Z position unchanged
        }

        // Move the character towards the target position smoothly
        transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);
    }
}