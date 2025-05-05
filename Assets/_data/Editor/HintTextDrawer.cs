using UnityEditor;
using UnityEngine;

[CustomPropertyDrawer(typeof(HintTextAttribute))]
public class HintTextDrawer : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        HintTextAttribute hint = (HintTextAttribute)attribute;

        if (property.propertyType == SerializedPropertyType.String)
        {
            EditorGUI.BeginProperty(position, label, property);

            string value = property.stringValue;
            string displayed = string.IsNullOrEmpty(value) ? hint.Hint : value;

            Color originalColor = GUI.color;

            if (string.IsNullOrEmpty(value))
                GUI.color = new Color(0.5f, 0.5f, 0.5f); // màu xám mờ cho hint

            property.stringValue = EditorGUI.TextField(position, label, displayed);

            GUI.color = originalColor;
            EditorGUI.EndProperty();
        }
        else
        {
            EditorGUI.LabelField(position, label.text, "Use [HintText] with string only.");
        }
    }
}
