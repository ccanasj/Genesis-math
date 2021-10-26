using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unidad : MonoBehaviour
{
    public Animator animator;
    public string nombre;

    public int vidaMaxima,vidaActual,golpe;
    public static int NivelCompletado, nivel = 1;
    int aux;
    /*
        Al momento de ser invocado inicializa el daño y la vida segun el nivel
    */
    void Awake(){
        golpe = Mathf.RoundToInt((golpe * nivel) * 0.7f);
        vidaMaxima = Mathf.RoundToInt((vidaMaxima * nivel) * 0.8f);
        vidaActual = vidaMaxima;
    }
    /*
        Donde se actualiza la vida segun el daño recibido
    */
    public bool TakeDamage(int golpeRecibido)
    {   
        animator.SetTrigger("golpe");
        vidaActual -= golpeRecibido;
        
        if (vidaActual <= 0)
        {
            animator.SetBool("muerte",true);
            return true;
        }
        else
        {
            return false;
        }

    }
    /*
        Donde se calcula cuanto daño causara segun el tiempo de respuesta del jugador
    */
    public int Golpe(float tiempo){
        if(tiempo >= 45f){
            aux = golpe * 2;
            return aux;
        } else if(tiempo >= 25f && tiempo < 45f){
            return golpe;
        } else {
            aux = Mathf.RoundToInt(golpe / 1.5f);
            return aux;
        }
    }
    /*
        Donde se calcula cuanto daño causara el ataque especial segun el tiempo de respuesta del jugador
    */
    public int GolpeEspecial(float tiempo){
        if(tiempo >= 30){
            aux = Mathf.RoundToInt(golpe * 2.5f);
        } else {
            aux = Mathf.RoundToInt(golpe * 1.5f);
        }
        return aux;
    }
    /*
        Donde se actualiza la vida segun la cantidad de curacion
    */
    public void Curar(){
        vidaActual += Mathf.RoundToInt(vidaMaxima / 2.5f);
        if(vidaActual > vidaMaxima){
            vidaActual = vidaMaxima;
        }
    }
}
