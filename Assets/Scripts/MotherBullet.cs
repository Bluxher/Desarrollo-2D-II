using UnityEngine;

public class MotherBullet : MonoBehaviour
{
    public GameObject proyectilLateralPrefab;
    public float velocidad = 20f;
    public float intervaloDisparo = 0.5f;
    public float vida = 5f;
    public float fuerzaLateral = 10f;

    private Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.linearVelocity = transform.up * velocidad;

        InvokeRepeating(nameof(DispararLados), 0f, intervaloDisparo);
        Destroy(gameObject, vida);
    }

    void DispararLados()
    {
        DispararEnDireccion(Vector2.Perpendicular(rb.linearVelocity.normalized));      
        DispararEnDireccion(-Vector2.Perpendicular(rb.linearVelocity.normalized));    
    }

    void DispararEnDireccion(Vector2 direccion)
    {
        GameObject bala = Instantiate(proyectilLateralPrefab, transform.position, Quaternion.identity);
        Rigidbody2D rbBala = bala.GetComponent<Rigidbody2D>();

        if (rbBala != null)
            rbBala.linearVelocity = direccion.normalized * fuerzaLateral;

        Destroy(bala, 3f);
    }
}
