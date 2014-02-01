﻿using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;

[CustomEditor(typeof(AudioLayerManager))]
public class AudioLayerManagerEditor : EditorPlus
{
    private string description = "The layers are based on the hardcoded AudioLayersEnum.\n The enum can be found in the AudioLayerManager.cs file. This might change in the future.";
    private string settingsNameTooltip = "The layer names are hardcoded in the AudioLayerManager";

    public override void OnInspectorGUI()
    {
        //EditorGUI.up
        
        AudioLayerManager manager = target as AudioLayerManager;
        SerializedObject soManager = new SerializedObject(manager);
        
        int count = manager.AudioLayerSettings.Count;

        if(SavedFoldout("Description", -1))
            EditorGUILayout.TextArea(description);
        for (int i = 0; i < count; i++)
        {
            AudioLayerSettings settings = manager.AudioLayerSettings[i]; 
            GUIContent name = new GUIContent("AudioLayer: " + settings.Layer.ToString(), settingsNameTooltip);

            //bool cont = true;
            //SerializedProperty sp = soManager.GetIterator();
            //while (cont)
            //{
            //    //Debug.Log(i +  " + " + sp.name + " - " + sp.type + " - depth: " + sp.depth + " - " + sp.hasChildren);// +sp.arraySize + " - " + sp.
            //    //SerializedProperty sp2 = sp.GetArrayElementAtIndex(i);
            //    if (!sp.name.StartsWith("m_"))
            //        Debug.Log(i + " + " + sp.name + " - " + sp.type + " - depth: " + sp.depth + " - " + sp.hasChildren);
            //    cont = sp.Next(true);
            //}

            if (SavedFoldout(name, i))
            {
                EditorGUI.indentLevel++;
                {
                    SerializedProperty prop = soManager.FindProperty(string.Format("AudioLayerSettings.Array.data[{0}]", i));
                    prop.Next(true);
                    prop.Next(true);
                    //Debug.Log(prop.name);
                    EditorGUILayout.PropertyField(prop);
                    prop.Next(true);
                    EditorGUILayout.PropertyField(prop);
                    prop.Next(true);
                    EditorGUILayout.PropertyField(prop);
                    
                }
                EditorGUI.indentLevel--;
            }
        }
        soManager.ApplyModifiedProperties();
        
    }
}
