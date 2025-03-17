using UnityEngine;

[System.Serializable]
public class DialogueLine
{
    [TextArea(3, 5)]
    public string line;
    public string[] choices;
}