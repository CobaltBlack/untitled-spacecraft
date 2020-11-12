using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ResourceType {
  WaterIce,
  MetalOre,
  MetalPlate,
  HeavyOre,
  HeavyAlloy,
  VolatileGas,
  Crystals,
  CatalyticDust,
  Plasma,
  Antimatter,
  DarkMatter,
  StrangeMatter,
}

public class ResourceManager : Singleton<ResourceManager> {
  public Dictionary<ResourceType, ResourceData> ResourceByType = new Dictionary<ResourceType, ResourceData>();

  void Start() {
    var resources = Resources.LoadAll("ResourcesData", typeof(ResourceData));
    foreach (ResourceData r in resources) {
      ResourceByType.Add(r.Type, r);
    }
  }
  public ResourceData GetResource(ResourceType type) {
    return ResourceByType[type];
  }
}
