using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    [Header("Sprites de Vida")]
    public Sprite[] vidaSprites; 

    [Header("UI")]
    public Image vidaImage;      

    [Header("Vida")]
    public int vidaMaxima = 6;
    private int vidaActual;

    void Start()
    {
        vidaActual = vidaMaxima;
        ActualizarBarraDeVida();
    }

    public void RecibirDanio(int cantidad)
    {
        vidaActual -= cantidad;
        vidaActual = Mathf.Clamp(vidaActual, 0, vidaMaxima);
        ActualizarBarraDeVida();

        if (vidaActual <= 0)
        {
            Debug.Log("Jugador derrotado.");
            ReiniciarEscena();
        }
    }

    void ActualizarBarraDeVida()
    {
        if (vidaActual >= 0 && vidaActual < vidaSprites.Length)
        {
            vidaImage.sprite = vidaSprites[vidaActual];
        }
    }

    public void ReiniciarEscena()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
