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
    bool disableInput = false;

    [SerializeField] Rigidbody2D rb;
    [SerializeField] RespawnSystem respawnSystem;

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
        if (!disableInput)
            moveDirection = value.Get<Vector2>();
    }

    public void Die()
    {
        respawnSystem.RespawnPlayer();
    }

    public void FreezePlayer()
    {
        rb.gravityScale = 0f;
        rb.velocity = Vector2.zero;
        disableInput = true;
        moveDirection = Vector2.zero;
    }

    public void UnfreezePlayer()
    {
        rb.gravityScale = 1f;
        disableInput = false;
    }
}
