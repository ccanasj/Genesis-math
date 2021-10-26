using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EfectoEscribir : MonoBehaviour
{

        public TextMeshProUGUI TextoDialogo;
        float delay = 0.02f;

        /*
            Realiza el efecto de escritura en los textos
        */
        public IEnumerator TypeChar(string dialogo){

        TextoDialogo.text = "";
        foreach (char letra in dialogo.ToCharArray())
        {
            TextoDialogo.text += letra;
            yield return new WaitForSeconds(delay);
        }

    }
}
