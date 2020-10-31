using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCommand : ICommand {
  private Ship _ship;
  private Vector2 _destination;
  private float _stopWithinDistance;

  // stopWithinDistance: Ship will stop moving once within certain distance to destination
  public MoveCommand(Ship ship, Vector2 destination, float stopWithinDistance) {
    this._ship = ship;
    this._destination = destination;
    this._stopWithinDistance = stopWithinDistance;
  }

  public void SimUpdate() {
    // moving code
  }
}
