using UnityEngine;

public class ModeloSeleccion : MonoBehaviour
{
    //public string ModelitoNombrecito;

    //[SerializeField] private ModelData modeldata;
    //public ModelData Data => modeldata;

    [SerializeField] private ElementData data;

    public ElementData ElementData { get => data; set => data = value; }

}
