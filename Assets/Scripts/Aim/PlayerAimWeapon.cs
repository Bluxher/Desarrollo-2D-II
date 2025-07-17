using UnityEngine;
using System;

public class PlayerAimWeapon : MonoBehaviour
{
    private Transform aimTransform;
    private Transform aimGunEndPointPosition;

    [SerializeField] private PlayerShoot playerShoot;
    private void Awake()
    {
        aimTransform = transform.Find("Gun");
        aimGunEndPointPosition = aimTransform.Find("GunEndPointPosition");
    }

    private void Update()
    {
        HandleAim();
    }

    public static Vector3 GetMouseWorldPosition()
    {
        Vector3 mouseScreenPosition = Input.mousePosition;
        mouseScreenPosition.z = Camera.main.nearClipPlane;

        Vector3 worldPosition = Camera.main.ScreenToWorldPoint(mouseScreenPosition);
        worldPosition.z = 0f; // Juego 2D
        return worldPosition;
    }

    private void HandleAim()
    {
        Vector3 mousePosition = GetMouseWorldPosition();

        // Calcula la dirección desde el Player al mouse
        Vector3 aimDirection = (mousePosition - transform.position).normalized;

        float angle = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg;

        // Rota el arma (Gun), no el Player
        aimTransform.rotation = Quaternion.Euler(0, 0, angle);

        Debug.Log(angle);
    }
}
