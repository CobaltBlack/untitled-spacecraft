using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Ship component for movement using thrusters
public class CmpThruster : BaseShipComponent
{
    public const float PI_180 = Mathf.PI / 180.0f;
    public const float ANGLE_THRESHOLD = 1.0f;
    public const float DISTANCE_THRESHOLD = 1.0f;

    public float MaxSpeed;
    public float Acceleration;
    public float TurnRate;
    public float TotalForce = 0;

    // Movement
    private float distanceToMaxSpeed;
    private Vector3 destination;
    private float currentSpeed;
    private float currentAngle;
    private Vector3 currentVelocity;
    private MovementState movementState;

    void Start()
    {
        MaxSpeed = 10;
        TurnRate = 2;

        destination = new Vector3(transform.position.x, transform.position.y, 0.0f);
        transform.position = destination;
        currentSpeed = 0.0f;
        currentAngle = 0.0f;
        currentVelocity = new Vector3(0.0f, 0.0f, 0.0f);
    }


    // ========================
    // Actual Movement code

    void Update()
    {
        // Position delta
        Vector3 pos_d = destination - transform.position;
        if (pos_d.sqrMagnitude < DISTANCE_THRESHOLD)
        {
            return;
        }

        // Get Angle
        currentAngle = normalizeAngle(transform.eulerAngles.z);
        float targetAngle = getAnnoyingAngle(pos_d);
        float angleDiff = normalizeAngle(targetAngle - currentAngle);
        float absAngleDiff = Mathf.Abs(angleDiff);
        if (absAngleDiff > ANGLE_THRESHOLD)
        {
            if (angleDiff > 0)
            {
                angleDiff = Mathf.Min(angleDiff, TurnRate);
            }
            else
            {
                angleDiff = Mathf.Max(angleDiff, -TurnRate);
            }

            transform.Rotate(Vector3.forward, angleDiff);
        }


        // Accelerate
        float targetDistance = pos_d.magnitude;
        if (currentSpeed >= targetDistance)
        {
            currentSpeed = targetDistance;
        }
        else
        {
            // TODO: Approximate this calculation for performance
            // Counterpoint: premature optimization is something something evil
            float accelFactor = Mathf.Pow((180.0f - absAngleDiff) / 180.0f, 2);
            currentSpeed = Mathf.Min(MaxSpeed, currentSpeed + Acceleration * accelFactor);
        }

        currentVelocity = Vector3FromAngleMagnitude((transform.eulerAngles.z + 90) * PI_180, currentSpeed);
        transform.position += currentVelocity * Time.deltaTime;
    }

    public void setDestination(Vector2 mapPos)
    {
        destination = new Vector3(mapPos.x, mapPos.y, 0.0f);
    }

    Vector3 Vector3FromAngleMagnitude(float angle, float magnitude)
    {
        return new Vector3(Mathf.Cos(angle), Mathf.Sin(angle)) * magnitude;
    }

    float getAnnoyingAngle(Vector2 v)
    {
        float angle = Vector2.Angle(Vector2.up, v);
        if (v.x > 0)
        {
            return angle * -1.0f;
        }
        else
        {
            return angle;
        }
    }

    float normalizeAngle(float angle)
    {
        float outAngle = angle;
        while (outAngle >= 180)
        {
            outAngle -= 360;
        }
        while (outAngle <= -180)
        {
            outAngle += 360;
        }
        return outAngle;
    }

    // ========================
    // Component Initialization

    //public override void AddToShip(Ship ship)
    //{
    //    base.AddToShip(ship);
    //    ship.CmpThruster = this;
    //}

    public void AddThrusterPart(BpThruster thruster)
    {
        TotalForce += thruster.Force;
        Debug.Log("total force = " + TotalForce.ToString());
    }

    // Update stats based on ship mass
    public void UpdateByMass(float mass)
    {
        // Physics 101: F = ma, a = F/m
        Acceleration = TotalForce / mass;

        // Hacky math for ship decceleration
        distanceToMaxSpeed = 0.5f * Acceleration * Mathf.Pow(MaxSpeed / Acceleration, 2);
    }
}
