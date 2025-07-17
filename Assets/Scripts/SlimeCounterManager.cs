using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class SlimeCounterManager : MonoBehaviour
{
    [Header("UI")]
    public TextMeshProUGUI slimeCounterText;
    public GameObject winMessage; // El mensaje de "Ganaste"

    private int currentSlimes;

    void Start()
    {
        if (winMessage != null)
            winMessage.SetActive(false); // Asegúrate de ocultarlo al inicio

        UpdateSlimeCount();
    }

    void Update()
    {
        UpdateSlimeCount();
    }

    void UpdateSlimeCount()
    {
        GameObject[] allObjects = GameObject.FindObjectsOfType<GameObject>();
        currentSlimes = 0;

        foreach (GameObject obj in allObjects)
        {
            if (obj.name.StartsWith("Slime"))
            {
                currentSlimes++;
            }
        }

        slimeCounterText.text = "Enemigos: " + currentSlimes;

        if (currentSlimes <= 0)
        {
            winMessage.SetActive(true);
            Invoke("ReturnToMenu", 2f); // Espera 2 segundos y regresa al menú
        }
    }

    void ReturnToMenu()
    {
        SceneManager.LoadScene("MaunMenu"); // Asegúrate de que el nombre coincida exactamente
    }
}
