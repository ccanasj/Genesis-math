using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class DatosJugador 
{
    public bool[] muertos,cerrados;
    public float[] position;
    public int nivelCompletado,puntos;
    public string nombre;

    public DatosJugador(){
        muertos = Mapa.Muertos;
        cerrados = Mapa.Cerrados;

        position = new float[2];
        position[0] = Mapa.posicion.x;
        position[1] = Mapa.posicion.y;

        nivelCompletado = Unidad.NivelCompletado;
        puntos = MenuPrincipal.Puntos;
        
        nombre = MenuPrincipal.nombreJugador;
    }

}
