using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCommand : ICommand {
  const float PI_180 = Mathf.PI / 180.0f;
  const float ANGLE_THRESHOLD = 1.0f;

  private Ship _ship;
  private CmpThruster _thruster;
  
  private Vector3 _destination;
  private float _distanceThreshold;
  private float _sqrDistanceThreshold;
  private bool _isCorrectAngle = false;
  private bool _isClose = false;
  private bool _isDone = false;
  private float _decelerationValue;

  // distanceThreshold: Ship will stop moving once within certain distance to destination
  public MoveCommand(Ship ship, Vector2 destination, float distanceThreshold = 0.1f) {
    this._ship = ship;
    this._thruster = ship.CmpThruster;
    this._destination = new Vector3(destination.x, destination.y, 0.0f);
    this._distanceThreshold = distanceThreshold;
    this._sqrDistanceThreshold = Mathf.Pow(distanceThreshold, 2);
  }

  ~MoveCommand() {
    Debug.Log("Move destructor");
  }

  // Calculate ship movement
  public void SimUpdate() {
    var transform = _ship.transform;

    // Position delta
    Vector3 pos_d = _destination - transform.position;
    if (pos_d.sqrMagnitude <= _sqrDistanceThreshold && _thruster.currentSpeed < 0.1f) {
      _isDone = true;
      return;
    }

    // Get Angle
    // TODO: Can be optimzed to use less code here
    float currentAngle = normalizeAngle(transform.eulerAngles.z);
    float targetAngle = getAnnoyingAngle(pos_d);
    float angleDiff = normalizeAngle(targetAngle - currentAngle);
    float absAngleDiff = Mathf.Abs(angleDiff);
    if (absAngleDiff > ANGLE_THRESHOLD) {
      if (angleDiff > 0) {
        angleDiff = Mathf.Min(angleDiff, _thruster.TurnRate);
      } else {
        angleDiff = Mathf.Max(angleDiff, -(_thruster.TurnRate));
      }
      transform.Rotate(Vector3.forward, angleDiff);
    } else {
      this._isCorrectAngle = true;
    }

    // Stopping distance is how much further ship will travel if we only decel
    float stoppingDistance = stoppingDistanceFormula(_thruster.currentSpeed, _thruster.Acceleration);
    float targetDistance = pos_d.magnitude - _distanceThreshold;
    if (targetDistance < stoppingDistance) {
      this._isClose = true;
    }

    if (this._isClose || !this._isCorrectAngle) {
      _thruster.currentSpeed = Mathf.Max(0, _thruster.currentSpeed - _thruster.Acceleration * Constants.FrameTime);
    } else {
      // Accelerate
      _thruster.currentSpeed = Mathf.Min(_thruster.MaxSpeed, _thruster.currentSpeed + _thruster.Acceleration * Constants.FrameTime);
    }
    var currentVelocity = Vector3FromAngleMagnitude((transform.eulerAngles.z + 90) * PI_180, _thruster.currentSpeed);
    transform.position += currentVelocity * Constants.FrameTime;
  } // End SimUpdate


  public bool IsDone() {
    return _isDone;
  }


  Vector3 Vector3FromAngleMagnitude(float angle, float magnitude) {
    return new Vector3(Mathf.Cos(angle), Mathf.Sin(angle)) * magnitude;
  }

  float getAnnoyingAngle(Vector2 v) {
    float angle = Vector2.Angle(Vector2.up, v);
    if (v.x > 0) {
      return angle * -1.0f;
    } else {
      return angle;
    }
  }

  float normalizeAngle(float angle) {
    float outAngle = angle;
    while (outAngle >= 180) {
      outAngle -= 360;
    }
    while (outAngle < -180) {
      outAngle += 360;
    }
    return outAngle;
  }

  float stoppingDistanceFormula(float speed, float accel) {
    // somehow 0.5 is the correct constant here
    return (0.5f * speed * speed) / accel;
  }
}
