using UnityEngine;

public interface IMineable {
  uint GetAmountRemaining();
  uint OnMine(uint amount);
  ResourceType GetResourceType();
}
