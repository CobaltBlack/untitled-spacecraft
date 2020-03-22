using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;
using UnityEngine;

public class BlueprintUtils
{
    private static Dictionary<string, BlueprintPart> BpPartMap = new Dictionary<string, BlueprintPart>();

    public static void InitializeBlueprintParts()
    {
        var parts = Resources.LoadAll("BlueprintParts", typeof(BlueprintPart));
        foreach (BlueprintPart part in parts)
        {
            BpPartMap.Add(part.Id, part);
        }
    }

    public static BlueprintPart GetBpPartById(string id)
    {
        // How to handle non-existent ID?
        if (!BpPartMap.ContainsKey(id))
        {
            return null;
        }
        return BpPartMap[id];
    }


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

    public static string SaveBlueprint(Blueprint bp)
    {
        string filepath = Application.persistentDataPath + "/" + FormatBlueprintFilename(bp);
        Debug.Log(filepath);
        FileStream file = File.Create(filepath);
        try
        {
            BinaryFormatter bf = new BinaryFormatter();
            bf.Serialize(file, bp);
        }
        catch (SerializationException e)
        {
            Debug.Log("Failed to Serialize. Reason: " + e.Message);
            throw;
        }
        finally
        {
            file.Close();
        }

        return filepath;
    }

    public static string FormatBlueprintFilename(Blueprint bp)
    {
        return bp.Name + ".bp";
    }
}
