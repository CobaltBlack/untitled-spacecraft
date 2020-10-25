using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Ship Attachments/Mining")]
public class MiningAttachment : ShipAttachment {
  public float MiningSpeed;
} 

[CreateAssetMenu(menuName = "Ship Attachments/Construction")]
public class ConstructionAttachment : ShipAttachment {
  public float BuildSpeed;
}

[CreateAssetMenu(menuName = "Ship Attachments/SolarPanel")]
public class SolarPanelAttachment : ShipAttachment {
  public float Efficiency;
  public float PanelArea;
}

[CreateAssetMenu(menuName = "Ship Attachments/Towing")]
public class TowingAttachment : ShipAttachment {
  public float TowStrength; // How much mass it can tow
}

[CreateAssetMenu(menuName = "Ship Attachments/Storage")]
public class StorageAttachment : ShipAttachment {
  public float CargoSize; 
  // TODO: Special storage
}
