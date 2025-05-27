using System.Reflection.Emit;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Clase que actualiza un componente de texto para mostrar el valor de un slider como porcentaje.
/// </summary>
[RequireComponent(typeof(Text))]
public class ShowSliderValue : MonoBehaviour
{
    /// <summary>
    /// Actualiza el texto para mostrar el valor del slider como un porcentaje redondeado.
    /// </summary>
    /// <param name="value">Valor del slider (entre 0 y 1).</param>
    public void UpdateLabel(float value)
    {
        Text lbl = GetComponent<Text>();
        if (lbl != null)
            lbl.text = Mathf.RoundToInt(value * 100) + "%";
    }
}
