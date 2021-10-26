using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Transicion : MonoBehaviour
{
    public Animator transicion;
    public void CargarSiguienteEscena()
    {
        StartCoroutine(CargarNivel(SceneManager.GetActiveScene().buildIndex + 1));
    }

    public void VolverMapa()
    {
        StartCoroutine(CargarNivel(SceneManager.GetActiveScene().buildIndex - 1));
    }
    /*
        Reanuda el juego y se va para el menu principal
    */
    public void CargarMenu()
    {
        StartCoroutine(CargarNivel(0));
        Time.timeScale = 1f;
    }
    IEnumerator CargarNivel(int Nivel){

        transicion.SetTrigger("Inicio");
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(Nivel);

    }
}
