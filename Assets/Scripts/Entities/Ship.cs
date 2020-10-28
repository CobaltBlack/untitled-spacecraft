using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

enum ShipState {
  Idle,
  Moving
}

enum MovementState {
  Idle,
  Accelerating,
  Cruising,
  Deccelerating
}

// Big note: Entity classes should contain only functions that does READ operations
// Modifications should be done centrally in a Manager Class. 
// This will make bugs more traceable

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


  // Set ship's common values
  public void AddBlueprintPart(BlueprintPart bpPart) {
    Mass += bpPart.Mass;
    Debug.Log("Mass is now: " + Mass);
  }



  public void OnSelect() {
    Debug.Log(gameObject.name + ": I am selected");
    // probably show some bling or some shit here
  }

  public void OnDeselect() {
    Debug.Log(gameObject.name + ": No longer selected :(");
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
}
