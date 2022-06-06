using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class RemoveItem : EditorWindow
{
    private string name;
    [MenuItem("Window/Remove Item")]
    public static void Show()
    {
        GetWindow(typeof(RemoveItem));
    }

    private void OnGUI()
    {
        name = EditorGUILayout.TextField("Name", name);

        if (GUILayout.Button("Remove"))
        {
            ItemContainer.Remove(name);
        }
    }
}
