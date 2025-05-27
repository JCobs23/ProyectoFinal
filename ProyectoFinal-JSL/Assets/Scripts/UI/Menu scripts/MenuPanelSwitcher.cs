using UnityEngine;

/// <summary>
/// Clase que alterna entre los paneles del menu principal y el menu de configuracion.
/// </summary>
public class MenuPanelSwitcher : MonoBehaviour
{
    /// <summary>
    /// Panel del menu principal.
    /// </summary>
    public GameObject mainMenuPanel;

    /// <summary>
    /// Panel de configuracion.
    /// </summary>
    public GameObject settingsPanel;

    /// <summary>
    /// Activa el panel de configuracion y desactiva el panel del menu principal.
    /// </summary>
    public void OpenSettings()
    {
        mainMenuPanel.SetActive(false);
        settingsPanel.SetActive(true);
    }

    /// <summary>
    /// Activa el panel del menu principal y desactiva el panel de configuracion.
    /// </summary>
    public void BackToMainMenu()
    {
        settingsPanel.SetActive(false);
        mainMenuPanel.SetActive(true);
    }
}
