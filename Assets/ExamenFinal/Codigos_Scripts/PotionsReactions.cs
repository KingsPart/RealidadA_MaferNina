using UnityEngine;
using UnityEngine.Rendering;

[CreateAssetMenu(fileName = "NewReaction", menuName = "AR/Potion Reaction")]
public class PotionsReactions : ScriptableObject
{
    [Header("Required Ingredients")]
    public ElementData elementA;
    public ElementData elementB;

    [Header("Result")]
    public ElementData result;

    [Header("Interaction")]
    public float interactionDistance = 0.15f;
    public AudioClip reactionSound;

    public bool CanReact(ElementData a, ElementData b)
    {
        return (a == elementA && b == elementB) || (a == elementB && b == elementA);
    }
}
