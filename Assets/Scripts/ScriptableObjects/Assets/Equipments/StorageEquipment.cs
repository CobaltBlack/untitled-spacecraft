using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Ship Equipment/Storage")]
public class StorageEquipment : ShipEquipment {
  public StorageType Type;
  public uint StorageAmount;
}
