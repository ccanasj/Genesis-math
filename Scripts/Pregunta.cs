using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class Pregunta : MonoBehaviour
{
    public TextMeshProUGUI TextoPregunta;
    public TextMeshProUGUI TextoTiempo;
    public TextMeshProUGUI textoRespues1;
    public TextMeshProUGUI textoRespues2;
    public TextMeshProUGUI textoRespues3;
    public TextMeshProUGUI TextoAyuda;
    public TextMeshProUGUI Puntos;
    public Button Libro;
    public float tiempo = 60f;
    public int c;
    int a, b, f, c1;
    int ale, ale2;
    int [] orden = new int[3];
    char[] op = { '+', '-', '*', '/' };
    
    public SistemaBatalla sistema;
    /*
        Se actualiza el tiempo que pasa durante la pregunta
    */
    public void Update()
    {
        tiempo -= Time.deltaTime;
        TextoTiempo.text = " " + tiempo.ToString("f0");
        Puntos.text = "X" + MenuPrincipal.Puntos;
        if(MenuPrincipal.Puntos <= 0){
            Libro.interactable = false;
        } else {
            Libro.interactable = true;
        }
        if (tiempo <= 0)
        {
            tiempo = 0;
            TextoTiempo.text = " " + tiempo.ToString("f0");
            sistema.Estado = EstadosBatalla.TurnoEnemigo;
            sistema.StartCoroutine(sistema.TurnoEnemigo());
            sistema.Pregunta.SetActive(false);
        }
    }
    /*
        Genera numero aleatorios y realiza la operacion de suma
    */
    public void OperacionSUMA()
    {
        a = Random.Range(10, 500);
        b = Random.Range(10, 501);

        TextoPregunta.text = a + " + " + b;
        TextoAyuda.text = "Recuerda que tambien puedes ayudarte con tus dedos para sumar";
        c = a + b;
    }
    /*
        Genera numero aleatorios y realiza la operacion de resta
    */
    public void OperacionRESTA()
    {
        a = Random.Range(10, 1000);
        b = Random.Range(10, 501);

        TextoPregunta.text = a + " - " + b;
        TextoAyuda.text = "Recuerda, si a es mayor que b El resultado sera positivo";
        c = a - b;
    }
    /*
        Genera numero aleatorios y realiza la operacion de multiplicacion
    */
    public void OperacionMUL()
    {
        a = Random.Range(5, 30);
        b = Random.Range(5, 16);

        TextoPregunta.text = a + " X " + b;
        TextoAyuda.text = "Recuerda, que tambien puedes sumar " + a + " + " + a + "... La cantidad de veces de " + b;
        c = a * b;
    }
    /*
        Genera numero aleatorios y realiza la operacion de division
    */
    public void OperacionDIV()
    {
        a = Random.Range(2, 20);
        b = a * (Random.Range(2, 14));

        TextoPregunta.text = b + " / " + a;
        TextoAyuda.text = "Recuerda, que el denominador lo puedes sumar hasta que sea igual al numerador";
        c = b / a;
    }
    /*
        Reinicia el tiempo al momento de apretar el boton
    */
    public void ReiniciarTiempo()
    {
        tiempo = 60f;
    }
    /*
        Genera un numero aleatorio el cual sera la operacion a realizar
    */
    public void OperacionCurar()
    {
        int ale = Random.Range(0, 4);
        switch (ale)
        {
            case 0:
                OperacionSUMA();
                break;
            case 1:
                OperacionRESTA();
                break;
            case 2:
                OperacionMUL();
                break;
            case 3:
                OperacionDIV();
                break;
            default:
                break;
        }

    }
    /*
        Genera 3 numeros aleatorios y genera 2 operciones aleatoriamente y los agrupa para generar una operacion
    */
    public void OperacionEspecial()
    {
        ale = Random.Range(0, 4);
        ale2 = Random.Range(0, 3);
        switch (ale)
        {
            case 0:
                a = Random.Range(10, 51);
                b = Random.Range(10, 50);
                c1 = a + b;
                break;
            case 1:
                a = Random.Range(10, 81);
                b = Random.Range(10, 40);
                c1 = a - b;
                break;
            case 2:
                a = Random.Range(3, 16);
                b = Random.Range(3, 10);
                c1 = a * b;
                break;
            case 3:
                b = Random.Range(3, 16);
                a = (b * Random.Range(3, 10));
                c1 = a / b;
                break;
            default:
                break;
        }
        switch (ale2)
        {
            case 0:
                f = Random.Range(3, 30);
                c = c1 + f;
                break;
            case 1:
                f = Random.Range(1, 15);
                c = c1 - f;
                break;
            case 2:
                f = Random.Range(2, 11);
                c = c1 * f;
                break;
            default:
                break;
        }
        TextoPregunta.text = " (" + a + " " + op[ale] + " " + b + ") " + op[ale2] + " " + f;
        TextoAyuda.text = "Recuerda solucionar primero (" + a + " " + op[ale] + " " + b + ") ";
    }
    int Lenght = 3;
    
    public void GenerarRespuestas()
    {
        orden[0] = (c + Random.Range(-5, 6));
        orden[1] = c;
        orden[2] = (c + Random.Range(5, 15));
        List<int> list = new List<int>();
        for (int j = 0; j < Lenght; j++)
        {
            int Rand = Random.Range(0,3);
            while(list.Contains(Rand))
            {
                Rand = Random.Range(0,3);
            }
            list.Add(Rand);
        }
            textoRespues1.text = "" + orden[list[1]];
            textoRespues2.text = "" + orden[list[0]];
            textoRespues3.text = "" + orden[list[2]];
        list.Clear();
        MenuPrincipal.Puntos--;
    }


}
