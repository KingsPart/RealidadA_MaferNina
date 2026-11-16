using UnityEngine;
using UnityEngine.Rendering;

[CreateAssetMenu(fileName = "NewReaction", menuName = "AR/Potion Reaction")]
public class PotionsReactions : ScriptableObject
{
    [Header("Required Ingredients")]
    public ElementData potionA;
    public ElementData potionB;

    [Header("Result")]
    public ElementData result;

    [Header("Interaction")]
    public float interactionDistance = 0.15f;
    public AudioClip reactionSound;

    public bool CanReact(ElementData a, ElementData b)
    {
        return (a == potionA && b == potionB) || (a == potionB && b == potionA);
    }
}
