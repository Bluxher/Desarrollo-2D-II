using UnityEngine;
using UnityEngine.UI;

public class VisualNovelManager : MonoBehaviour
{
    public GameObject interactionPrompt; // Icono de la tecla E
    public GameObject visualNovelUI;    // UI de diálogo activa/desactiva
    public VisualNovelPlayer visualNovelPlayer;
    public VisualNovelData dialogueData;
    public string sequenceName = "Introduction";

    private bool playerInRange = false;
    private bool dialogueStarted = false;

    //Dar arma
    public bool giveWeaponOnEnd = false; // Activa esto en el NPC que da el arma
    public PlayerShoot playerShoot;       // Referencia al PlayerShoot
    void Start()
    {
        interactionPrompt.SetActive(false);
        visualNovelUI.SetActive(false);
    }

    void Update()
    {
        if (playerInRange && !dialogueStarted && Input.GetKeyDown(KeyCode.E))
        {
            StartDialogue();
        }
    }

    void StartDialogue()
    {
        dialogueStarted = true;
        interactionPrompt.SetActive(false);
        visualNovelUI.SetActive(true);

        visualNovelPlayer.dialogueData = dialogueData;
        visualNovelPlayer.sequenceName = sequenceName;

        visualNovelPlayer.StartDialogueExternally();

        GameManager.Instance.LockPlayerControls();
    }

    public void EndDialogue()
    {
        dialogueStarted = false;
        visualNovelUI.SetActive(false);
        GameManager.Instance.UnlockPlayerControls();

        if (giveWeaponOnEnd && playerShoot != null)
        {
            playerShoot.hasWeapon = true;
            playerShoot.gun.SetActive(true);
            Debug.Log("¡Has conseguido el arma!");
        }
    }


    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !dialogueStarted)
        {
            interactionPrompt.SetActive(true);
            playerInRange = true;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            interactionPrompt.SetActive(false);
            playerInRange = false;
        }
    }
}