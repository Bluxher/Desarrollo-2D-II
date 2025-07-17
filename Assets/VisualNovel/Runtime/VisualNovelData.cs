using UnityEngine;

[CreateAssetMenu(fileName = "NewVisualNovelData", menuName = "Visual Novel/Data")]
public class VisualNovelData : ScriptableObject
{
    public DialogueSequence[] sequences;
}

[System.Serializable]
public class DialogueSequence
{
    public string sequenceName;
    public DialogueLine[] lines;
}

[System.Serializable]
public class DialogueLine
{
    public string characterName;
    [TextArea] public string text;
    public Sprite characterSprite;
    public AudioClip musicClip;
    public VisualNovelAnimation animation;
    [Range(-1f, 1f)] public float screenPositionX;
    [Range(-1f, 1f)] public float screenPositionY;
    [Range(0.25f, 2f)] public float scale = 1f;
}

public enum VisualNovelAnimation
{
    None,
    FadeIn,
    Left,
    Right,
    Shake
}
