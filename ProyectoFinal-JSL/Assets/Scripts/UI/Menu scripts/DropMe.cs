using System.Reflection;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

/// <summary>
/// Clase que maneja la funcionalidad de arrastrar y soltar en una interfaz de usuario, permitiendo cambiar sprites al soltar un objeto.
/// </summary>
public class DropMe : MonoBehaviour, IDropHandler, IPointerEnterHandler, IPointerExitHandler
{
    /// <summary>
    /// Imagen contenedora que cambia de color durante las interacciones.
    /// </summary>
    public Image containerImage;

    /// <summary>
    /// Imagen que recibe el sprite soltado.
    /// </summary>
    public Image receivingImage;

    /// <summary>
    /// Color normal de la imagen contenedora.
    /// </summary>
    private Color normalColor;

    /// <summary>
    /// Color resaltado aplicado cuando un objeto arrastrable pasa sobre la imagen.
    /// </summary>
    public Color highlightColor = Color.yellow;

    /// <summary>
    /// Inicializa el color normal de la imagen contenedora al activar el componente.
    /// </summary>
    public void OnEnable()
    {
        if (containerImage != null)
            normalColor = containerImage.color;
    }

    /// <summary>
    /// Maneja el evento cuando un objeto es soltado en el area de destino.
    /// </summary>
    /// <param name="data">Datos del evento de arrastrar y soltar.</param>
    public void OnDrop(PointerEventData data)
    {
        containerImage.color = normalColor;

        if (receivingImage == null)
            return;

        Sprite dropSprite = GetDropSprite(data);
        if (dropSprite != null)
            receivingImage.overrideSprite = dropSprite;
    }

    /// <summary>
    /// Maneja el evento cuando el puntero entra en el area de destino.
    /// </summary>
    /// <param name="data">Datos del evento del puntero.</param>
    public void OnPointerEnter(PointerEventData data)
    {
        if (containerImage == null)
            return;

        Sprite dropSprite = GetDropSprite(data);
        if (dropSprite != null)
            containerImage.color = highlightColor;
    }

    /// <summary>
    /// Maneja el evento cuando el puntero sale del area de destino.
    /// </summary>
    /// <param name="data">Datos del evento del puntero.</param>
    public void OnPointerExit(PointerEventData data)
    {
        if (containerImage == null)
            return;

        containerImage.color = normalColor;
    }

    /// <summary>
    /// Obtiene el sprite del objeto que se esta arrastrando.
    /// </summary>
    /// <param name="data">Datos del evento de arrastrar y soltar.</param>
    /// <returns>El sprite del objeto arrastrado, o null si no es valido.</returns>
    private Sprite GetDropSprite(PointerEventData data)
    {
        var originalObj = data.pointerDrag;
        if (originalObj == null)
            return null;

        var dragMe = originalObj.GetComponent<DragMe>();
        if (dragMe == null)
            return null;

        var srcImage = originalObj.GetComponent<Image>();
        if (srcImage == null)
            return null;

        return srcImage.sprite;
    }
}
