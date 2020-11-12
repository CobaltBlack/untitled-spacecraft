using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
