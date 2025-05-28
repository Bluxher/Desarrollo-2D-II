using UnityEngine;

public class CamaraFollow : MonoBehaviour
{
    public Transform target;
    public Vector3 offset = new Vector3(0f, 0f, -10f);
    public float smoothSpeed = 0.125f;

    [Header("Límites")]
    public Vector2 minLimits;
    public Vector2 maxLimits;

    void LateUpdate()
    {
        if (target == null) return;

        Vector3 desiredPosition = target.position + offset;
        Vector3 clampedPosition = new Vector3(
            Mathf.Clamp(desiredPosition.x, minLimits.x, maxLimits.x),
            Mathf.Clamp(desiredPosition.y, minLimits.y, maxLimits.y),
            desiredPosition.z
        );

        transform.position = Vector3.Lerp(transform.position, clampedPosition, smoothSpeed);
    }

    void OnDrawGizmosSelected()
    {
        // Dibuja un rectángulo para visualizar los límites
        Gizmos.color = Color.green;
        Vector3 center = new Vector3(
            (minLimits.x + maxLimits.x) / 2,
            (minLimits.y + maxLimits.y) / 2,
            transform.position.z
        );
        Vector3 size = new Vector3(
            Mathf.Abs(maxLimits.x - minLimits.x),
            Mathf.Abs(maxLimits.y - minLimits.y),
            1f
        );
        Gizmos.DrawWireCube(center, size);
    }
}
