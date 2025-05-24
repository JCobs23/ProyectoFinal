using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using TMPro;

/// <summary>
/// Clase que gestiona un cronometro en el juego, mostrando el tiempo acumulado.
/// </summary>
public class Cronometro : MonoBehaviour
{
    /// <summary>
    /// Referencia al componente TextMeshProUGUI para mostrar el tiempo.
    /// </summary>
    [SerializeField] private TextMeshProUGUI textoCronometro;

    /// <summary>
    /// Minutos acumulados en el cronometro.
    /// </summary>
    private int tiempoMinutos;

    /// <summary>
    /// Segundos acumulados en el cronometro.
    /// </summary>
    private int tiempoSegundos;

    /// <summary>
    /// Milisegundos acumulados en el cronometro.
    /// </summary>
    private int tiempoMilisegundos;

    /// <summary>
    /// Indica si el cronometro esta activo.
    /// </summary>
    private bool cronometroActivo = true;

    /// <summary>
    /// Actualiza el cronometro cada frame si esta activo.
    /// </summary>
    void Update()
    {
        ActualizarCronometro();
    }

    /// <summary>
    /// Calcula y actualiza el tiempo acumulado, mostrando minutos, segundos y milisegundos en el texto.
    /// </summary>
    void ActualizarCronometro()
    {
        if (cronometroActivo == true)
        {
            GameManager.Instance.TiempoAcumulado += Time.deltaTime;

            float tiempo = GameManager.Instance.TiempoAcumulado;

            tiempoMinutos = Mathf.FloorToInt(tiempo / 60);
            tiempoSegundos = Mathf.FloorToInt(tiempo % 60);
            tiempoMilisegundos = Mathf.FloorToInt((tiempo - (tiempoSegundos + tiempoMinutos * 60)) * 100);

            if (textoCronometro != null)
            {
                textoCronometro.text = string.Format("{0:00}:{1:00}:{2:00}", tiempoMinutos, tiempoSegundos, tiempoMilisegundos);
            }
        }
    }

    /// <summary>
    /// Detiene el cronometro, pausando la actualizacion del tiempo.
    /// </summary>
    public void DetenerTiempo()
    {
        cronometroActivo = false;
    }

    /// <summary>
    /// Reanuda el cronometro, permitiendo que el tiempo se actualice nuevamente.
    /// </summary>
    public void ReanudarTiempo()
    {
        cronometroActivo = true;
    }
}