using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// This implementation of player controller will move using transform.position and have no acceleration curve
/// </summary>
public class PlayerController : MonoBehaviour
{
    [SerializeField] float moveSpeed = 10f;
    Vector2 moveDirection = Vector2.zero;

    // Oops, shouldn't be using Update for movement. Instead it's better to use FixedUpdate for a more stable simulation.
    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector2(transform.position.x + moveDirection.x * moveSpeed * Time.deltaTime, transform.position.y);
    }

    void OnMove(InputValue value)
    {
        moveDirection = value.Get<Vector2>();
    }
}
