using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum StorageType {
  Liquid,
  Gas,
  Magnetic,
}

[CreateAssetMenu(menuName = "Ship Equipment/Power")]
public class PowerEquipment : ShipEquipment {
  public float PowerGeneration;
}

[CreateAssetMenu(menuName = "Ship Equipment/Storage")]
public class StorageEquipment : ShipEquipment {
  public StorageType Type;
  public uint StorageAmount;
} 

[CreateAssetMenu(menuName = "Ship Equipment/Special")]
public class SpecialEquipment : ShipEquipment {
  public string SpecialEffectName;
}
