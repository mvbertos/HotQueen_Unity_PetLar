using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

public class ItemContainer
{

    private static string ITEM_PATH = Application.persistentDataPath + "/Items.xml";
    public class SerializableItemData
    {
        public List<ItemData> itemDataList = new List<ItemData>();
    }

    public static void Save(ItemData data)
    {
        SerializableItemData serializableItemData = new SerializableItemData();
        serializableItemData = Load();
        serializableItemData.itemDataList.Add(data);

        //serialize and save data
        XmlSerializer serializer = new XmlSerializer(typeof(SerializableItemData));
        FileStream stream = new FileStream(ITEM_PATH, FileMode.Create);
        serializer.Serialize(stream, serializableItemData);
        stream.Close();
        Debug.Log("Saved at " + ITEM_PATH);
    }

    public static SerializableItemData Load()
    {
        //deserialize and load data
        if (File.Exists(ITEM_PATH))//if file exist load data
        {
            SerializableItemData serializableItemData = new SerializableItemData();
            XmlSerializer serializer = new XmlSerializer(typeof(SerializableItemData));
            FileStream stream = new FileStream(ITEM_PATH, FileMode.Open);
            serializableItemData = serializer.Deserialize(stream) as SerializableItemData;
            stream.Close();
            return serializableItemData;
        }
        else //else create new file
        {
            SerializableItemData serializableItemData = new SerializableItemData();
            XmlSerializer serializer = new XmlSerializer(typeof(SerializableItemData));
            FileStream stream = new FileStream(ITEM_PATH, FileMode.Create);
            serializer.Serialize(stream, serializableItemData);
            stream.Close();
            return serializableItemData;
        }
    }
}
