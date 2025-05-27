using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Clase que aplica un efecto de desplazamiento a una textura de detalle en un componente de imagen UI.
/// </summary>
[RequireComponent(typeof(Image))]
public class ScrollDetailTexture : MonoBehaviour
{
    /// <summary>
    /// Indica si se debe usar un material unico para el efecto.
    /// </summary>
    public bool uniqueMaterial = false;

    /// <summary>
    /// Velocidad de desplazamiento de la textura por segundo.
    /// </summary>
    public Vector2 scrollPerSecond = Vector2.zero;

    /// <summary>
    /// Matriz de transformacion para el efecto de desplazamiento.
    /// </summary>
    private Matrix4x4 m_Matrix;

    /// <summary>
    /// Copia del material para modificaciones.
    /// </summary>
    private Material mCopy;

    /// <summary>
    /// Material original del componente Image.
    /// </summary>
    private Material mOriginal;

    /// <summary>
    /// Referencia al componente Image.
    /// </summary>
    private Image mSprite;

    /// <summary>
    /// Material activo para el efecto de desplazamiento.
    /// </summary>
    private Material m_Mat;

    /// <summary>
    /// Inicializa el componente Image y configura el material para el efecto de desplazamiento.
    /// </summary>
    void OnEnable()
    {
        mSprite = GetComponent<Image>();
        mOriginal = mSprite.material;

        if (uniqueMaterial && mSprite.material != null)
        {
            mCopy = new Material(mOriginal);
            mCopy.name = "Copy of " + mOriginal.name;
            mCopy.hideFlags = HideFlags.DontSave;
            mSprite.material = mCopy;
        }
    }

    /// <summary>
    /// Restaura el material original y destruye la copia al desactivar el componente.
    /// </summary>
    void OnDisable()
    {
        if (mCopy != null)
        {
            mSprite.material = mOriginal;
            if (Application.isEditor)
                UnityEngine.Object.DestroyImmediate(mCopy);
            else
                UnityEngine.Object.Destroy(mCopy);
            mCopy = null;
        }
        mOriginal = null;
    }

    /// <summary>
    /// Aplica el efecto de desplazamiento a la textura de detalle en cada frame.
    /// </summary>
    void Update()
    {
        Material mat = (mCopy != null) ? mCopy : mOriginal;

        if (mat != null)
        {
            Texture tex = mat.GetTexture("_DetailTex");

            if (tex != null)
            {
                mat.SetTextureOffset("_DetailTex", scrollPerSecond * Time.time);
            }
        }
    }
}
