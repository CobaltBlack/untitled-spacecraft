using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public enum ShipEvent {
  MassChanged,
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

  // Mining
  public uint MiningRate;

  // Component references
  [HideInInspector]
  public CmpCargo CmpCargo;
  [HideInInspector]
  public CmpThruster CmpThruster;
  [HideInInspector]
  public CmpMining CmpMining;

  public ICommand CurrentCommand;
  public Queue<ICommand> CommandQueue = new Queue<ICommand>();

  public BlueprintNew Blueprint { get; set; }

  private bool _isMassStale = true;
  public void SetStaleMass() {
    _isMassStale = true;
  }
  
  private float _mass; // (kg)
  public float Mass {
    get {
      if (_isMassStale) {
        _mass = CalculateShipMass();
        _isMassStale = false;
        BroadcastShipEvent(ShipEvent.MassChanged);
      }
      return _mass;
    }
  }

  private float CalculateShipMass() {
    // Add mass of ship class, components, cargo
    return ShipClass.Mass
      + (CmpThruster ? CmpThruster.Mass : 0)
      + (CmpCargo ? CmpCargo.Mass : 0);
  }


  public delegate void ShipEventCallback(Ship ship);
  public delegate void UnregisterEventCallback(); // is this needed?
  private Dictionary<ShipEvent, HashSet<ShipEventCallback>> EventCallbacks = new Dictionary<ShipEvent, HashSet<ShipEventCallback>>();

  private void BroadcastShipEvent(ShipEvent e) {
    if (!EventCallbacks.ContainsKey(e)) return;
    foreach (var callback in EventCallbacks[e]) {
      callback(this);
    }
  }

  public UnregisterEventCallback RegisterEventCallback(ShipEvent e, ShipEventCallback callback) {
    if (!EventCallbacks.ContainsKey(e)) {
      EventCallbacks.Add(e, new HashSet<ShipEventCallback>());
    }
    HashSet<ShipEventCallback> callbacks = EventCallbacks[e];
    callbacks.Add(callback);
    UnregisterEventCallback unreg = () => {
      callbacks.Remove(callback);
    };
    return unreg;
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
      // Create command and add it to the ship
      // Ship no longer in auto-mode?
      // Set ship as active 
      MoveCommand command = new MoveCommand(this, mapPos);
      CurrentCommand = command;
      CommandQueue.Clear();
      GeneralManager.Instance.SetShipState(this, EntityState.Active);
    } else {
      Debug.Log("Ship has no thrusters!");
    }
  }

  public void ActOnEntity(Entity entity) {
    // Get this ship's available actions from blueprint

    // Get all actions that can act on this entity

    // For now, this ship can only mine

    Debug.Log("ActOnEntity");
    IMineable i = entity as IMineable;
    if (i != null) {
      if (CmpMining) {
        // TODO: Check mining equipment range
        MineCommand command = new MineCommand(this, i);
        CurrentCommand = command;
        CommandQueue.Clear();
        GeneralManager.Instance.SetShipState(this, EntityState.Active);
      } else {
        Debug.Log("Ship has no Mining attachment!");
      }
    }
  }


  public void SimUpdate() {
    // Perform an update on current command
    if (CurrentCommand != null) {
      CurrentCommand.SimUpdate();
    }
  }

  public void SimPostUpdate() {
    // Check if command is done
    if (CurrentCommand != null && CurrentCommand.IsDone()) {
      if (CommandQueue.Count > 0) {
        CurrentCommand = CommandQueue.Dequeue();
      } else {
        Debug.Log("Clears command");
        CurrentCommand = null;
        GeneralManager.Instance.SetShipState(this, EntityState.Idle);
      }
    }

    // Check if any triggers should activate
  }
}
