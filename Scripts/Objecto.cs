using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class Objecto : IInteractivo
{
    public Sprite spriteAbierto;
    public TextMeshProUGUI textoPuntos;
    public bool cerrado = true;
    public override void Interactuar(Personaje personaje)
    {
        if (cerrado)
        {
            GetComponent<SpriteRenderer>().sprite = spriteAbierto;
            MenuPrincipal.Puntos++;
            textoPuntos.text = "x " + MenuPrincipal.Puntos;
            cerrado = false;
        }
    }
}
