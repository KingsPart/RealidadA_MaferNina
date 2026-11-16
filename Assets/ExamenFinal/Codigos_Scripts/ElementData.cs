using UnityEngine;

[CreateAssetMenu(fileName = "NewPotion", menuName = "AR/Potions Data")]

public class ElementData : ScriptableObject
{
    [Header("Information")]
    public string PotionName;

    [TextArea(3, 5)]
    public string description;

    public int level;
    public Sprite icon;

    [Header("Model")]
    public GameObject prefab;
}
