using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum ShipAttachmentType {
  Construction,
  Mining,
  Towing,
  SolarPanel,
  Storage,
}

public abstract class ShipAttachment : ScriptableObject {
  public string Id; // Unique ID - must match prefab name

  // Display values
  public string Name;
  public string Description;
  public Sprite Sprite;

  // Gameplay values
  public ShipAttachmentType Type;
  public SlotSize Size;
  public float Mass;
  public float PowerConsumption;
  public List<ResourceAmount> BuildCost;
  public List<ResourceAmount> ResearchCost;
}
