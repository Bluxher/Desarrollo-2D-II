using UnityEngine;

public class playerMovement : MonoBehaviour
{
    public float speed = 5f;
    private Rigidbody2D Rb;
    private Vector2 movementInput;
    private Animator anim;

    void Start()
    {
        Rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();

    }

     void Update()
    {
        movementInput.x = Input.GetAxisRaw("Horizontal");
        movementInput.y = Input.GetAxisRaw("Vertical");

        movementInput = movementInput.normalized;

        anim.SetFloat("Horizontal",movementInput.x);
        anim.SetFloat("Vertical", movementInput.y);
        anim.SetFloat("Speed", movementInput.magnitude);


    }
    private void FixedUpdate()
    {
        Rb.linearVelocity = movementInput * speed;
    }
}
