using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

// Este codigo va a controlar las combinaciones entre los modelos, comprueba si las
// dos imagenes son validas, calcula su distancia y crea el resultado y si deja de rastrear
// se cancela automaticamente.
public class ARReactionManager : MonoBehaviour
{
    [Header("Spawner")]
    [SerializeField] private ARImageSpawner imageSpawner;

    [Header("Available Reactions")]
    [SerializeField] private List<PotionsReactions> reactions = new List<PotionsReactions>();

    [Header("Audio")]
    [SerializeField] private AudioSource audioSource;

    GameObject resultInstance;

    // Pociones participando en la reaccion activa
    TrackedPotion reactingA;
    TrackedPotion reactingB;

    bool hasInteracted = false;

    private void Update()
    {
        if (hasInteracted)
        {
            if (reactingA == null || reactingB == null || !reactingA.IsTracking() || !reactingB.IsTracking())
            {
                CancelReaction();
                return;
            }

            // Comprobar si las pociones siguen suficientemente cerca
            float distance = Vector3.Distance(reactingA.GetPosition(), reactingB.GetPosition());

            // Si se alejan, cancelar la combinacion
            PotionsReactions activeReaction = null;

            foreach (PotionsReactions reaction in reactions)
            {
                if (reaction != null && reaction.CanReact(reactingA.data, reactingB.data))
                {
                    activeReaction = reaction;
                    break;
                }
            }

            if (activeReaction != null && distance > activeReaction.interactionDistance)
            {
                CancelReaction();
                return;
            }

            return;
        }

        //Obtener las pociones trackeados actualmente
        List<TrackedPotion> visiblePotions = new List<TrackedPotion>();

        foreach (TrackedPotion potion in imageSpawner.SpawnedPotions)
        {
            if(potion == null)
            {
                Debug.LogWarning("ARReactionManager: TrackedPotion is NULL!");
                continue;
            }

            if(potion.IsTracking() && !potion.isReacting)
            {
                visiblePotions.Add(potion);
            }
        }

        for(int i = 0; i < visiblePotions.Count; i++)
        {
            for(int j = i + 1; j < visiblePotions.Count; j++)
            {
                if (CheckReaction(visiblePotions[i], visiblePotions[j]))
                    return;
            }
        }
    }

    bool CheckReaction(TrackedPotion a, TrackedPotion b)
    {
        foreach (PotionsReactions reaction in reactions)
        {
            if (reaction == null)
                continue;

            if (!reaction.CanReact(a.data, b.data))
                continue;

            float distance = Vector3.Distance(a.GetPosition(), b.GetPosition());

            if(distance > reaction.interactionDistance)
                continue;

            if(reaction.result == null || reaction.result.prefab == null)
                continue;

            ExecuteReaction(a, b, reaction);
            return true;
        }

        return false;
    }

    void ExecuteReaction(TrackedPotion a, TrackedPotion b, PotionsReactions reaction)
    {
        hasInteracted = true;

        reactingA = a;
        reactingB = b;

        imageSpawner.SetPotionReacting(a, true);
        imageSpawner.SetPotionReacting(b, true);

        Vector3 midPoint = (a.GetPosition() + b.GetPosition()) / 2;

        resultInstance = Instantiate(reaction.result.prefab, midPoint, Quaternion.identity);

        ModeloSeleccion info = resultInstance.GetComponent<ModeloSeleccion>();

        if(info != null)
        {
            info.ElementData = reaction.result;
        }

        if(audioSource != null && reaction.reactionSound != null)
        {
            audioSource.PlayOneShot(reaction.reactionSound);
        }

        Debug.Log($"Reaction: {a.data.PotionName} + {b.data.PotionName} = {reaction.result.PotionName}");
    }

    void CancelReaction()
    {
        //Destruir resultado
        if (resultInstance != null)
        {
            Destroy(resultInstance);
            resultInstance = null;
        }

        //Restaurar pociones originales
        if (reactingA != null)
        {
            imageSpawner.SetPotionReacting(reactingA, false);
        }

        if (reactingB != null)
        {
            imageSpawner.SetPotionReacting(reactingB, false);
        }

        reactingA = reactingB = null;

        hasInteracted = false;

        Debug.Log("Reaction Cancelled");
    }

    public void ResetReaction()
    {
        if (hasInteracted)
        {
            CancelReaction();
        }
    }
}
