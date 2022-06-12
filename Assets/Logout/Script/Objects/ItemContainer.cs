using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

public class ItemContainer
{

    private static string ITEM_PATH = Application.streamingAssetsPath+ "/JSON/" + "Items.json";

    [System.Serializable]
    public class SaveObject
    {
        public ItemData itemData;
        public GameObject prefab;

        public SaveObject(ItemData itemDataList, GameObject prefab)
        {
            this.itemData = itemDataList;
            this.prefab = prefab;
        }
    }

    [System.Serializable]
    public class SaveObjectList
    {
        public List<SaveObject> saveObjects = new List<SaveObject>();

        public List<ItemData> GetItemDatas()
        {
            List<ItemData> itemDatas = new List<ItemData>();
            foreach (SaveObject save in saveObjects)
            {
                itemDatas.Add(save.itemData);
            }
            return itemDatas;
        }

        public bool ContainsProduct(string name)
        {
            foreach (SaveObject save in saveObjects)
            {
                if (save.itemData.name == name)
                {
                    return true;
                }
            }
            return false;
        }
    }

    public static void Save(ItemData data, GameObject prefab)
    {
        //create new saveObjectlist
        SaveObject saveObject = new SaveObject(data, prefab);
        SaveObjectList saveObjectList = new SaveObjectList();

        //if file exists, overwrite it
        if (File.Exists(ITEM_PATH))
        {
            saveObjectList = Load(); //load the saveObjectList
            if (saveObjectList.ContainsProduct(saveObject.itemData.name)) // if product already exist return 
            {
                Debug.Log("Product already exists");
                return;
            }
            File.Delete(ITEM_PATH); //then delete the file
        }

        saveObjectList.saveObjects.Add(saveObject);//add the saveObject to the list

        //save new file
        string json = JsonUtility.ToJson(saveObjectList);
        File.WriteAllText(ITEM_PATH, json);
        Debug.Log("Saved at " + ITEM_PATH);
    }

    //this code is just for local list updating
    //the use of this is to prevent the list to have items already removed
    //only send the list representing the real content present in game
    private static void UpdateSaveFile(List<SaveObject> saveObjectList)
    {
        if (saveObjectList.Count > 0)
        {
            foreach (SaveObject save in saveObjectList)
            {
                {
                    Save(save.itemData, save.prefab);
                }
            }
        }
        else
        {
            //if file exists 
            //clear it
            if (File.Exists(ITEM_PATH))
            {
                File.Delete(ITEM_PATH);
            }
        }
    }

    public static void Remove(string name)
    {
        //load save and remove the product with the name given
        SaveObjectList saveObjectList = new SaveObjectList();
        if (File.Exists(ITEM_PATH))
        {
            saveObjectList = Load(); //load the saveObjectList
            foreach (SaveObject save in saveObjectList.saveObjects)
            {
                if (save.itemData.name == name)
                {
                    saveObjectList.saveObjects.Remove(save);
                    UpdateSaveFile(saveObjectList.saveObjects);
                    break;
                }
            }
        }
    }

    public static SaveObjectList Load()
    {
        //if json file exists 
        //read values and return
        if (File.Exists(ITEM_PATH))
        {
            string json = File.ReadAllText(ITEM_PATH);
            Debug.Log("Loaded at " + ITEM_PATH);
            return JsonUtility.FromJson<SaveObjectList>(json);
        }
        else
        {
            //if return new save object 
            return new SaveObjectList();
        }
    }
}
