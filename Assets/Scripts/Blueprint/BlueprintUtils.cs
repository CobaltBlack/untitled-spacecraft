using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;
using UnityEngine;

public class BlueprintUtils {
  public static BlueprintNew ReadBlueprintFile(string filepath) {
    BlueprintNew bp = null;
    FileStream file = new FileStream(filepath, FileMode.Open);
    try {
      BinaryFormatter formatter = new BinaryFormatter();
      bp = (BlueprintNew)formatter.Deserialize(file);
    } catch (SerializationException e) {
      Debug.Log("Failed to deserialize. Reason: " + e.Message);
      throw;
    } finally {
      file.Close();
    }

    return bp;
  }

  public static string SaveBlueprint(BlueprintNew bp) {
    string filepath = Application.persistentDataPath + "/" + FormatBlueprintFilename(bp);
    Debug.Log(filepath);
    FileStream file = File.Create(filepath);
    try {
      BinaryFormatter bf = new BinaryFormatter();
      bf.Serialize(file, bp);
    } catch (SerializationException e) {
      Debug.Log("Failed to Serialize. Reason: " + e.Message);
      throw;
    } finally {
      file.Close();
    }

    return filepath;
  }

  public static string FormatBlueprintFilename(BlueprintNew bp) {
    return bp.Name + ".bp";
  }

  public static bool ValidateBlueprint(BlueprintNew bp) {
    // All Ids must be valid
    // Equipment must not exceed internal cargo space on ship
    // Ship must have enough power?? (somehow handle low power case...)
    return true;
  }
}
