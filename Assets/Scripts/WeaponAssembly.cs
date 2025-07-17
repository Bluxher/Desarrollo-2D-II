using System.Collections.Generic;
using UnityEngine;

public class WeaponAssembly : MonoBehaviour
{
    public static WeaponAssembly Instance;

    private HashSet<string> collectedParts = new HashSet<string>();

    void Awake()
    {
        Instance = this;
    }

    public void AddPart(string partName)
    {
        if (collectedParts.Add(partName))
        {
            Debug.Log("Pieza recogida: " + partName);
        }

        if (collectedParts.Count >= 2) // Cambia a 4 si luego usas 4 piezas
        {
            Debug.Log("¡Arma final desbloqueada!");
        }
    }
}
