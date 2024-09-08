using UnityEditor.Callbacks;
using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// This implementation of player controller will move using transform.position and have no acceleration curve
/// </summary>
public class PlayerController : MonoBehaviour
{
    [SerializeField] float moveSpeed = 10f;
    Vector2 moveDirection = Vector2.zero;

    [SerializeField] Rigidbody2D rb;

    // Oops, shouldn't be using Update for movement. Instead it's better to use FixedUpdate for a more stable simulation.
    // Update is called once per frame
    void Update()
    {
        // Transform based movement
        // transform.position = new Vector2(transform.position.x + moveDirection.x * moveSpeed * Time.deltaTime, transform.position.y);
    }

    void FixedUpdate()
    {
        // Rigidbody force based movement
        // float targetSpeed = moveDirection.x * moveSpeed;
        // // Difference vs our current velocity (this is so we apply a smaller force the closer we are to moveSpeed)
        // float speedDifference = targetSpeed - rb.velocity.x;

        // rb.AddForce(speedDifference * Vector2.right);

        // Rigidbody velocity based movement
        // Note: we're overriding the x velocity every time
        rb.velocity = new Vector2(moveDirection.x * moveSpeed, rb.velocity.y);
    }

    void OnMove(InputValue value)
    {
        moveDirection = value.Get<Vector2>();
    }
}
