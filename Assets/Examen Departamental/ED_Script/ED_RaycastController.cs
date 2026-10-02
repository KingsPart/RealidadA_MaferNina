using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.EnhancedTouch;
using System.Collections.Generic;
using TMPro;

public class ED_RaycastController : MonoBehaviour
{
    [SerializeField] ARRaycastManager raycastManager;
    [SerializeField] GameObject horizontalObject;
    [SerializeField] GameObject verticalObject;
    [SerializeField] TMP_Text debugText;

    List<ARRaycastHit> hits = new List<ARRaycastHit>();

    // Modelo que el usuario ha seleccionado
    GameObject objetoSeleccionado;

    private void OnEnable()
    {
        EnhancedTouchSupport.Enable();
    }

    private void OnDisable()
    {
        EnhancedTouchSupport.Disable();
    }

    // BOTÓN: seleccionar modelo horizontal
    public void SeleccionarHorizontal()
    {
        objetoSeleccionado = horizontalObject;

        debugText.text = "Modelo horizontal seleccionado";
        Debug.Log("MODELO HORIZONTAL SELECCIONADO");
    }

    // BOTÓN: seleccionar modelo vertical
    public void SeleccionarVertical()
    {
        objetoSeleccionado = verticalObject;

        debugText.text = "Modelo vertical seleccionado";
        Debug.Log("MODELO VERTICAL SELECCIONADO");
    }

    void Update()
    {
        var activeTouches = UnityEngine.InputSystem.EnhancedTouch.Touch.activeTouches;

        if (activeTouches.Count == 0)
            return;

        var touch = activeTouches[0];

        if (touch.phase != UnityEngine.InputSystem.TouchPhase.Began)
            return;

        Debug.Log("TOUCH DETECTADO");

        if (raycastManager.Raycast(touch.screenPosition, hits, TrackableType.PlaneWithinPolygon))
        {
            Debug.Log("HIT ENCONTRADO");

            Pose hitPose = hits[0].pose;

            ARPlane plane = hits[0].trackable.GetComponent<ARPlane>();

            if (plane == null)
                return;

            // PLANO HORIZONTAL
            if (plane.alignment == PlaneAlignment.HorizontalUp)
            {
                Debug.Log("PLANO HORIZONTAL");
                debugText.text = "Superficie horizontal detectada!";

                // Solo colocar si seleccionamos el modelo horizontal
                if (objetoSeleccionado == horizontalObject)
                {
                    Instantiate(horizontalObject, hitPose.position, hitPose.rotation);

                    debugText.text = "Objeto colocado en plano horizontal";
                    Debug.Log("OBJETO COLOCADO EN PLANO HORIZONTAL");
                }
            }

            // PLANO VERTICAL
            else if (plane.alignment == PlaneAlignment.Vertical)
            {
                Debug.Log("PLANO VERTICAL");
                debugText.text = "Superficie vertical detectada!";

                // Solo colocar si seleccionamos el modelo vertical
                if (objetoSeleccionado == verticalObject)
                {
                    Instantiate(verticalObject, hitPose.position, hitPose.rotation);

                    debugText.text = "Objeto colocado en plano vertical";
                    Debug.Log("OBJETO COLOCADO EN PLANO VERTICAL");
                }
            }
        }
        else
        {
            Debug.Log("NO SE ENCONTRO PLANO");
        }
    }
}
