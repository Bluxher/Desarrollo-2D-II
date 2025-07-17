using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Linq;

public class VisualNovelPlayer : MonoBehaviour
{
    public VisualNovelData dialogueData;
    public string sequenceName = "Introduction";

    [Header("UI References")]
    public Image vnImage;
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI dialogueText;
    public Button buttonNext;
    public Button buttonPrev;
    public Button buttonEnd;
    public AudioSource musicSource;

    private DialogueSequence currentSequence;
    private int currentLine = 0;
    private bool dialogueActive = false;

    void Start()
    {
        buttonNext.onClick.AddListener(NextLine);
        buttonPrev.onClick.AddListener(PreviousLine);
        buttonEnd.onClick.AddListener(EndDialogue);
    }

    void Update()
    {
        if (!dialogueActive) return;

        if (Input.GetKeyDown(KeyCode.Space))
        {
            NextLine();
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            EndDialogue();
        }
    }

    public void StartDialogueExternally()
    {
        currentLine = 0;
        currentSequence = dialogueData.sequences.FirstOrDefault(s => s.sequenceName == sequenceName);

        if (currentSequence == null)
        {
            Debug.LogError($"Secuencia {sequenceName} no encontrada.");
            return;
        }

        dialogueActive = true;

        buttonNext.interactable = true;
        buttonPrev.interactable = true;
        buttonEnd.interactable = true;

        vnImage.gameObject.SetActive(false);
        nameText.text = "";
        dialogueText.text = "";

        PlayLine();
    }

    void PlayLine()
    {
        if (currentLine < 0 || currentLine >= currentSequence.lines.Length) return;

        DialogueLine line = currentSequence.lines[currentLine];

        nameText.text = line.characterName;
        dialogueText.text = line.text;

        if (line.characterSprite != null)
        {
            vnImage.sprite = line.characterSprite;
            vnImage.gameObject.SetActive(true);
        }
        else
        {
            vnImage.gameObject.SetActive(false);
        }

        if (line.musicClip != null)
        {
            musicSource.clip = line.musicClip;
            musicSource.Play();
        }

        buttonPrev.interactable = currentLine > 0;
    }

    void NextLine()
    {
        currentLine++;
        if (currentLine >= currentSequence.lines.Length)
        {
            EndDialogue();
        }
        else
        {
            PlayLine();
        }
    }

    void PreviousLine()
    {
        currentLine = Mathf.Max(0, currentLine - 1);
        PlayLine();
    }

    void EndDialogue()
    {
        dialogueText.text = "";
        nameText.text = "";
        vnImage.gameObject.SetActive(false);
        musicSource.Stop();

        buttonNext.interactable = false;
        buttonPrev.interactable = false;
        buttonEnd.interactable = false;

        dialogueActive = false;

        FindObjectOfType<VisualNovelManager>()?.EndDialogue();
    }
}
