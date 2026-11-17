using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PotionInfoCard : MonoBehaviour
{
    [Header("UI Elements")]
    [SerializeField] private TMP_Text potionName;
    [SerializeField] private TMP_Text potionDescription;
    [SerializeField] private TMP_Text potionLevel;
    [SerializeField] private Image potionIcon;

    public void SetInfo(ElementData data)
    {
        if (data == null)
            return;

        potionName.text = data.PotionName;
        potionDescription.text = data.description;
        potionLevel.text = "~ " + data.level;
        potionIcon.sprite = data.icon;
    }

    private void Start()
    {
        ModeloSeleccion modelo = GetComponentInParent<ModeloSeleccion>();

        if (modelo != null)
        {
            SetInfo(modelo.ElementData);
        }
    }
}
