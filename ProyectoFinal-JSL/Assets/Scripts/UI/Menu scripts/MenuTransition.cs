using UnityEngine;
using UnityEngine.UI;
using System.Collections;

/// <summary>
/// Clase que maneja transiciones animadas entre los paneles del menu principal y el menu de configuracion.
/// </summary>
public class MenuTransition : MonoBehaviour
{
    /// <summary>
    /// Panel del menu principal.
    /// </summary>
    public RectTransform mainMenuPanel;

    /// <summary>
    /// Panel de configuracion.
    /// </summary>
    public RectTransform settingsPanel;

    /// <summary>
    /// Duracion de la transicion entre paneles en segundos.
    /// </summary>
    public float transitionDuration = 0.5f;

    /// <summary>
    /// Posicion fuera de pantalla para los paneles durante la transicion.
    /// </summary>
    public Vector2 offScreenPos = new Vector2(-1920, 0);

    /// <summary>
    /// Posicion central de los paneles en la pantalla.
    /// </summary>
    private Vector2 centerPos = Vector2.zero;

    /// <summary>
    /// Inicializa los paneles, asegurando que el menu principal este visible y el de configuracion oculto.
    /// </summary>
    private void Start()
    {
        // Asegura que el main menu este visible y settings oculto al iniciar
        mainMenuPanel.gameObject.SetActive(true);
        mainMenuPanel.anchoredPosition = centerPos;

        settingsPanel.gameObject.SetActive(false);
        settingsPanel.anchoredPosition = offScreenPos;
    }

    /// <summary>
    /// Inicia la transicion para mostrar el panel de configuracion y ocultar el menu principal.
    /// </summary>
    public void OpenSettings()
    {
        StopAllCoroutines();
        StartCoroutine(SlideOut(mainMenuPanel));
        StartCoroutine(SlideIn(settingsPanel));
    }

    /// <summary>
    /// Inicia la transicion para mostrar el menu principal y ocultar el panel de configuracion.
    /// </summary>
    public void BackToMain()
    {
        StopAllCoroutines();
        StartCoroutine(SlideOut(settingsPanel));
        StartCoroutine(SlideIn(mainMenuPanel));
    }

    /// <summary>
    /// Desplaza un panel fuera de la pantalla.
    /// </summary>
    /// <param name="panel">El panel a desplazar.</param>
    /// <returns>Un IEnumerator para controlar la corrutina.</returns>
    IEnumerator SlideOut(RectTransform panel)
    {
        panel.gameObject.SetActive(true); // Asegura que este activo antes de moverlo
        Vector2 startPos = panel.anchoredPosition;
        Vector2 targetPos = offScreenPos;

        float t = 0;
        while (t < 1)
        {
            t += Time.unscaledDeltaTime / transitionDuration;
            panel.anchoredPosition = Vector2.Lerp(startPos, targetPos, t);
            yield return null;
        }

        panel.anchoredPosition = targetPos;
        panel.gameObject.SetActive(false); // Ocultarlo despues del movimiento
    }

    /// <summary>
    /// Desplaza un panel hacia el centro de la pantalla.
    /// </summary>
    /// <param name="panel">El panel a desplazar.</param>
    /// <returns>Un IEnumerator para controlar la corrutina.</returns>
    IEnumerator SlideIn(RectTransform panel)
    {
        panel.gameObject.SetActive(true);
        Vector2 startPos = offScreenPos;
        Vector2 targetPos = centerPos;

        panel.anchoredPosition = startPos;

        float t = 0;
        while (t < 1)
        {
            t += Time.unscaledDeltaTime / transitionDuration;
            panel.anchoredPosition = Vector2.Lerp(startPos, targetPos, t);
            yield return null;
        }

        panel.anchoredPosition = targetPos;
    }
}
