using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public enum EstadosBatalla
{
    Inicio, TurnoJugador, TurnoEnemigo, Victoria, Derrota, SeleccionAtaque, Curar, AtaqueEspecial
}
public class SistemaBatalla : MonoBehaviour
{
    public EstadosBatalla Estado;

    public GameObject Jugador;
    public GameObject Enemigo;
    public GameObject Pregunta;

    public Transform JugadorZona;
    public Transform EnemigoZona;

    public EfectoEscribir escribir;

    public Transicion transicion;

    public TextMeshProUGUI TextoDialogo;

    public TMP_InputField input;
    public HUDBatalla JugadorHUD;
    public HUDBatalla EnemigoHUD;

    Unidad UnidadJugador;
    Unidad UnidadEnemigo;
    Pregunta Entrada;
    int aux;
    bool correcto, Muerto;
    float tiempoCorrecto;
    string textoDialogo;
    /*
        Inicializa los menus y el combate
    */
    void Start()
    {
        Entrada = Pregunta.GetComponent<Pregunta>();
        Pregunta.SetActive(false);
        JugadorHUD.DesactivarHUD(2);
        JugadorHUD.DesactivarHUD(1);
        Estado = EstadosBatalla.Inicio;
        StartCoroutine(SetupBatalla());
    }

    /*
        Inicializa los atributos del jugador y del enemigo
    */
    IEnumerator SetupBatalla()
    {
        GameObject JugadorGO = Instantiate(Jugador, JugadorZona);
        UnidadJugador = JugadorGO.GetComponent<Unidad>();

        UnidadJugador.nombre = MenuPrincipal.nombreJugador;

        GameObject EnemigoGO = Instantiate(Enemigo, EnemigoZona);
        UnidadEnemigo = EnemigoGO.GetComponent<Unidad>();

        FindObjectOfType<Audio>().Reproducir("Slime");

        TextoDialogo.text = "Encontraste a un " + UnidadEnemigo.nombre + " Salvaje...";
        StartCoroutine(escribir.TypeChar(TextoDialogo.text));

        JugadorHUD.setHUD(UnidadJugador);
        EnemigoHUD.setHUD(UnidadEnemigo);

        yield return new WaitForSeconds(2.5f);

        Estado = EstadosBatalla.TurnoJugador;
        TurnoJugador();
    }

    /*
        Realiza la accion de ataque, inflingiendo daño al enemigo
    */
    IEnumerator Ataque()
    {
        Pregunta.SetActive(false);
        if (Estado == EstadosBatalla.AtaqueEspecial)
        {
            Muerto = UnidadEnemigo.TakeDamage(UnidadJugador.GolpeEspecial(tiempoCorrecto));
            FindObjectOfType<Audio>().Reproducir("AtaqueEspecial");
            TextoDialogo.text = "Le has dado un golpe fuerte!!!";
        }
        else
        {
            Muerto = UnidadEnemigo.TakeDamage(UnidadJugador.Golpe(tiempoCorrecto));
            FindObjectOfType<Audio>().Reproducir("Ataque");
            TextoDialogo.text = "Le has dado!!!";
        }
        FindObjectOfType<Audio>().Reproducir("SlimeGolpe");
        StartCoroutine(escribir.TypeChar(TextoDialogo.text));
        EnemigoHUD.setHP(UnidadEnemigo.vidaActual, UnidadEnemigo);

        yield return new WaitForSeconds(1.5f);
        UnidadJugador.animator.SetTrigger("quieto");

        if (Muerto)
        {
            UnidadJugador.animator.SetBool("victoria", true);
            Estado = EstadosBatalla.Victoria;
            FinBatalla();
        }
        else
        {
            UnidadEnemigo.animator.SetTrigger("quieto");
            Estado = EstadosBatalla.TurnoEnemigo;
            StartCoroutine(TurnoEnemigo());
        }

    }
    /*
        Realiza la accion de curacion
    */
    IEnumerator Curacion()
    {
        Pregunta.SetActive(false);
        UnidadJugador.Curar();
        JugadorHUD.setHP(UnidadJugador.vidaActual, UnidadJugador);
        TextoDialogo.text = "Has meditado y recuperaste vida";
        FindObjectOfType<Audio>().Reproducir("Curacion");
        StartCoroutine(escribir.TypeChar(TextoDialogo.text));

        yield return new WaitForSeconds(2f);

        Estado = EstadosBatalla.TurnoEnemigo;
        StartCoroutine(TurnoEnemigo());

    }

    /*
        Recibe el valor que ingresa el jugador y compruba si esta correcto
    */
    public void RealizarOperacion()
    {
        correcto = int.TryParse(input.text, out aux);

        if (!correcto){
            Debug.Log("Incorrecto, debes introducir numeros");
        } else {

            if (Entrada.c == aux){

                if (Estado == EstadosBatalla.Curar){

                    StartCoroutine(Curacion());
                }
                else if (Estado == EstadosBatalla.AtaqueEspecial){

                    UnidadJugador.animator.SetTrigger("ataqueR");
                    tiempoCorrecto = Entrada.tiempo;
                    StartCoroutine(Ataque());
                } else {

                    UnidadJugador.animator.SetTrigger("ataqueS");
                    tiempoCorrecto = Entrada.tiempo;
                    StartCoroutine(Ataque());
                }
            } else {

                Entrada.tiempo -= 10f;
                if (Entrada.tiempo <= 0){

                    Estado = EstadosBatalla.TurnoEnemigo;
                    StartCoroutine(TurnoEnemigo());
                }
            }
        }
    }
    /*
        Activa la pregunta y realiza la operacion de cura
    */
    public void BotonCurar(){

        Entrada.tiempo = 60f;
        input.text = "";
        Pregunta.SetActive(true);
        StopAllCoroutines();
        TextoDialogo.text = "";
        Entrada.OperacionCurar();
        JugadorHUD.DesactivarHUD(1);
        Estado = EstadosBatalla.Curar;
    }
    /*
        Activa la pregunta y realiza la operacion de Ataque especial
    */
    public void BotonEspecial(){

        Entrada.tiempo = 60f;
        input.text = "";
        Pregunta.SetActive(true);
        StopAllCoroutines();
        TextoDialogo.text = "";
        Entrada.OperacionEspecial();
        JugadorHUD.DesactivarHUD(1);
        Estado = EstadosBatalla.AtaqueEspecial;
    }
    /*
        Activa la pregunta y realiza la operacion del Ataque seleccionado
    */
    public void BotonSeleccionA(){

        Entrada.tiempo = 60f;
        input.text = "";
        Pregunta.SetActive(true);
        Entrada.OperacionSUMA();
        JugadorHUD.DesactivarHUD(2);
    }
    /*
        Borra la ultima respuesta del jugador del input
    */
    public void Inputvacio(){
        input.text = "";
    }
    /*
        Comienza el turno del enemigo, inflingiendo daño al jugador
    */

    IEnumerator TiempoEntreEscenas(int i){

        
        switch (i)
        {
            case 1:
                yield return new WaitForSeconds(2f);
                transicion.VolverMapa();
            break;
            case 2:
                yield return new WaitForSeconds(4f);
                transicion.CargarMenu();
            break;
            
            default:
            break;
        }
       
        
    }
    public IEnumerator TurnoEnemigo(){

        Pregunta.SetActive(false);
        TextoDialogo.text = "Te atacan!!";
        StartCoroutine(escribir.TypeChar(TextoDialogo.text));

        yield return new WaitForSeconds(1.5f);

        FindObjectOfType<Audio>().Reproducir("RecibirGolpe");
        bool Muerto = UnidadJugador.TakeDamage(UnidadEnemigo.golpe);
        FindObjectOfType<Audio>().Reproducir("SlimeGolpe");
        JugadorHUD.setHP(UnidadJugador.vidaActual, UnidadJugador);
        UnidadEnemigo.animator.SetTrigger("ataque");

        yield return new WaitForSeconds(1f);

        UnidadEnemigo.animator.SetTrigger("quieto");

        if (Muerto){
            Estado = EstadosBatalla.Derrota;
            FinBatalla();
        }
        else{
            UnidadJugador.animator.SetTrigger("quieto");
            Estado = EstadosBatalla.TurnoJugador;
            TurnoJugador();
        }
    }
    /*
        Comprueba en que estado de batalla ha finalizado, si gano o perdio
    */
    void FinBatalla(){
        Pregunta.SetActive(false);
        if (Estado == EstadosBatalla.Victoria){
            Mapa.Muertos[Mapa.IDgeneral] = true;
            UnidadEnemigo.gameObject.SetActive(false);
            FindObjectOfType<Audio>().Detener("Slime");
            FindObjectOfType<Audio>().Reproducir("Victoria");
            Unidad.nivel++;
            StartCoroutine(TiempoEntreEscenas(1));
            TextoDialogo.text = "Has derrotado al " + UnidadEnemigo.nombre + " Buen trabajo";
        }
        else if (Estado == EstadosBatalla.Derrota){
            FindObjectOfType<Audio>().Reproducir("Derrota");
            StartCoroutine(TiempoEntreEscenas(2));
            Mapa.posicion = new Vector2(1.2f,-1.84f);
            TextoDialogo.text = "Te han derrotado, intenta practicar las diferentes operaciones aritmeticas";
            
        }
        SistemaGuardado.Guardar();
        StartCoroutine(escribir.TypeChar(TextoDialogo.text));
        
    }
    /*
        Comienza el turno del enemigo activando los botones de seleccion de accion
    */
    void TurnoJugador(){
        TextoDialogo.text = "Es tu momento, ataca... ";
        textoDialogo = TextoDialogo.text;
        StartCoroutine(escribir.TypeChar(TextoDialogo.text));
        JugadorHUD.ActivarHUD(1);
    }
    /*
        activa la seleccion de las operaciones para realizar el ataque 
    */
    public void BotonAtaque(){
        Estado = EstadosBatalla.SeleccionAtaque;
        JugadorHUD.DesactivarHUD(1);
        JugadorHUD.ActivarHUD(2);
        StopAllCoroutines();
        TextoDialogo.text = "";
    }
    /*
        Restaura el texto al momento de navegar entre los menus 
    */
    public void MostrarTexto(){
        TextoDialogo.text = textoDialogo;
    }

    public void Respuesta(TextMeshProUGUI textoRespuesta){
        int.TryParse(textoRespuesta.text, out aux);
        if (Entrada.c == aux)
        {
            if (Estado == EstadosBatalla.Curar){

                    StartCoroutine(Curacion());
                }
                else if (Estado == EstadosBatalla.AtaqueEspecial){
                    UnidadJugador.animator.SetTrigger("ataqueR");
                    tiempoCorrecto = Entrada.tiempo;
                    StartCoroutine(Ataque());
                } else {

                    UnidadJugador.animator.SetTrigger("ataqueS");
                    tiempoCorrecto = Entrada.tiempo;
                    StartCoroutine(Ataque());
                }
        } else {
            StartCoroutine(TurnoEnemigo());
        }
    }
}
