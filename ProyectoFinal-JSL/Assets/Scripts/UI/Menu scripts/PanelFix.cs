using UnityEngine;

/// <summary>
/// Clase que alterna la visibilidad de los paneles del menu principal y de configuracion.
/// </summary>
public class PanelFix : MonoBehaviour
{
    /// <summary>
    /// Panel del menu principal.
    /// </summary>
    [Header("Paneles")]
    public GameObject mainMenuPanel;

    /// <summary>
    /// Panel de configuracion.
    /// </summary>
    public GameObject settingsPanel;

    /// <summary>
    /// Activa el panel de configuracion y desactiva el menu principal.
    /// </summary>
    public void OnSettingsButtonPressed()
    {
        if (mainMenuPanel != null) mainMenuPanel.SetActive(false);
        if (settingsPanel != null) settingsPanel.SetActive(true);
    }

    /// <summary>
    /// Activa el menu principal y desactiva el panel de configuracion.
    /// </summary>
    public void OnBackButtonPressed()
    {
        if (settingsPanel != null) settingsPanel.SetActive(false);
        if (mainMenuPanel != null) mainMenuPanel.SetActive(true);
    }
}
