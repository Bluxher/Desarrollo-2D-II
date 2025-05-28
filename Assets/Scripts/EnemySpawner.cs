using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [Header("Jugador")]
    public Transform player;

    [Header("Enemigos")]
    public GameObject enemyPrefab;

    [Header("Cantidad aleatoria")]
    public int minEnemigos = 1;
    public int maxEnemigos = 5;

    [Header("Área de spawn (local desde este GameObject)")]
    public Vector2 spawnAreaSize = new Vector2(5f, 5f);

    void Start()
    {
        int cantidad = Random.Range(minEnemigos, maxEnemigos + 1);

        for (int i = 0; i < cantidad; i++)
        {
            Vector2 randomOffset = new Vector2(
                Random.Range(-spawnAreaSize.x / 2, spawnAreaSize.x / 2),
                Random.Range(-spawnAreaSize.y / 2, spawnAreaSize.y / 2)
            );

            Vector2 spawnPosition = (Vector2)transform.position + randomOffset;

            GameObject newEnemy = Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);

            // Asignar el jugador al enemigo
            EnemyAnimation enemyScript = newEnemy.GetComponent<EnemyAnimation>();
            if (enemyScript != null)
            {
                enemyScript.player = player;
            }
        }
    }
}
