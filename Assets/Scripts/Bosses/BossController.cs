using UnityEngine;
using System.Collections;

public class BossController : MonoBehaviour
{
    public GameObject healthBarCanvas;
    public BossHealthUI healthUI;

    public int maxHealth = 100;
    public int currentHealth;

    public GameObject weaponPartDrop;

    public float detectionRange = 10f;
    protected bool playerDetected = false;

    private GameObject player;

    void Start()
    {
        currentHealth = maxHealth;
        if (healthBarCanvas != null)
            healthBarCanvas.SetActive(false);

        player = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        if (player == null) return;

        float distance = Vector3.Distance(transform.position, player.transform.position);

        if (!playerDetected && distance <= detectionRange)
        {
            playerDetected = true;
            if (healthBarCanvas != null)
                healthBarCanvas.SetActive(true);
        }
        else if (playerDetected && distance > detectionRange)
        {
            playerDetected = false;
            if (healthBarCanvas != null)
                healthBarCanvas.SetActive(false);
        }

        if (playerDetected)
            BossBehavior();
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        if (healthUI != null)
            healthUI.SetHealth(currentHealth, maxHealth);

        if (currentHealth <= 0)
            StartCoroutine(DeathAnimation());
    }

    protected virtual void Die()
    {
        if (healthBarCanvas != null)
            healthBarCanvas.SetActive(false);

        if (weaponPartDrop != null)
            Instantiate(weaponPartDrop, transform.position, Quaternion.identity);

        Destroy(gameObject);
    }

    IEnumerator DeathAnimation()
    {
        SpriteRenderer sr = GetComponent<SpriteRenderer>();
        if (sr != null)
        {
            for (int i = 0; i < 5; i++)
            {
                sr.color = new Color(1, 1, 1, 0.3f);
                yield return new WaitForSeconds(0.1f);
                sr.color = new Color(1, 1, 1, 1f);
                yield return new WaitForSeconds(0.1f);
            }

            float fadeTime = 1f;
            float t = 0;

            while (t < fadeTime)
            {
                t += Time.deltaTime;
                sr.color = new Color(1, 1, 1, Mathf.Lerp(1, 0, t / fadeTime));
                yield return null;
            }
        }

        Die();
    }

    protected virtual void BossBehavior() { }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionRange);
    }
}
