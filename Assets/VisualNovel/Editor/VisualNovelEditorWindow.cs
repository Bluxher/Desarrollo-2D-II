using UnityEngine;
using UnityEditor;
using UnityEngine.UI;
using TMPro;

public class VisualNovelEditorWindow : EditorWindow
{
    VisualNovelData data;
    Vector2 scrollPos;
    int currentLine = 0;
    int currentSequence = 0;

    GameObject previewDialogueUI;

    GameObject dialogueUIPrefab; // Campo para el prefab

    [MenuItem("Tools/Visual Novel Editor")]
    public static void OpenWindow()
    {
        GetWindow<VisualNovelEditorWindow>("VN Editor");
    }

    void OnGUI()
    {
        GUILayout.Label("Visual Novel Editor", EditorStyles.boldLabel);

        data = (VisualNovelData)EditorGUILayout.ObjectField("Visual Novel Data", data, typeof(VisualNovelData), false);

        dialogueUIPrefab = (GameObject)EditorGUILayout.ObjectField("Prefab de Diálogo", dialogueUIPrefab, typeof(GameObject), false);

        if (data == null) return;

        if (data.sequences == null || data.sequences.Length == 0)
        {
            if (GUILayout.Button("Crear Nueva Secuencia"))
            {
                data.sequences = new DialogueSequence[1];
                data.sequences[0] = new DialogueSequence { sequenceName = "Nueva Secuencia", lines = new DialogueLine[1] { new DialogueLine() } };
            }
            return;
        }

        string[] sequenceNames = new string[data.sequences.Length];
        for (int i = 0; i < data.sequences.Length; i++)
        {
            sequenceNames[i] = data.sequences[i].sequenceName;
        }

        currentSequence = EditorGUILayout.Popup("Secuencia", currentSequence, sequenceNames);

        var seq = data.sequences[currentSequence];

        seq.sequenceName = EditorGUILayout.TextField("Nombre de Secuencia", seq.sequenceName);

        if (seq.lines == null || seq.lines.Length == 0)
        {
            if (GUILayout.Button("Agregar Línea Inicial"))
            {
                seq.lines = new DialogueLine[1];
                seq.lines[0] = new DialogueLine();
            }
            return;
        }

        scrollPos = EditorGUILayout.BeginScrollView(scrollPos);

        GUILayout.Label($"Editando Línea {currentLine + 1}/{seq.lines.Length}", EditorStyles.helpBox);

        var line = seq.lines[currentLine];

        line.characterName = EditorGUILayout.TextField("Nombre", line.characterName);
        line.text = EditorGUILayout.TextArea(line.text, GUILayout.Height(60));
        line.characterSprite = (Sprite)EditorGUILayout.ObjectField("Sprite", line.characterSprite, typeof(Sprite), false);
        line.musicClip = (AudioClip)EditorGUILayout.ObjectField("Música", line.musicClip, typeof(AudioClip), false);

        GUILayout.Space(10);
        GUILayout.Label("Configuración de Imagen:", EditorStyles.boldLabel);

        line.animation = (VisualNovelAnimation)EditorGUILayout.EnumPopup("Animación", line.animation);
        line.screenPositionX = EditorGUILayout.Slider("Posición X", line.screenPositionX, -1f, 1f);
        line.screenPositionY = EditorGUILayout.Slider("Posición Y", line.screenPositionY, -1f, 1f);
        line.scale = EditorGUILayout.Slider("Escala", line.scale, 0.25f, 2f);

        GUILayout.Space(10);

        if (GUILayout.Button("Previsualizar Línea en Escena"))
        {
            CreatePreview(line);
        }

        if (GUILayout.Button("Guardar Previsualización en Línea"))
        {
            SavePreviewToLine(line);
        }

        if (GUILayout.Button("Limpiar Previsualización"))
        {
            ClearPreview();
        }

        GUILayout.Space(10);

        EditorGUILayout.BeginHorizontal();

        if (GUILayout.Button("< Línea Anterior"))
        {
            currentLine = Mathf.Max(0, currentLine - 1);
            CreatePreview(seq.lines[currentLine]);
        }

        if (GUILayout.Button("Siguiente Línea >"))
        {
            currentLine = Mathf.Min(seq.lines.Length - 1, currentLine + 1);
            CreatePreview(seq.lines[currentLine]);
        }

        EditorGUILayout.EndHorizontal();

        EditorGUILayout.EndScrollView();

        GUILayout.Space(10);

        EditorGUILayout.BeginHorizontal();
        if (GUILayout.Button("+ Agregar Nueva Secuencia"))
        {
            ArrayUtility.Add(ref data.sequences, new DialogueSequence { sequenceName = "Nueva Secuencia", lines = new DialogueLine[1] { new DialogueLine() } });
            currentSequence = data.sequences.Length - 1;
            currentLine = 0;
        }

        if (GUILayout.Button("- Eliminar Secuencia Actual"))
        {
            if (data.sequences.Length > 1)
            {
                ArrayUtility.RemoveAt(ref data.sequences, currentSequence);
                currentSequence = Mathf.Clamp(currentSequence, 0, data.sequences.Length - 1);
                currentLine = 0;
            }
        }
        EditorGUILayout.EndHorizontal();

        if (GUI.changed)
        {
            EditorUtility.SetDirty(data);
        }
    }

    void CreatePreview(DialogueLine line)
    {
        if (previewDialogueUI != null)
            ClearPreview();

        if (dialogueUIPrefab != null)
        {
            previewDialogueUI = (GameObject)PrefabUtility.InstantiatePrefab(dialogueUIPrefab);
            previewDialogueUI.name = "Preview_DialogueUI";

            // Set text and image
            Transform nameText = previewDialogueUI.transform.Find("Background/Panel_Content/Panel Container/Panel/Panel_Name/Name");
            Transform dialogueText = previewDialogueUI.transform.Find("Background/Panel_Content/Panel Container/Panel/Panel_Text/Dialogue_Text");
            Transform vnImage = previewDialogueUI.transform.Find("ImageParent/VN_Image");

            if (nameText != null) nameText.GetComponent<TextMeshProUGUI>().text = line.characterName;
            if (dialogueText != null) dialogueText.GetComponent<TextMeshProUGUI>().text = line.text;
            if (vnImage != null)
            {
                Image img = vnImage.GetComponent<Image>();
                img.sprite = line.characterSprite;

                RectTransform rect = vnImage.GetComponent<RectTransform>();
                rect.anchoredPosition = new Vector2(line.screenPositionX * (Screen.width / 2f), line.screenPositionY * (Screen.height / 2f));
                rect.localScale = Vector3.one * line.scale;

                // Selecciona primero el texto para editar en escena
                if (dialogueText != null)
                    Selection.activeGameObject = dialogueText.gameObject;
                else
                    Selection.activeGameObject = vnImage.gameObject;
            }
        }
    }

    void SavePreviewToLine(DialogueLine line)
    {
        if (previewDialogueUI == null) return;

        Transform vnImage = previewDialogueUI.transform.Find("ImageParent/VN_Image");
        if (vnImage == null) return;

        RectTransform rect = vnImage.GetComponent<RectTransform>();

        line.screenPositionX = rect.anchoredPosition.x / (Screen.width / 2f);
        line.screenPositionY = rect.anchoredPosition.y / (Screen.height / 2f);
        line.scale = rect.localScale.x;

        // Guardar también nombre y texto desde la previsualización
        Transform nameText = previewDialogueUI.transform.Find("Background/Panel_Content/Panel Container/Panel/Panel_Name/Name");
        Transform dialogueText = previewDialogueUI.transform.Find("Background/Panel_Content/Panel Container/Panel/Panel_Text/Dialogue_Text");

        if (nameText != null) line.characterName = nameText.GetComponent<TextMeshProUGUI>().text;
        if (dialogueText != null) line.text = dialogueText.GetComponent<TextMeshProUGUI>().text;

        EditorUtility.SetDirty(data);
    }

    void ClearPreview()
    {
        if (previewDialogueUI != null)
            DestroyImmediate(previewDialogueUI);
    }
}