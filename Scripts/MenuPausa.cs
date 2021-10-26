using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class MenuPausa : MonoBehaviour
{
    public static bool JegoPausado = false;
    
    public GameObject MenuPausaUI;

    /*
        Desactiva el menu de pausa y reanuda la partida
    */
    public void Reanudar(){

        MenuPausaUI.SetActive(false);
        Time.timeScale = 1f;
        JegoPausado = false;
    }
    /*
        Activa el menu de pausa y detine la partida
    */
    public void Pausa(){

        MenuPausaUI.SetActive(true);
        Time.timeScale = 0f;
        JegoPausado = true;
    }
}
