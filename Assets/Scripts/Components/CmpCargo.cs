using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

// NOTE: Structures may also have cargo
// This component needs to be generic enough

// Ship component for carrying cargos of resources
public class CmpCargo : BaseShipComponent {
  // Inventory
  public uint MaxSpace { get; private set; }
  public uint EmptySpace { get; private set; }
  public bool IsFull { get { return EmptySpace == 0; } }
  private Dictionary<ResourceType, uint> cargo = new Dictionary<ResourceType, uint>();

  private bool _isMassStale = true;
  private float _mass; // (kg)
  public new float Mass {
    get {
      if (_isMassStale) {
        _mass = CalculateCargoMass();
        _isMassStale = false;
      }
      return _mass;
    }
  }

  void SetStaleMass() {
    this._isMassStale = true;
    this.Ship.SetStaleMass();
  }

  float CalculateCargoMass() {
    float total = 0.0f;
    foreach (var i in cargo) {
      total += ResourceManager.Instance.GetResource(i.Key).Mass * i.Value; 
    }
      
    return total;
  }

  // Returns amount loaded
  public uint LoadCargo(ResourceAmount ra) {
    uint amountToLoad = ra.Amount > EmptySpace ? EmptySpace : ra.Amount;
    if (amountToLoad == 0) {
      return 0;
    }

    if (!cargo.ContainsKey(ra.Type)) {
      cargo.Add(ra.Type, 0);
    }
    cargo[ra.Type] += amountToLoad;
    EmptySpace -= amountToLoad;
    SetStaleMass();

    Assert.IsTrue(EmptySpace >= 0);
    Debug.Log(DebugUtils.ToDebugString<ResourceType, uint>(cargo));

    return amountToLoad;
  }

  // Returns amount unloaded
  public uint UnloadCargo(ResourceAmount ra) {
    if (!cargo.ContainsKey(ra.Type)) {
      return 0;
    }

    uint amountToUnload = ra.Amount > cargo[ra.Type] ? cargo[ra.Type] : ra.Amount;
    cargo[ra.Type] -= amountToUnload;
    EmptySpace += amountToUnload;
    SetStaleMass();
    Assert.IsTrue(EmptySpace <= MaxSpace);
    return amountToUnload;
  }

  // ========================
  // Component Initialization
  public void Init(Ship ship) {
    this.Ship = ship;
    ship.CmpCargo = this;

    this.MaxSpace = ship.ShipClass.CargoSpace;
    this.EmptySpace = this.MaxSpace;

    // TODO: Check ship equipment cargo usage
    //EmptySpace = 
  }
}
