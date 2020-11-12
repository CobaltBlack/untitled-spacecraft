using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Ship Attachments/Mining")]
public class MiningAttachment : ShipAttachment {
  public uint MiningSpeed; // Amount mined per tick
  public float Range;
}
