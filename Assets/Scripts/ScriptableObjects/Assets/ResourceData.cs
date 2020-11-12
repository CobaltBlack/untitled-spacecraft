using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Resource")]
public class ResourceData : ScriptableObject {
  public ResourceType Type;
  public string Name;
  public float Mass; // Per unit
  public Sprite Icon;
  public uint MiningDifficulty; // Drill must fill this value to mine 1 unit of resource
}
