using Microsoft.Win32.SafeHandles;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Ship component for movement using thrusters
public class CmpThruster : BaseShipComponent {
  public const float PI_180 = Mathf.PI / 180.0f;
  public const float ANGLE_THRESHOLD = 1.0f;
  public const float DISTANCE_THRESHOLD = 1.0f;

  public float MaxSpeed;
  public float Acceleration;
  public float TurnRate;
  public float TotalForce = 0;

  public float currentSpeed { get; set; }

  // Movement
  private Vector3 destination;
  private float currentAngle;
  private Vector3 currentVelocity;

  void Start() {
    // TODO: initial values
    MaxSpeed = 10;
    TurnRate = 2;

    destination = new Vector3(transform.position.x, transform.position.y, 0.0f);
    transform.position = destination;
    currentSpeed = 0.0f;
    currentAngle = 0.0f;
    currentVelocity = new Vector3(0.0f, 0.0f, 0.0f);
  }

  // ========================
  // Component Initialization

  //public override void AddToShip(Ship ship)
  //{
  //    base.AddToShip(ship);
  //    ship.CmpThruster = this;
  //}
  public List<ShipThruster> Thrusters = new List<ShipThruster>();
  public void Add(ShipThruster thruster) {
    Thrusters.Add(thruster);
  }

  public void Init(Ship ship) {
    this.Ship = ship;
    ship.CmpThruster = this;
    this.Mass = 0;
    this.TotalForce = 0;

    foreach (ShipThruster thruster in Thrusters) {
      this.Mass += thruster.Mass;
      this.TotalForce += thruster.Force;
    }

    this.Ship.SetStaleMass();

    this.Ship.RegisterEventCallback(ShipEvent.MassChanged, OnShipMassChanged);
    OnShipMassChanged(ship);
  }

  void OnShipMassChanged(Ship ship) {
    // Physics 101: F = ma, a = F/m
    this.Acceleration = this.TotalForce / ship.Mass;
  }

  // Update stats based on ship mass
  public void UpdateByMass(float mass) {
    // Physics 101: F = ma, a = F/m
    Acceleration = TotalForce / mass;
  }
}
