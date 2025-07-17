using UnityEngine;

public class BossOleadas : BossController
{
    [Header("Ataque de Oleadas")]
    public GameObject bulletPrefab;
    public float shootInterval = 1f;
    private float shootTimer;

    [Header("Movimiento Aleatorio")]
    public Vector2 areaSize = new Vector2(5f, 3f);
    public float moveSpeed = 2f;
    public float changeDirectionTime = 2f;

    private Vector2 moveDirection;
    private float moveTimer;

    protected override void BossBehavior()
    {
        // Movimiento aleatorio
        moveTimer += Time.deltaTime;

        if (moveTimer >= changeDirectionTime)
        {
            Vector2 targetPoint = GetRandomPointInArea();
            moveDirection = (targetPoint - (Vector2)transform.position).normalized;
            moveTimer = 0f;
        }

        transform.Translate(moveDirection * moveSpeed * Time.deltaTime);

        // Disparo en oleadas
        shootTimer += Time.deltaTime;
        if (shootTimer >= shootInterval)
        {
            BulletHellAttack();
            shootTimer = 0;
        }
    }

    void BulletHellAttack()
    {
        int bullets = 12;
        for (int i = 0; i < bullets; i++)
        {
            float angle = i * (360f / bullets);
            Vector3 dir = Quaternion.Euler(0, 0, angle) * Vector3.right;

            GameObject bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
            bullet.GetComponent<Rigidbody2D>().linearVelocity = dir * 5f;
        }
    }

    Vector2 GetRandomPointInArea()
    {
        Vector2 origin = (Vector2)transform.position;
        float x = Random.Range(-areaSize.x / 2, areaSize.x / 2);
        float y = Random.Range(-areaSize.y / 2, areaSize.y / 2);
        return origin + new Vector2(x, y);
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(transform.position, areaSize);
    }
}
