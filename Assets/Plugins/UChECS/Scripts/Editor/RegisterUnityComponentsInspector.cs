using System;
using uchlab.ecs.zenject;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(RegisterUnityComponents))]
public class RegisterUnityComponentsInspector : Editor {

    public override void OnInspectorGUI() {
        serializedObject.Update();

        RegisterUnityComponents targetMb = (RegisterUnityComponents)target;

        EditorGUILayout.BeginHorizontal();

        if (GUILayout.Button("All")) {
            GameObject go = targetMb.gameObject;
            Component[] componentList = go.GetComponents<Component>();

            targetMb.UnityComponents.Clear();
            for (var i = 0; i < componentList.Length; i++) {
                if (!(componentList[i] is RegisterComponentBehaviour))
                {
                    targetMb.UnityComponents.Add(componentList[i]);
                }
            }

        }

        if (GUILayout.Button("None")) {

            targetMb.UnityComponents.Clear();
        }

        EditorGUILayout.EndHorizontal();
        EditorGUI.indentLevel += 1;
        DrawLineWithComponents();
        EditorGUI.indentLevel -= 1;

        serializedObject.ApplyModifiedProperties();
    }

    private void DrawLineWithComponents() {
        RegisterUnityComponents targetMb = (RegisterUnityComponents)target;
        GameObject go = targetMb.gameObject;

        Component[] componentList = go.GetComponents<Component>();

        bool wasSelected;
        Type componentType;

        if (componentList.Length <= 2) {
            EditorGUILayout.LabelField("No components available to register");
            return;
        }

        for (var i = 0; i < componentList.Length; i++) {
            if (!(componentList[i] is RegisterComponentBehaviour))
            {
                componentType = componentList[i].GetType();
                wasSelected = targetMb.UnityComponents.IndexOf(componentList[i]) >= 0;
                if (wasSelected != EditorGUILayout.Toggle(componentType.ToString().Replace("UnityEngine.", ""), wasSelected)) {
                    if (wasSelected) {
                        targetMb.UnityComponents.Remove(componentList[i]);
                    } else {
                        targetMb.UnityComponents.Add(componentList[i]);
                    }
                }
            }
        }


    }


}
