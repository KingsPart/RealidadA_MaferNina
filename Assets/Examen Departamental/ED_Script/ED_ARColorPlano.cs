using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class ED_ARColorPlano : MonoBehaviour
{
    public Material materialHorizontal;
    public Material materialVertical;

    private ARPlane plano;
    private MeshRenderer meshRenderer;

    void Start()
    {
        plano = GetComponent<ARPlane>();
        meshRenderer = GetComponent<MeshRenderer>();

        if (plano.alignment == PlaneAlignment.HorizontalUp)
        {
            meshRenderer.material = materialHorizontal;
        }
        else if (plano.alignment == PlaneAlignment.Vertical)
        {
            meshRenderer.material = materialVertical;
        }
    }
}
