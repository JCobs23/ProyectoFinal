using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// Clase que gestiona la apertura y cierre de paneles con animaciones, manteniendo el foco en elementos seleccionables.
/// </summary>
public class PanelManager : MonoBehaviour
{
    /// <summary>
    /// Panel inicial que se abre al activar el componente.
    /// </summary>
    public Animator initiallyOpen;

    /// <summary>
    /// Identificador del parametro de animacion para abrir paneles.
    /// </summary>
    private int m_OpenParameterId;

    /// <summary>
    /// Panel actualmente abierto.
    /// </summary>
    private Animator m_Open;

    /// <summary>
    /// Ultimo objeto seleccionado antes de abrir un nuevo panel.
    /// </summary>
    private GameObject m_PreviouslySelected;

    /// <summary>
    /// Nombre de la transicion de apertura en el Animator.
    /// </summary>
    private const string k_OpenTransitionName = "Open";

    /// <summary>
    /// Nombre del estado cerrado en el Animator.
    /// </summary>
    private const string k_ClosedStateName = "Closed";

    /// <summary>
    /// Inicializa el parametro de animacion y abre el panel inicial.
    /// </summary>
    public void OnEnable()
    {
        m_OpenParameterId = Animator.StringToHash(k_OpenTransitionName);

        if (initiallyOpen == null)
            return;

        OpenPanel(initiallyOpen);
    }

    /// <summary>
    /// Abre un panel con animacion y establece el foco en el primer elemento seleccionable.
    /// </summary>
    /// <param name="anim">El Animator del panel a abrir.</param>
    public void OpenPanel(Animator anim)
    {
        if (m_Open == anim)
            return;

        anim.gameObject.SetActive(true);
        var newPreviouslySelected = EventSystem.current.currentSelectedGameObject;

        anim.transform.SetAsLastSibling();

        CloseCurrent();

        m_PreviouslySelected = newPreviouslySelected;

        m_Open = anim;
        m_Open.SetBool(m_OpenParameterId, true);

        GameObject go = FindFirstEnabledSelectable(anim.gameObject);

        SetSelected(go);
    }

    /// <summary>
    /// Busca el primer elemento seleccionable activo e interactuable en un objeto.
    /// </summary>
    /// <param name="gameObject">El objeto que contiene los elementos seleccionables.</param>
    /// <returns>El primer elemento seleccionable encontrado, o null si no hay ninguno.</returns>
    static GameObject FindFirstEnabledSelectable(GameObject gameObject)
    {
        GameObject go = null;
        var selectables = gameObject.GetComponentsInChildren<Selectable>(true);
        foreach (var selectable in selectables)
        {
            if (selectable.IsActive() && selectable.IsInteractable())
            {
                go = selectable.gameObject;
                break;
            }
        }
        return go;
    }

    /// <summary>
    /// Cierra el panel actualmente abierto con animacion.
    /// </summary>
    public void CloseCurrent()
    {
        if (m_Open == null)
            return;

        m_Open.SetBool(m_OpenParameterId, false);
        SetSelected(m_PreviouslySelected);
        StartCoroutine(DisablePanelDeleyed(m_Open));
        m_Open = null;
    }

    /// <summary>
    /// Desactiva un panel despues de que su animacion de cierre haya terminado.
    /// </summary>
    /// <param name="anim">El Animator del panel a cerrar.</param>
    /// <returns>Un IEnumerator para controlar la corrutina.</returns>
    IEnumerator DisablePanelDeleyed(Animator anim)
    {
        bool closedStateReached = false;
        bool wantToClose = true;
        while (!closedStateReached && wantToClose)
        {
            if (!anim.IsInTransition(0))
                closedStateReached = anim.GetCurrentAnimatorStateInfo(0).IsName(k_ClosedStateName);

            wantToClose = !anim.GetBool(m_OpenParameterId);

            yield return new WaitForEndOfFrame();
        }

        if (wantToClose)
            anim.gameObject.SetActive(false);
    }

    /// <summary>
    /// Establece el objeto seleccionado en el sistema de eventos.
    /// </summary>
    /// <param name="go">El objeto a seleccionar.</param>
    private void SetSelected(GameObject go)
    {
        EventSystem.current.SetSelectedGameObject(go);
    }
}
