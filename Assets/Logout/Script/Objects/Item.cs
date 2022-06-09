using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum ItemType
{
    Bed,
    Toy,
    Bathroom,
    Food,
}

public class Item : MonoBehaviour
{
    [SerializeField] private ItemData itemData;

    public ItemData GetData()
    {
        return new ItemData();
    }
}

[System.Serializable]
public struct ItemData
{
    public Sprite image;
    public string name;
    public string description;
    public float cost;
    public ItemType type;

    public ItemData(Sprite image, string name, string description, float cost, ItemType type)
    {
        this.image = image;
        this.name = name;
        this.description = description;
        this.cost = cost;
        this.type = type;
    }
}