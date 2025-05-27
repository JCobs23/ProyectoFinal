using UnityEngine;

/// <summary>
/// Clase que aplica un efecto de inclinacion a un objeto UI basado en la posicion del raton.
/// </summary>
public class TiltWindow : MonoBehaviour
{
    /// <summary>
    /// Rango de inclinacion en los ejes X e Y.
    /// </summary>
    public Vector2 range = new Vector2(5f, 3f);

    /// <summary>
    /// Transform del objeto.
    /// </summary>
    private Transform mTrans;

    /// <summary>
    /// Rotacion inicial del objeto.
    /// </summary>
    private Quaternion mStart;

    /// <summary>
    /// Rotacion actual basada en la posicion del raton.
    /// </summary>
    private Vector2 mRot = Vector2.zero;

    /// <summary>
    /// Inicializa las referencias al Transform y la rotacion inicial.
    /// </summary>
    void Start()
    {
        mTrans = transform;
        mStart = mTrans.localRotation;
    }

    /// <summary>
    /// Actualiza la rotacion del objeto en funcion de la posicion del raton.
    /// </summary>
    void Update()
    {
        Vector3 pos = Input.mousePosition;

        float halfWidth = Screen.width * 0.5f;
        float halfHeight = Screen.height * 0.5f;
        float x = Mathf.Clamp((pos.x - halfWidth) / halfWidth, -1f, 1f);
        float y = Mathf.Clamp((pos.y - halfHeight) / halfHeight, -1f, 1f);
        mRot = Vector2.Lerp(mRot, new Vector2(x, y), Time.deltaTime * 5f);

        mTrans.localRotation = mStart * Quaternion.Euler(-mRot.y * range.y, mRot.x * range.x, 0f);
    }
}
