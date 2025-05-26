using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HideTextsOnButtonClick : MonoBehaviour
{
    [Header("Textos a ocultar / mostrar")]
    public TextMeshProUGUI[] textsToToggle;

    [Header("Botones que ocultan los textos")]
    public Button[] hideButtons;

    [Header("Botón que hace reaparecer los textos")]
    public Button showButton;

    void Start()
    {
        foreach (Button btn in hideButtons)
        {
            if (btn != null)
                btn.onClick.AddListener(HideTexts);
        }

        if (showButton != null)
            showButton.onClick.AddListener(ShowTexts);
    }

    void HideTexts()
    {
        foreach (var txt in textsToToggle)
        {
            if (txt != null)
                txt.gameObject.SetActive(false);
        }
    }

    void ShowTexts()
    {
        foreach (var txt in textsToToggle)
        {
            if (txt != null)
                txt.gameObject.SetActive(true);
        }
    }
}
