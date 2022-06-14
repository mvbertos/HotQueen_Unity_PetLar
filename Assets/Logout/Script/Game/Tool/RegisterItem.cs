using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

//if unity editor
#if UNITY_EDITOR
public class RegisterItem : EditorWindow
{
    public Sprite image;
    public string name;
    public string description;
    public float cost;
    public ItemType type;
    public GameObject prefab;

    [MenuItem("Window/Register Item")]
    public static void Show()
    {
        GetWindow(typeof(RegisterItem));
    }

    private void OnGUI()
    {
        name = EditorGUILayout.TextField("Name", name);
        description = EditorGUILayout.TextField("Description", description);
        cost = EditorGUILayout.FloatField("Cost", cost);
        type = (ItemType)EditorGUILayout.EnumPopup("Type", type);

        if (GUILayout.Button("Register"))
        {
            ItemData itemData = new ItemData(image, name, description, cost, type);
            ItemContainer.Save(itemData, prefab);
        }
    }
}
#endif