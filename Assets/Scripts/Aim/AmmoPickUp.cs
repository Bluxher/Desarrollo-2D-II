using UnityEngine;

public class AmmoPickUp : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerShoot playerShoot = other.GetComponent<PlayerShoot>();
            playerShoot.extraMagazines++;
            Destroy(gameObject);
        }
    }
}