using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class RegisterItem : EditorWindow
{
    public Sprite image;
    public string name;
    public string description;
    public float cost;
    public ItemType type;

    [MenuItem("Window/Register Item")]
    public static void Show()
    {
        GetWindow(typeof(RegisterItem));
    }

    private void OnGUI() {
        image = (Sprite)EditorGUILayout.ObjectField("Image", image, typeof(Sprite), false);
        name = EditorGUILayout.TextField("Name", name);
        description = EditorGUILayout.TextField("Description", description);
        cost = EditorGUILayout.FloatField("Cost", cost);
        type = (ItemType)EditorGUILayout.EnumPopup("Type", type);

        if (GUILayout.Button("Register")) {
            ItemData itemData = new ItemData(image, name, description, cost, type);
            ItemContainer.Save(itemData);
        }
    }
}
