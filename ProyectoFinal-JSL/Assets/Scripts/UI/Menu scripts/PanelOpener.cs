using UnityEngine;

public class PanelOpener : MonoBehaviour
{
    public PanelManager panelManager;          // Asigna aquí tu PanelManager
    public Animator panelToOpenAnimator;       // Asigna aquí el Animator del panel que quieras abrir (ej. panel de volumen)

    // Este método se llamará desde el botón
    public void OpenVolumePanel()
    {
        if (panelManager != null && panelToOpenAnimator != null)
        {
            panelManager.OpenPanel(panelToOpenAnimator);
        }
    }
}
