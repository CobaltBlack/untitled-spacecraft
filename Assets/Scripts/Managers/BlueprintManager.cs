using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlueprintManager : Singleton<BlueprintManager> {
  private static Dictionary<string, BlueprintPart> BpPartMap = new Dictionary<string, BlueprintPart>();
  private static Dictionary<BpPartType, List<BlueprintPart>> BpPartsByType = new Dictionary<BpPartType, List<BlueprintPart>>();

  void OnEnable() {
    InitializeBlueprintParts();
  }

  void InitializeBlueprintParts() {
    var parts = Resources.LoadAll("BlueprintParts", typeof(BlueprintPart));
    foreach (BlueprintPart part in parts) {
      BpPartMap.Add(part.Id, part);

      if (!BpPartsByType.ContainsKey(part.Type)) {
        BpPartsByType[part.Type] = new List<BlueprintPart>();
      }
      BpPartsByType[part.Type].Add(part);
    }
  }

  public BlueprintPart GetBpPartById(string id) {
    // How to handle non-existent ID?
    if (!BpPartMap.ContainsKey(id)) {
      return null;
    }
    return BpPartMap[id];
  }

  public List<BlueprintPart> GetBpPartsListByType(BpPartType type) {
    return BpPartsByType[type];
  }
}
