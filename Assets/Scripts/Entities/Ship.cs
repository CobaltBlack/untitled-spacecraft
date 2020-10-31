using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

enum ShipState {
  Idle,
  Moving,
}

enum MovementState {
  Idle,
  Accelerating,
  Cruising,
  Deccelerating
}

// Big note: For game logic/state data, entity classes should contain only functions that does READ operations
// Modifications should be done centrally in a Manager Class. 
// This will make bugs more traceable

// Entity can modify its own data as long as it's not part of the sim

// TODO: implement mass system
// Lazy eval
// When components make mass changes, set stale
// When get mass, 
//   if stale: calculate mass
//   return mass
// 

// Old ship class implementation using Interfaces... will use component system instead
public class Ship :
    Entity,
    ISelectable,
    IHasMapAction,
    IHasEntityAction {


  // usually constant variabless
  public ShipClass ShipClass { get; set; }

  // Private
  private ShipState state;

  // Mining
  public uint MiningRate;

  // Component references
  [HideInInspector]
  public CmpCargo CmpCargo;
  [HideInInspector]
  public CmpThruster CmpThruster;
  [HideInInspector]
  public CmpMining CmpMining;

  // Start is called before the first frame update
  void Start() {
    state = ShipState.Idle;
  }

  public BlueprintNew Blueprint { get; set; }

  private bool _isMassStale = true;
  public void SetStaleMass() {
    _isMassStale = true;
  }
  
  private float _mass;
  public float Mass {
    get {
      if (_isMassStale) {
        _mass = CalculateShipMass();
        _isMassStale = false;
      }
      return _mass;
    }
  }


  private float CalculateShipMass() {
    // Add mass of ship class, components, cargo
    return ShipClass.Mass 
      + CmpThruster.Mass;
  }


  public void OnSelect() {
    Debug.Log("Selected: " + this.Id);
    // probably show some bling or some shit here
  }

  public void OnDeselect() {
    Debug.Log("No longer selected: " + this.Id);
  }

  public void ActOnMap(Vector2 mapPos) {
    if (CmpThruster) {
      CmpThruster.setDestination(mapPos);
    }
  }

  public void ActOnEntity(Entity entity) {
    // Get this ship's available actions from blueprint


    // Get all actions that can act on this entity

    // For now, this ship can only mine

    Debug.Log("ActOnEntity");
    IMineable i = entity as IMineable;
    if (i != null) {
      uint mineAmount = MiningRate;
      uint mined = i.OnMine(mineAmount);

      // Add to cargo
      ResourceType resourceType = i.GetResourceType();
      CmpCargo.LoadCargo(new ResourceAmount {
        Type = resourceType,
        Amount = mined
      });
    }
  }


  public void SimUpdate() {
    // Perform an update on current command
    // CurrentTask.SimUpdate()
    // move command
    // mine command
    // transfer command
  }

  public void SimPostUpdate() {
    // Check for triggers
  }
}
