using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using TMPro;

public class ED_ARColorPlano : MonoBehaviour
{
    public Material materialHorizontal;
    public Material materialVertical;
    public TextMeshPro EtiquetaDimensiones;

    private ARPlane plano;
    private MeshRenderer meshRenderer;

    void Awake()
    {
        plano = GetComponent<ARPlane>();
        meshRenderer = GetComponent<MeshRenderer>();
    }

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
        ActualizarDimensiones();
    }

    void Update()
    {
        ActualizarDimensiones();
    }

    void ActualizarDimensiones()
    {
        float ancho = plano.size.x;
        float largo = plano.size.y;

        EtiquetaDimensiones.text = ancho.ToString("F2") + " m x " + largo.ToString("F2") + " m";

        EtiquetaDimensiones.transform.localPosition = Vector3.zero;
    }
}
