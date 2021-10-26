using System;
using UnityEngine;

public class Audio : MonoBehaviour
{
    public Sonido[] sonidos;

    void Awake(){
        foreach (Sonido s in sonidos)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;

            s.source.volume = s.volumen;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
        }
    }
    public void Reproducir(string nombre){
        Sonido s = Array.Find(sonidos, sonido => sonido.nombre == nombre);
        if (s == null)
        {
            Debug.LogWarning("No se encontro el sonido");
            return;
        }
        s.source.Play();
    }

        public void Detener(string nombre){
        Sonido s = Array.Find(sonidos, sonido => sonido.nombre == nombre);
        if (s == null)
        {
            Debug.LogWarning("No se encontro el sonido");
            return;
        }
        s.source.Stop();
    }
}

