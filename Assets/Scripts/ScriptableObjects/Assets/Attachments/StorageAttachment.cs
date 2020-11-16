using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum StorageType {
  Liquid,
  Gas,
  Magnetic,
}

[CreateAssetMenu(menuName = "Ship Attachments/Storage")]
public class StorageAttachment : ShipAttachment {
  public uint CargoSize;
  // TODO: Special storage
}
