using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BpPartType {
  Cargo,
  Thruster,
  Mining,
  ShipComputer
}

public enum BpPartOrientation {
  Up,
  Down,
  Left,
  Right
}

// Data only class, created by deserializing blueprint JSON
[System.Serializable]
public class Blueprint {
  public string Name;
  public List<BlueprintPartPlaced> Parts;
}


// Includes positional values for the part placed in a Blueprint
[System.Serializable]
public class BlueprintPartPlaced {
  public string PartId;
  public int X;
  public int Y;
  public BpPartOrientation Orientation;
}


// Ship part asset data
public abstract class BlueprintPart : ScriptableObject {
  public string Id; // Unique ID; should match prefab name

  // Display values
  public string Name;
  public string Description;
  public Sprite Sprite;

  // Gameplay values
  public BpPartType Type;
  public uint Width; // in tiles
  public uint Length; // in tiles
  public float Mass;
  public List<ResourceAmount> BuildCost;
  public List<ResourceAmount> ResearchCost;
}


// ========================================================
// New Code below:
// ========================================================

[System.Serializable]
public struct ResourceAmount {
  public ResourceType Type;
  public uint Amount;
}


public enum SlotSize {
  Small,
  Medium,
  Large
}

public enum SlotType {
  Attachment,
  Thruster,
}

[System.Serializable]
public struct ExternalSlot {
  public SlotType Type;
  public SlotSize Size;
  public Vector2 PositionOffset;
}

// Data only class, created by deserializing blueprint JSON
[System.Serializable]
public class BlueprintNew {
  public string Name;
  public string Description;

  public string ShipClassId;
  public List<string> ShipAttachmentIds; // Externally attached tools
  public List<string> ShipThrusterIds; 
  public List<string> ShipEquipmentIds; // Internal equipment

  // TODO: Initial signals/tags
  // TODO: Ship automation triggers
}

// Asset data
[CreateAssetMenu(menuName = "Ship Class")]
public class ShipClass : ScriptableObject {
  public string Id; // Unique ID - must match prefab name

  // Display values
  public string Name;
  public string Description;
  public Sprite Sprite;

  // Gameplay values
  public float Mass;
  public List<ResourceAmount> BuildCost;
  public List<ResourceAmount> ResearchCost;

  public uint CargoSpace;
  public List<ExternalSlot> ExternalSlots;
}

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


[CreateAssetMenu(menuName = "Ship Thruster")]
public class ShipThruster : ScriptableObject {
  public string Id; // Unique ID - must match prefab name

  // Display values
  public string Name;
  public string Description;
  public Sprite Sprite;

  // Gameplay values
  public SlotSize Size;
  public float Mass;
  public float PowerConsumption;
  public List<ResourceAmount> BuildCost;
  public List<ResourceAmount> ResearchCost;

  public float Force;
  public List<ResourceAmount> PropellantCost;
}
