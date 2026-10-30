using UnityEngine;
using TMPro;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.EnhancedTouch;

public class Raycast_Nombres : MonoBehaviour
{
    [SerializeField] Camera arCamera;
    [SerializeField] TMP_Text infoText;

    private void OnEnable()
    {
        EnhancedTouchSupport.Enable();
    }

    private void OnDisable()
    {
        EnhancedTouchSupport.Disable();
    }

    void Start()
    {
        infoText.text = "Toca un modelo ya ya";
    }

    void Update()
    {
        var activeTouches = UnityEngine.InputSystem.EnhancedTouch.Touch.activeTouches;

        if (activeTouches.Count == 0)
            return;

        var touch = activeTouches[0];

        if (touch.phase != UnityEngine.InputSystem.TouchPhase.Began)
            return;

        // Crear el rayo desde la cámara hacia el punto tocado
        Ray ray = arCamera.ScreenPointToRay(touch.screenPosition);

        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            // Buscar el componente del modelo seleccionado
            ModeloSeleccion selectedModel =
                hit.collider.GetComponentInParent<ModeloSeleccion>();

            if (selectedModel != null)
            {
                infoText.text = "Modelo seleccionado: " + selectedModel.ModelitoNombrecito;

                Debug.Log("Modelo seleccionado: " + selectedModel.ModelitoNombrecito);
            }
        }
    }
}
