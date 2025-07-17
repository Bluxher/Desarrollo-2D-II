using UnityEngine;

public class WeaponPartPickup : MonoBehaviour
{
    public string partName;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            WeaponAssembly.Instance.AddPart(partName);
            Destroy(gameObject);
        }
    }
}
