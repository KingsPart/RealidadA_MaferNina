using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class ARImageSpawner : MonoBehaviour
{
    [Header("AR")]
    [SerializeField] ARTrackedImageManager imageManager;

    [Header("Available Potions")]
    [SerializeField] List<ElementData> potions = new List<ElementData>();

    Dictionary<TrackableId, TrackedPotion> spawnedPotions = new Dictionary<TrackableId, TrackedPotion> ();

    public IEnumerable<TrackedPotion> SpawnedPotions => spawnedPotions.Values;

    private void OnEnable()
    {
        imageManager.trackablesChanged.AddListener(OnImagesChanged);
    }

    private void OnDisable()
    {
        imageManager.trackablesChanged.RemoveListener(OnImagesChanged);
    }

    void OnImagesChanged(ARTrackablesChangedEventArgs<ARTrackedImage> args)
    {
        foreach(ARTrackedImage addedImage in args.added)
        {
            RegisterImage(addedImage);
        }

        foreach (ARTrackedImage updatedImage in args.updated)
        {
            RegisterImage(updatedImage);
        }

        foreach (var removedImage in args.removed)
        {
            RemoveImage(removedImage.Key);
        }
    }

    // Busca el scriptable object que corresponde a la imagen detectada e instancia el modelo
    // y lo guarda en el diccionario
    void RegisterImage(ARTrackedImage image)
    {
        TrackableId id = image.trackableId;

        if(spawnedPotions.TryGetValue(id, out TrackedPotion existing))
        {
            existing.trackedImage = image;
            UpdateVisibility(existing);
            return;
        }

        string imageName = image.referenceImage.name;
        ElementData data = FindPotion(imageName);

        if(data == null || data.prefab == null)
            return;

        GameObject model = Instantiate(data.prefab, image.transform);

        TrackedPotion potion = new TrackedPotion(image, data, model);

        spawnedPotions.Add(id, potion);

        UpdateVisibility(potion);

        Debug.Log("Potion detected: " + data.PotionName);
    }

    ElementData FindPotion(string imageName)
    {
        foreach(ElementData data in potions)
        {
            if (data != null && data.PotionName == imageName)
            {
                return data;
            }
        }

        return null;
    }

    void RemoveImage(TrackableId id)
    {
        if(spawnedPotions.TryGetValue(id, out TrackedPotion potion))
        {
            potion.isRemoved = true;

            if(potion.instance != null)
            {
                Destroy(potion.instance);
            }

            spawnedPotions.Remove(id);
        }
    }

    //Muestra un elemento si esta siendo trackeado y no esta participando en una reaccion
    void UpdateVisibility(TrackedPotion potion)
    {
        bool shouldBeVisible = potion.IsTracking() && !potion.isReacting;

        potion.SetVisible(shouldBeVisible);
    }

    //Llamada oir ARReactionManager | esta es una clase
    public void SetPotionReacting(TrackedPotion potion, bool reacting)
    {
        potion.SetReacting(reacting);
        UpdateVisibility(potion);
    }
}
