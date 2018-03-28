using UnityEditor;
using UnityEngine;

//source : https://answers.unity.com/questions/442342/how-to-make-public-variable-read-only-during-run-t.html

[CustomPropertyDrawer(typeof(ReadOnlyOnPlayAttribute))]
public class ReadOnlyOnPlay : PropertyDrawer
{
    public override float GetPropertyHeight(SerializedProperty property,
                                            GUIContent label)
    {
        return EditorGUI.GetPropertyHeight(property, label, true);
    }

    public override void OnGUI(Rect position,
                                SerializedProperty property,
                                GUIContent label)
    {
        GUI.enabled = !Application.isPlaying;
        EditorGUI.PropertyField(position, property, label, true);
        GUI.enabled = true;
    }
}
