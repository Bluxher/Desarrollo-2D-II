using UnityEngine;

public class EnemyAnimation : MonoBehaviour
{
    public Transform player;
    public float speed = 1f;

    private bool isPlayerInRange = false;
    private Animator animator;
    private Vector2 lastPosition;

    private void Start()
    {
        animator = GetComponent<Animator>();
        lastPosition = transform.position;
    }

    private void Update()
    {
        if (isPlayerInRange && player != null)
        {
            Vector2 currentPosition = transform.position;
            Vector2 direction = ((Vector2)player.position - currentPosition).normalized;

            // Movimiento
            transform.position = Vector2.MoveTowards(currentPosition, player.position, speed * Time.deltaTime);

            // Animaciones
            animator.SetFloat("Horizontal", direction.x);
            animator.SetFloat("Vertical", direction.y);
            animator.SetFloat("Speed", direction.magnitude);

            lastPosition = currentPosition;
        }
        else
        {
            // Si no se mueve, velocidad cero
            animator.SetFloat("Speed", 0f);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInRange = false;
        }
    }
}
