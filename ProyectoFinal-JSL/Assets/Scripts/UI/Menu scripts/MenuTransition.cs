using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MenuTransition : MonoBehaviour
{
    public RectTransform mainMenuPanel;
    public RectTransform settingsPanel;
    public float transitionDuration = 0.5f;
    public Vector2 offScreenPos = new Vector2(-1920, 0); // Mover hacia la izquierda fuera de pantalla

    private Vector2 centerPos = Vector2.zero;

    private void Start()
    {
        // Asegura que el main menu esté visible y settings oculto al iniciar
        mainMenuPanel.gameObject.SetActive(true);
        mainMenuPanel.anchoredPosition = centerPos;

        settingsPanel.gameObject.SetActive(false);
        settingsPanel.anchoredPosition = offScreenPos;
    }

    public void OpenSettings()
    {
        StopAllCoroutines();
        StartCoroutine(SlideOut(mainMenuPanel));
        StartCoroutine(SlideIn(settingsPanel));
    }

    public void BackToMain()
    {
        StopAllCoroutines();
        StartCoroutine(SlideOut(settingsPanel));
        StartCoroutine(SlideIn(mainMenuPanel));
    }

    IEnumerator SlideOut(RectTransform panel)
    {
        panel.gameObject.SetActive(true); // Asegura que esté activo antes de moverlo
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
        panel.gameObject.SetActive(false); // Ocultarlo después del movimiento
    }

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
