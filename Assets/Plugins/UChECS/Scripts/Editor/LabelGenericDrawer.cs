using uchlab.ecs.zenject;
using UnityEditor;
using UnityEngine;

[CustomPropertyDrawer(typeof(LabelGenericAttribute))]
public class LabelGenericLabel : PropertyDrawer {

    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {

        label.text = ObjectNames.NicifyVariableName(property.type);
        EditorGUI.PropertyField(position, property, label, true);
    }

    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        return EditorGUI.GetPropertyHeight(property);
    }
}
