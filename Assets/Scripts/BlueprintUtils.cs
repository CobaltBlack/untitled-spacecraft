using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;
using UnityEngine;

public class BlueprintUtils
{
    public static Blueprint ReadBlueprintFile(string filepath)
    {
        Blueprint bp = null;
        FileStream file = new FileStream(filepath, FileMode.Open);
        try
        {
            BinaryFormatter formatter = new BinaryFormatter();
            bp = (Blueprint)formatter.Deserialize(file);
        }
        catch (SerializationException e)
        {
            Debug.Log("Failed to deserialize. Reason: " + e.Message);
            throw;
        }
        finally
        {
            file.Close();
        }
        
        return bp;
    }

    public static void SaveBlueprint(Blueprint bp)
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/" + FormatBlueprintFilename(bp));
        bf.Serialize(file, bp);
        file.Close();

    }

    private static string FormatBlueprintFilename(Blueprint bp)
    {
        return bp.Name + ".bp";
    }
}
