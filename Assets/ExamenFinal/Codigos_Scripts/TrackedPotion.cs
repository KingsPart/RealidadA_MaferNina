using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
public class TrackedPotion
{
    public ARTrackedImage trackedImage;
    public ElementData data;
    public GameObject instance;

    public bool isReacting = false;
    public bool isRemoved = false;

    public TrackedPotion(ARTrackedImage image, ElementData elementData, GameObject model)
    {
        trackedImage = image;
        data = elementData;
        instance = model;
    }

    public bool IsTracking()
    {
        return !isRemoved && trackedImage != null && trackedImage.trackingState == TrackingState.Tracking;
    }

    public Vector3 GetPosition()
    {
        return trackedImage.transform.position;
    }

    public void SetVisible(bool visible)
    {
        if (instance != null)
            instance.SetActive(visible);
    }

    public void SetReacting(bool reacting) 
    { 
        isReacting = reacting;
    }
}
