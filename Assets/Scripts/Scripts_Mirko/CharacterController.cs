using UnityEngine;

public class CharacterController : MonoBehaviour
{
    private float moveSpeed = 5f;
    private Rigidbody2D rb;
    private Vector3 moveDirection;
    private bool isDashButtonPressed;


    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    private void Update()
    {

        float moveX = 0f;
        float moveY = 0f;
        if (Input.GetKey(KeyCode.W))
        {
            moveY = 1f;
        }
        if (Input.GetKey(KeyCode.S))
        {
            moveY = -1f;
        }
        if (Input.GetKey(KeyCode.A))
        {
            moveX = -1f;
        }
        if (Input.GetKey(KeyCode.D))
        {
            moveX = 1f; 
        }

        moveDirection = new Vector3(moveX, moveY).normalized;
        if(Input.GetKey(KeyCode.Space))
        {
            isDashButtonPressed = true;
        }
    }

    private void FixedUpdate()
    {
        rb.linearVelocity = moveDirection * moveSpeed;
        
        if (isDashButtonPressed)
        {
            float dashAmount = 1;
            rb.MovePosition(transform.position + moveDirection * dashAmount);
            isDashButtonPressed = false; // Resetear el estado del botón de dash
        }
    }
}
