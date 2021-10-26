using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemigo : MonoBehaviour
{
    public LayerMask personajeL;
    public GameObject exclamacion;   
    public int direccion,ID;
    private void Update()
    {
        switch(direccion){
            case 1:
                RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.left, 0.96f, personajeL);
                Debug.DrawRay(transform.position, Vector2.left,Color.red ,5,true);
                    if (hit.collider != null)
                    {
                        StartCoroutine(Encuentro());
                    }  
            break;
            case 2:
                RaycastHit2D hit2 = Physics2D.Raycast(transform.position, Vector2.right, 0.96f, personajeL);
                Debug.DrawRay(transform.position, Vector2.right,Color.red ,5,true);
                    if (hit2.collider != null)
                    {
                        StartCoroutine(Encuentro());
                    }
            break;
            default:
            break;
        }
            
    }
    IEnumerator Encuentro(){
        Mapa.IDgeneral = ID;
        FindObjectOfType<Mapa>().GuardarDatos();
        exclamacion.SetActive(true);
        Personaje.PuedeMoverse = false;
        yield return new WaitForSecondsRealtime(1f);
        FindObjectOfType<Transicion>().CargarSiguienteEscena();
    }

}
