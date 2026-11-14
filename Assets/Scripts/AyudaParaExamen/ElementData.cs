using UnityEngine;

[CreateAssetMenu(fileName = "NewElement", menuName = "AR/Element Data")]

public class ElementData : ScriptableObject
{
    [Header("Information")]
    public string elementName;

    [TextArea(3, 5)]
    public string description;

    public int level;
    public Sprite icon;

    [Header("Model")]
    public GameObject prefab;
}
