using UnityEngine;

public class WeaponPickUp : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerShoot playerShoot = other.GetComponent<PlayerShoot>();
            playerShoot.gun.SetActive(true);
            playerShoot.hasWeapon = true;
            Destroy(gameObject);
        }
    }
}
