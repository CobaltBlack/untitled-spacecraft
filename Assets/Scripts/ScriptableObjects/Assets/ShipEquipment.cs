using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ShipEquipmentType {
  Engine,
  Power,
  Storage,
  Special,
}

public abstract class ShipEquipment : ScriptableObject {
  public string Id; // Unique ID - must match prefab name

  // Display values
  public string Name;
  public string Description;
  public Sprite Sprite;

  // Gameplay values
  public ShipEquipmentType Type;
  public float Mass;
  public float PowerConsumption;
  public uint CargoSize;
  public List<ResourceAmount> BuildCost;
  public List<ResourceAmount> ResearchCost;
}
