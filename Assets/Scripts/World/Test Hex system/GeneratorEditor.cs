using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

//[CustomEditor(typeof(ContinentsMap))]
public class GeneratorEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        ContinentsMap Map = (ContinentsMap)target;

        DrawDefaultInspector();

        if (GUILayout.Button("Generate"))
            Map.GenerateMap();
    }
}
