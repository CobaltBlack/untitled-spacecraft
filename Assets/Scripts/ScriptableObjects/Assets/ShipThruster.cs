using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

  public float Force; // (Newtons)
  public List<ResourceAmount> PropellantCost;
}
