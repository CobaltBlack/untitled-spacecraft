using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BpPartType
{
	Cargo,
	Thruster,
	Mining,
	ShipComputer
}

public enum BpPartOrientation
{
	Up,
	Down,
	Left,
	Right
}

// Data only class, created by deserializing blueprint JSON
[System.Serializable]
public class Blueprint
{
	public string Name;
	public List<BlueprintPartPlaced> Parts;
}


// Includes positional values for the BP part
[System.Serializable]
public class BlueprintPartPlaced
{
	public string BlueprintId;
	public int X;
	public int Y;
	public BpPartOrientation Orientation;
}


// Represents each tile/object placed in the blueprint
public class BlueprintPart : ScriptableObject
{
	public string Name;
	public string Id;
	public BpPartType Type;
	public uint Width; // in tiles
	public uint Length; // in tiles
	public float Mass;
	public List<ResourceCost> BuildCost;
	public List<ResourceCost> ResearchCost;
}

[System.Serializable]
public struct ResourceCost
{
	public ResourceType Resource;
	public uint Amount;
}
