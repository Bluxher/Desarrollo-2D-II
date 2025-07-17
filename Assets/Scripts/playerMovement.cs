using System.Collections;
using UnityEngine;

public class playerMovement : MonoBehaviour
{
    public float speed = 5f;
    private Vector2 movementInput;

    private bool canDash = true;
    private bool isDashing;
    private float dashingPower = 24f;
    private float dashingTime = 0.2f;
    private float dashingCooldown = 1f;

    [SerializeField] private Rigidbody2D Rb;
    [SerializeField] private Animator anim;
    [SerializeField] private TrailRenderer tr;


    void Start()
    {

    }

     void Update()
    {
        if(isDashing)
        {
           return;
        }
        movementInput.x = Input.GetAxisRaw("Horizontal");
        movementInput.y = Input.GetAxisRaw("Vertical");

        movementInput = movementInput.normalized;

        anim.SetFloat("Horizontal",movementInput.x);
        anim.SetFloat("Vertical", movementInput.y);
        anim.SetFloat("Speed", movementInput.magnitude);

        if(Input.GetKeyDown(KeyCode.LeftShift) && canDash)
        {
            StartCoroutine(Dash());
        }
    }
    private void FixedUpdate()
    {
        if (isDashing)
        {
            return;
        }
        Rb.linearVelocity = movementInput * speed;
    }

    private IEnumerator Dash()
    {
        if (canDash)
        {
            canDash = false;
            isDashing = true;
            tr.emitting = true;
            Vector2 dashDirection = movementInput.normalized;
            Rb.AddForce(dashDirection * dashingPower, ForceMode2D.Impulse);
            yield return new WaitForSeconds(dashingTime);
            isDashing = false;
            tr.emitting = false;
            yield return new WaitForSeconds(dashingCooldown);
            canDash = true;
        }
    }
}
