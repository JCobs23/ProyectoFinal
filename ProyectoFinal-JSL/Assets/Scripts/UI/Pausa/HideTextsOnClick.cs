using UnityEngine;
using UnityEngine.UI;
using TMPro;

/// <summary>
/// Clase que permite ocultar y mostrar textos al interactuar con botones especificados.
/// </summary>
public class HideTextsOnButtonClick : MonoBehaviour
{
    /// <summary>
    /// Arreglo de textos que se ocultaran o mostraran.
    /// </summary>
    [Header("Textos a ocultar / mostrar")]
    public TextMeshProUGUI[] textsToToggle;

    /// <summary>
    /// Arreglo de botones que ocultan los textos.
    /// </summary>
    [Header("Botones que ocultan los textos")]
    public Button[] hideButtons;

    /// <summary>
    /// Boton que hace reaparecer los textos.
    /// </summary>
    [Header("Boton que hace reaparecer los textos")]
    public Button showButton;

    /// <summary>
    /// Configura los eventos de los botones para ocultar y mostrar textos al iniciar.
    /// </summary>
    void Start()
    {
        foreach (Button btn in hideButtons)
        {
            if (btn != null)
                btn.onClick.AddListener(HideTexts);
        }

        if (showButton != null)
            showButton.onClick.AddListener(ShowTexts);
    }

    /// <summary>
    /// Oculta todos los textos especificados.
    /// </summary>
    void HideTexts()
    {
        foreach (var txt in textsToToggle)
        {
            if (txt != null)
                txt.gameObject.SetActive(false);
        }
    }

    /// <summary>
    /// Muestra todos los textos especificados.
    /// </summary>
    void ShowTexts()
    {
        foreach (var txt in textsToToggle)
        {
            if (txt != null)
                txt.gameObject.SetActive(true);
        }
    }
}
