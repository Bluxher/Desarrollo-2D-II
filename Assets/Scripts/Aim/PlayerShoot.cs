using TMPro;
using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    [Header("Arma y Disparo")]
    public GameObject gun;              // El objeto Gun (se activa al recogerla)
    public GameObject bulletPrefab;     // Prefab de la bala
    public Transform shootPoint;        // Punto de salida de la bala
    public float bulletSpeed = 10f;     // Velocidad de la bala

    [Header("Munición")]
    public int currentAmmo = 10;        // Balas actuales
    public int maxAmmo = 10;            // Máximo de balas en el cargador
    public int extraMagazines = 0;      // Cargadores extra

    [Header("Estado")]
    public bool hasWeapon = false;      // Si el jugador tiene el arma

    [Header("UI")]
    public TextMeshProUGUI bulletsText;
    public TextMeshProUGUI magazinesText;

    //Colores Originales
    private Color originalBulletColor;
    private Color originalMagColor;

    void Start()
    {
        if (gun != null)
            gun.SetActive(false);


        if (bulletsText != null) originalBulletColor = bulletsText.color;
        if (magazinesText != null) originalMagColor = magazinesText.color;
        
        UpdateUI();
    }

    void Update()
    {
        if (!hasWeapon) return;

        if (Input.GetMouseButtonDown(0))
        {
            Shoot();
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            Reload();
        }
    }

    void Shoot()
    {
        if (currentAmmo <= 0)
        {
            Debug.Log("Sin balas. Recarga con R.");
            NoAmmo();
            return;
        }

        GameObject bullet = Instantiate(bulletPrefab, shootPoint.position, shootPoint.rotation);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        rb.linearVelocity = shootPoint.right * bulletSpeed;

        currentAmmo--;

        UpdateUI();
    }

    void Reload()
    {
        if (extraMagazines > 0 && currentAmmo < maxAmmo)
        {
            currentAmmo = maxAmmo;
            extraMagazines--;
            Debug.Log("Recargado!");

            NoReload();
        }
        else
        {
            Debug.Log("No tienes cargadores.");
        }

        UpdateUI();
    }

    void UpdateUI()
    {
        if (bulletsText != null)
            bulletsText.text = currentAmmo + "/" + maxAmmo;

        if (magazinesText != null)
            magazinesText.text = "x" + extraMagazines;
    }

    void NoAmmo()
    {
        if (bulletsText != null)
        {
            LeanTween.value(gameObject, bulletsText.color, Color.red, 0.15f)
                .setOnUpdate((Color val) => { bulletsText.color = val; })
                .setLoopPingPong(2)
                .setOnComplete(() => { bulletsText.color = originalBulletColor; });
        }
    }

    void NoReload()
    {
        if (bulletsText != null)
        {
            LeanTween.value(gameObject, bulletsText.color, Color.yellow, 0.2f)
                .setOnUpdate((Color val) => { bulletsText.color = val; })
                .setLoopPingPong(1)
                .setOnComplete(() => { bulletsText.color = originalBulletColor; });
        }

        if (magazinesText != null)
        {
            LeanTween.value(gameObject, magazinesText.color, Color.yellow, 0.2f)
                .setOnUpdate((Color val) => { magazinesText.color = val; })
                .setLoopPingPong(1)
                .setOnComplete(() => { magazinesText.color = originalMagColor; });
        }
    }
}
