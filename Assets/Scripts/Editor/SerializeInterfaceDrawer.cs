using System;
using UnityEditor;
using UnityEngine;
using Object = UnityEngine.Object;

[CustomPropertyDrawer(typeof(SerializeInterfaceAttribute))]
public class SerializeInterfaceDrawer : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        Type requiredType = (attribute as SerializeInterfaceAttribute).Type;

        UpdatePropertyValue(property, requiredType);
        UpdateDropIcon(position, requiredType);

        property.objectReferenceValue = EditorGUI.ObjectField(position, label, property.objectReferenceValue, typeof(GameObject), true);
    }

    private bool IsInvalidObject(Object @object ,Type requiredType)
    {
        if(@object is GameObject gameObject)
            return gameObject.GetComponent(requiredType) == null;

        return true;
    }

    private void UpdatePropertyValue(SerializedProperty property, Type requiredType)
    {
        if (property.objectReferenceValue == null)
            return;

        if (IsInvalidObject(property.objectReferenceValue, requiredType))
            property.objectReferenceValue = null;
    }

    private void UpdateDropIcon(Rect position, Type requiredType)
    {
        if (position.Contains(Event.current.mousePosition) == false)
            return;

        foreach(Object @object in DragAndDrop.objectReferences)
        {
            if (IsInvalidObject(@object, requiredType))
            {
                DragAndDrop.visualMode = DragAndDropVisualMode.Rejected;
                break;
            }
        }
    }
}
