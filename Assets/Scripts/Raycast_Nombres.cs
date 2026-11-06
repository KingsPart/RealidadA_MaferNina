using UnityEngine;
using TMPro;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.EnhancedTouch;
using UnityEngine.UI;

public class Raycast_Nombres : MonoBehaviour
{
    [SerializeField] Camera arCamera;

    [SerializeField] TMP_Text ModelName;
    [SerializeField] TMP_Text ModelDescription;
    [SerializeField] TMP_Text ModelCategory;
    [SerializeField] TMP_Text ModelLevel;
    [SerializeField] Image iconImage;
    private void OnEnable()
    {
        EnhancedTouchSupport.Enable();
    }

    private void OnDisable()
    {
        EnhancedTouchSupport.Disable();
    }

    //void Start()
    //{
    //    ModelName.text = "Model name: ";
    //}

    void Update()
    {
        var activeTouches = UnityEngine.InputSystem.EnhancedTouch.Touch.activeTouches;

        if (activeTouches.Count == 0)
            return;

        var touch = activeTouches[0];

        if (touch.phase != UnityEngine.InputSystem.TouchPhase.Began)
            return;

        // Limpiar información anterior
        ClearInfo();

        // Crear el rayo desde la cámara hacia el punto tocado
        Ray ray = arCamera.ScreenPointToRay(touch.screenPosition);

        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            // Buscar el componente del modelo seleccionado
            ModeloSeleccion selectedModel = hit.collider.GetComponentInParent<ModeloSeleccion>();

            if (selectedModel != null && selectedModel.Data != null)
            {
                ShowInfo(selectedModel.Data);
            }
        }
    }

    private void ShowInfo(ModelData data)
    {
        //ClearInfo();

        ModelName.text = data.name;
        ModelDescription.text = data.ModelDescription;
        ModelCategory.text = data.category;
        ModelLevel.text = "Nivel: " + data.level;
        iconImage.sprite = data.icon;

        Debug.Log("Modelo seleccionado: " + data);
    }

    private void ClearInfo()
    {
        ModelName.text = "[Model name]";
        ModelDescription.text = "[Description]";
        ModelCategory.text = "[Category]";
        ModelLevel.text = "[Lvl]";
        iconImage.sprite = null;
    }
}
