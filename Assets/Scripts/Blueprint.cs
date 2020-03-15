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
	public int X;
	public int Y;
	public uint Width;
	public uint Length;
	public BpPartOrientation Orientation;
}
