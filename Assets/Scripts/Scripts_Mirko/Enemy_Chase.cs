using UnityEngine;

public class Enemy_Chase : MonoBehaviour
{
    public Transform player;
    public float speed = 1f;

    private bool isPlayerInRange = false;

    private void Update()
    {
        if (isPlayerInRange)
        {
            transform.position = Vector2.MoveTowards(transform.position, player.position, speed * Time.deltaTime);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Triggered by: " + other.name);
        if (other.CompareTag("Player"))
        {
            isPlayerInRange = true;
        }
        
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        Debug.Log("Exit by: " + other.name);
        if (other.CompareTag("Player"))
        {
            isPlayerInRange = false;
        }
        Debug.Log("tocando");

    }
}
