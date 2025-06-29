using UnityEngine;
using System.Collections;

public class BossShoots : MonoBehaviour
{
    public enum TipoOleada { Gradual360, Instantanea360 }

    [Header("Configuración general")]
    public GameObject proyectilPrefab;
    public Transform crossHair;
    public float shootingInterval = 0.1f;
    private float proyectileVelocity = 20f;
    private float destroyProyectileAfter = 5f;

    [Header("Sentido de rotación")]
    public bool invertirDireccion = false;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            StopAllCoroutines();
            TipoOleada tipo = (TipoOleada)Random.Range(0, 2); // 0 o 1
            StartCoroutine(DispararOleada(tipo));
        }
    }

    IEnumerator DispararOleada(TipoOleada tipo)
    {
        while (true)
        {
            float anguloIncremento = Random.Range(10f, 30f);
            float intervaloEntreOleadas = Random.Range(1, 4);

            switch (tipo)
            {
                case TipoOleada.Gradual360:
                    if (invertirDireccion)
                    {
                        for (float angulo = 360f; angulo > 0f; angulo -= anguloIncremento)
                        {
                            DispararEnDireccion(angulo);
                            yield return new WaitForSeconds(shootingInterval);
                        }
                    }
                    else
                    {
                        for (float angulo = 0f; angulo < 360f; angulo += anguloIncremento)
                        {
                            DispararEnDireccion(angulo);
                            yield return new WaitForSeconds(shootingInterval);
                        }
                    }
                    break;

                case TipoOleada.Instantanea360:
                    for (float angulo = 0f; angulo < 360f; angulo += anguloIncremento)
                    {
                        DispararEnDireccion(angulo);
                    }
                    break;
            }

            yield return new WaitForSeconds(intervaloEntreOleadas);
        }
    }

    void DispararEnDireccion(float angulo)
    {
        if (proyectilPrefab == null || crossHair == null) return;

        GameObject proyectil = Instantiate(proyectilPrefab, crossHair.position, Quaternion.identity);
        Rigidbody2D rb = proyectil.GetComponent<Rigidbody2D>();

        if (rb != null)
        {
            float rad = angulo * Mathf.Deg2Rad;
            Vector2 direccion = new Vector2(Mathf.Cos(rad), Mathf.Sin(rad)).normalized;
            rb.linearVelocity = direccion * proyectileVelocity;
        }

        Destroy(proyectil, destroyProyectileAfter);
    }
}
