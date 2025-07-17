using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [Header("Referencias")]
    public playerMovement playerMovement;
    public PlayerShoot playerShoot;
    public PlayerAimWeapon playerAim;

    private bool controlsLocked = false;

    void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    public void LockPlayerControls()
    {
        controlsLocked = true;
        playerMovement.enabled = false;
        playerShoot.enabled = false;
        playerAim.enabled = false;
    }

    public void UnlockPlayerControls()
    {
        controlsLocked = false;
        playerMovement.enabled = true;
        playerShoot.enabled = true;
        playerAim.enabled = true;
    }

    public bool AreControlsLocked()
    {
        return controlsLocked;
    }
}
