using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
	public List<BlueprintPart> Parts;
}

// Represents each tile/object placed in the blueprint
[System.Serializable]
public class BlueprintPart
{
	// Constant values
	public string Name;
	public uint Width; // in tiles
	public uint Length; // in tiles
	public Dictionary<ResourceType, uint> Cost;
	public float Mass;

	// Positional values... should be split into editor object?
	public int X;
	public int Y;
	public BpPartOrientation Orientation;
}
