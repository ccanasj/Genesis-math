using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class SistemaGuardado
{

    public static void Guardar(){
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/jugador.cazais";
        FileStream stream = new FileStream(path,FileMode.Create);

        DatosJugador datos = new DatosJugador();

        formatter.Serialize(stream,datos);
        stream.Close();
    }
    public static DatosJugador Cargar(){
        string path = Application.persistentDataPath + "/jugador.cazais";
        Debug.Log(path);
        if(File.Exists(path)){

            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path,FileMode.Open);

            DatosJugador datos = (DatosJugador) formatter.Deserialize(stream);
            stream.Close();

            return datos;
        } else {
            Debug.LogError("El archivo de guardado no se encontro en "  + path);
            return null;
        }
    }
}
