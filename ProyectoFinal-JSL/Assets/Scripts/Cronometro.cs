using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using TMPro;

public class Cronometro : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI textoCronometro;

    private int tiempoMinutos;
    private int tiempoSegundos;
    private int tiempoMilisegundos;

    private bool cronometroActivo = true;

    void Update()
    {
        ActualizarCronometro();
    }

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

    public void DetenerTiempo()
    {
        cronometroActivo = false;
    }

    public void ReanudarTiempo()
    {
        cronometroActivo = true;
    }
}
