using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class MenuPrincipal : MonoBehaviour
{
    public GameObject[] trofeos;
    public Button nivel1, nivel2;
    public TMP_InputField inputNombre;
    int NivelCompletado;
    public static string nombreJugador;
    public static int Puntos;
    /*
        Inicializa el menu principal mostrando los trofeos y los niveles accesibles
    */
    void Start()
    {
        CargarDatos();
        NivelCompletado = Unidad.NivelCompletado;

        switch (NivelCompletado)
        {
            case 0:
                nivel2.interactable = false;
                trofeos[0].SetActive(false);
                trofeos[1].SetActive(false);
                break;
            case 1:
                trofeos[0].SetActive(true);
                trofeos[1].SetActive(false);
                nivel2.interactable = true;
                break;
            case 2:
                trofeos[1].SetActive(true);
                break;
            default:
                break;
        }
    }

    public void GuardarDatos(){
        SistemaGuardado.Guardar();
    }

    public void CargarDatos(){
        DatosJugador datos = SistemaGuardado.Cargar();

        MenuPrincipal.Puntos = datos.puntos;
        MenuPrincipal.nombreJugador = datos.nombre;
        Unidad.NivelCompletado = datos.nivelCompletado;
    }
    /*
        Sale de la aplicacion
    */
    public void Salir()
    {
        Application.Quit();
    }
    /*
        Registra el nombre ingresado por el jugador y lo guarda
    */
    public void IngresarNombre(){
        nombreJugador = inputNombre.text;
    }
}
