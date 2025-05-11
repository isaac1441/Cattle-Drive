using UnityEngine;

public class move : MonoBehaviour
{
    public float speed = 2f;

    void Update()
    {
        // Get input from WASD keys
        float moveX = 0f;
        float moveY = 0f;

        if (Input.GetKey(KeyCode.W)) moveY = 1f;
        if (Input.GetKey(KeyCode.S)) moveY = -1f;
        if (Input.GetKey(KeyCode.A)) moveX = -1f;
        if (Input.GetKey(KeyCode.D)) moveX = 1f;

        Vector3 moveDirection = new Vector3(moveX, moveY, 0f).normalized;

        // Move the object
        transform.position += moveDirection * speed * Time.deltaTime;

        // Flip sprite based on horizontal movement
        if (Mathf.Abs(moveX) > 0.01f)
        {
            Vector3 scale = transform.localScale;
            scale.x = moveX > 0 ? Mathf.Abs(scale.x) : -Mathf.Abs(scale.x);
            transform.localScale = scale;
        }
    }
}
