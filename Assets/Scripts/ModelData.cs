using UnityEngine;

[CreateAssetMenu(fileName = "NewModelData", menuName = "AR/Model Data")]

public class ModelData : ScriptableObject
{
    public string ModelName;
    
    [TextArea(3, 6)]
    public string ModelDescription;

    public string category;
    public int level;
    public Sprite icon;

}
