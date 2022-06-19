using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class SaveSystem
{
    private string path = Application.persistentDataPath + "/save.bin";
    public void SaveONG(ONG ong)
    {
        //create a new ONGData object with the current ONG's data
        //create a binary formater 
        //create a file stream
        //serialize ongData into the file stream
        //close the file stream
        ONGData ongData = new ONGData(ong);

        BinaryFormatter bf = new BinaryFormatter();
        FileStream stream = new FileStream(path, FileMode.Create);
        bf.Serialize(stream, ongData);
        stream.Close();
        Debug.Log("Ong saved at " + path);
    }

    public ONGData LoadONG()
    {
        //if the file exists
        //load the ongData from the file
        //return ONGData

        if (File.Exists(path))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);
            ONGData ong = bf.Deserialize(stream) as ONGData;
            stream.Close();
            Debug.Log("loaded file at " + path);
            return ong;
        }
        else
        {
            Debug.LogError("File not found");
            return null;
        }
    }
}
