using UnityEngine;

public class PlayerBullet : MonoBehaviour
{
    public int damage = 10; // Daño configurable en el Inspector

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Daño al Boss
        if (collision.CompareTag("Boss"))
        {
            BossController boss = collision.GetComponent<BossController>();
            if (boss != null)
            {
                boss.TakeDamage(damage);
            }

            Destroy(gameObject);
            return;
        }

        // Daño a enemigos pequeños
        if (collision.CompareTag("Enemy"))
        {
            EnemyHealth enemy = collision.GetComponent<EnemyHealth>();
            if (enemy != null)
            {
                enemy.TakeDamage(damage);
            }
            else if (collision.GetComponent<EnemyAnimation>())
            {
                // Si no tienes sistema de salud aún, destruye directamente
                Destroy(collision.gameObject);
            }

            Destroy(gameObject);
            return;
        }

        // Opcional: Destruir si choca con paredes
        if (collision.CompareTag("Wall"))
        {
            Destroy(gameObject);
        }
    }
}
