using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Ajustes))]
public class AjustesCustomInspector : Editor {

    private Ajustes scriptPrincipal;
    private int selectedTag;

    private void OnEnable()
    {
        scriptPrincipal = (Ajustes)target;
        selectedTag = CurrentTag();
    }

    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        selectedTag = EditorGUILayout.Popup("Tag Interacciones", selectedTag, UnityEditorInternal.InternalEditorUtility.tags);
        scriptPrincipal.tagInteraccion = UnityEditorInternal.InternalEditorUtility.tags[selectedTag];
        
    }

    private int CurrentTag()
    {
        for (int i = 0; i < UnityEditorInternal.InternalEditorUtility.tags.Length; i++)
        {
            if (UnityEditorInternal.InternalEditorUtility.tags[i] == scriptPrincipal.tagInteraccion)
                return i;
        }
        return 0;
    }
}
