using System.Collections.Generic;
using UnityEngine;

public class EventoBase
{
    public string nombreEvento;
    public EventoBase() { nombreEvento = this.GetType().Name; }
}

public class EventoTeletransportarse : EventoBase
{
    public Vector2 Destino { get; private set; }
    public Personaje Personaje { get; private set; }

    public EventoTeletransportarse(Vector2 destino, Personaje personaje)
    {
        Destino = destino;
        Personaje = personaje;
    }
}

public class EventoTeletransportarseCentroPokemon : EventoBase
{
    public Personaje Personaje { get; private set; }

    public EventoTeletransportarseCentroPokemon(Personaje personaje)
    {
        Personaje = personaje;
    }
}

