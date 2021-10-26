using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(AjustarAlGrid))]
public class AjustarAlGridCustomInspector : Editor {
    
    private AjustarAlGrid scriptPrincipal;
    private float restoX;
    private float restoY;
    private float newPositionX;
    private float newPositionY;
    private float centroCasilla;

    private void OnEnable()
    {
        scriptPrincipal = (AjustarAlGrid)target;
    }

    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        if (GUILayout.Button("Ajustar al Grid"))
        {
            AutoAjustar();
        }
    }

    private void AutoAjustar()
    {        
        centroCasilla = Ajustes.Instancia.tamanioCasilla / 2;
        restoX = scriptPrincipal.gameObject.transform.position.x % Ajustes.Instancia.tamanioCasilla;
        restoY = scriptPrincipal.gameObject.transform.position.y % Ajustes.Instancia.tamanioCasilla;

        if (scriptPrincipal.gameObject.transform.position.x % centroCasilla != 0 || scriptPrincipal.gameObject.transform.position.y % centroCasilla != 0)
        {
            newPositionX = scriptPrincipal.gameObject.transform.position.x - (scriptPrincipal.gameObject.transform.position.x % Ajustes.Instancia.tamanioCasilla) + centroCasilla;
            newPositionY = scriptPrincipal.gameObject.transform.position.y - (scriptPrincipal.gameObject.transform.position.y % Ajustes.Instancia.tamanioCasilla) + centroCasilla;
            scriptPrincipal.gameObject.transform.position = new Vector2(newPositionX, newPositionY);
        }        
        
    }
}
