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

