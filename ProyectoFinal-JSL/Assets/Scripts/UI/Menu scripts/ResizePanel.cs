using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

/// <summary>
/// Clase que permite redimensionar un panel de UI mediante arrastre.
/// </summary>
public class ResizePanel : MonoBehaviour, IPointerDownHandler, IDragHandler
{
    /// <summary>
    /// Tamano minimo permitido para el panel.
    /// </summary>
    public Vector2 minSize = new Vector2(100, 100);

    /// <summary>
    /// Tamano maximo permitido para el panel.
    /// </summary>
    public Vector2 maxSize = new Vector2(400, 400);

    /// <summary>
    /// Referencia al RectTransform del panel padre.
    /// </summary>
    private RectTransform panelRectTransform;

    /// <summary>
    /// Posicion inicial del puntero al comenzar el arrastre.
    /// </summary>
    private Vector2 originalLocalPointerPosition;

    /// <summary>
    /// Tamano inicial del panel al comenzar el arrastre.
    /// </summary>
    private Vector2 originalSizeDelta;

    /// <summary>
    /// Inicializa la referencia al RectTransform del panel padre.
    /// </summary>
    void Awake()
    {
        panelRectTransform = transform.parent.GetComponent<RectTransform>();
    }

    /// <summary>
    /// Maneja el evento de presionar el puntero en el panel.
    /// </summary>
    /// <param name="data">Datos del evento del puntero.</param>
    public void OnPointerDown(PointerEventData data)
    {
        originalSizeDelta = panelRectTransform.sizeDelta;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(panelRectTransform, data.position, data.pressEventCamera, out originalLocalPointerPosition);
    }

    /// <summary>
    /// Maneja el evento de arrastre para redimensionar el panel.
    /// </summary>
    /// <param name="data">Datos del evento de arrastre.</param>
    public void OnDrag(PointerEventData data)
    {
        if (panelRectTransform == null)
            return;

        Vector2 localPointerPosition;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(panelRectTransform, data.position, data.pressEventCamera, out localPointerPosition);
        Vector3 offsetToOriginal = localPointerPosition - originalLocalPointerPosition;

        Vector2 sizeDelta = originalSizeDelta + new Vector2(offsetToOriginal.x, -offsetToOriginal.y);
        sizeDelta = new Vector2(
            Mathf.Clamp(sizeDelta.x, minSize.x, maxSize.x),
            Mathf.Clamp(sizeDelta.y, minSize.y, maxSize.y)
        );

        panelRectTransform.sizeDelta = sizeDelta;
    }
}
