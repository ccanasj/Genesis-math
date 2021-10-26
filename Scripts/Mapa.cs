using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mapa : MonoBehaviour
{  
    public  GameObject [] CofresMapa;
    public static bool[] Cerrados = {true,true,true,true,true};
    public  GameObject [] EnemigosMapa;
    public static bool[] Muertos = {false,false,false,false,false};
    int contador = 0;
    public static Vector2 posicion = new Vector2(1.2f,-1.84f);
    public Transform personaje;
    public static int IDgeneral;
    public GameObject MenuCompletado,MenuPausa;


    void Awake(){
        CargarDatos();
        personaje.position = posicion;
        for (int i = 0; i < EnemigosMapa.Length; i++)
        {
            if(Muertos[i] == true){
                Destroy(EnemigosMapa[i]);
                contador++;
            }
        }
        if(contador >= EnemigosMapa.Length){
            MenuCompletado.SetActive(true);
            Personaje.PuedeMoverse = false;
            MenuPausa.SetActive(false);
            Unidad.NivelCompletado++;
            SistemaGuardado.Guardar();
        }

        for (int i = 0; i < Cerrados.Length; i++)
        {
            Objecto Cofre = CofresMapa[i].GetComponent<Objecto>();
            Cofre.textoPuntos.text = "X " + MenuPrincipal.Puntos;
            Cofre.cerrado = Cerrados[i];
            if(Cerrados[i] == false){
                CofresMapa[i].GetComponent<SpriteRenderer>().sprite = Cofre.spriteAbierto;
            }
        }
    }

    public void GuardarDatos(){
        SistemaGuardado.Guardar();
    }

    public void CargarDatos(){
        DatosJugador datos = SistemaGuardado.Cargar();
        Muertos = datos.muertos;
        Cerrados = datos.cerrados;

        posicion.x = datos.position[0];
        posicion.y = datos.position[1];
    }
    void Update()
    {
        
        posicion = personaje.position;
        for (int i = 0; i < CofresMapa.Length; i++)
        {
            Cerrados[i] = CofresMapa[i].GetComponent<Objecto>().cerrado;
        }
    }
}
