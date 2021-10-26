using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HUDBatalla : MonoBehaviour
{
    public TextMeshProUGUI textoNombre;
    public TextMeshProUGUI textoVida;
    public TextMeshProUGUI textoNivel;
    public Slider barra;
    public GameObject BotonesSeleccion;
    public GameObject BotonesAtaque;
    /*
        Se inicializa la interfaz que muestra los datos de la entidad
    */
    public void setHUD(Unidad unidad){
        textoNombre.text = unidad.nombre;
        barra.maxValue = unidad.vidaMaxima;
        barra.value = unidad.vidaActual;
        textoNivel.text = "LV " + Unidad.nivel;
        textoVida.text = unidad.vidaActual + " / " + unidad.vidaMaxima;
    }
    /*
        Se actualiza en la interfaz los datos durante le combate de la entidad
    */
    public void setHP(int vida, Unidad unidad ){
        unidad.vidaActual = vida;
        if(unidad.vidaActual <= 0){
            unidad.vidaActual = 0;
        }
        barra.value = unidad.vidaActual;
        textoVida.text = unidad.vidaActual + " / " + unidad.vidaMaxima;
    }
    /*
        Donde se desactiva los botones de seleccion de ataques o los botones principales
    */
    public void DesactivarHUD(int i){
        switch (i)
        {
            case 1:
            BotonesSeleccion.SetActive(false);
            break;
            case 2:
            BotonesAtaque.SetActive(false);
            break;

            default:
            break;
        }
        
    }
    /*
        Donde se activan los botones de seleccion de ataques o los botones principales
    */
    public void ActivarHUD(int i){
        switch (i)
        {
            case 1:
            BotonesSeleccion.SetActive(true);
            break;
            case 2:
            BotonesAtaque.SetActive(true);
            break;
            
            default:
            break;
        }
        
    }
}
