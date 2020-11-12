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
