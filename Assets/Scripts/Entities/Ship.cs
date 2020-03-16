using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

enum ShipState
{
    Idle,
    Moving
}

enum MovementState
{
    Idle,
    Accelerating,
    Cruising,
    Deccelerating
}


// Old ship class implementation using Interfaces... will use component system instead
public class Ship :
    Entity,
    ISelectable,
    IHasMapAction,
    IHasEntityAction
{
    public const float PI_180 = Mathf.PI / 180.0f;
    public const float ANGLE_THRESHOLD = 0.1f;

    // usually constant variabless
    public Blueprint Blueprint;
    public float MaxSpeed;
    public float Acceleration;
    public float TurnRate;

    // Private
    private ShipState state;

    // Movement
    private float distanceToMaxSpeed;
    private Vector3 destination;
    private float currentSpeed;
    private float currentAngle;
    private Vector3 currentVelocity;
    private MovementState movementState;

    // Mining
    public uint MiningRate;

    // Inventory
    public uint MaxCargoSpace;
    public uint EmptyCargoSpace;
    private Dictionary<ResourceType, uint> cargo;

    // Start is called before the first frame update
    void Start()
    {
        // Load stats from ship blueprint
        state = ShipState.Idle;
        destination = transform.position;
        currentSpeed = 0.0f;
        currentAngle = 0.0f;

        currentVelocity = new Vector3(0.0f, 0.0f, 0.0f);

        distanceToMaxSpeed = 0.5f * Acceleration * Mathf.Pow(MaxSpeed / Acceleration, 2);

        EmptyCargoSpace = MaxCargoSpace;
        cargo = new Dictionary<ResourceType, uint>();
    }

    // Update is called once per frame
    void Update()
    {
        UpdatePosition();
    }


    void UpdatePosition()
    {
        // Position delta
        Vector3 pos_d = destination - transform.position;
        if (pos_d.sqrMagnitude < 0.1)
        {
            return;
        }

        float targetDistance = pos_d.magnitude;

        // Accelerate
        currentSpeed = Mathf.Min(MaxSpeed, currentSpeed + Acceleration);
        if (currentSpeed > targetDistance)
        {
            currentSpeed = targetDistance;
        }

        // Get Angle
        currentAngle = transform.eulerAngles.z;
        float targetAngle = getAnnoyingAngle(pos_d);
        float angleDiff = targetAngle - currentAngle;
        if (Mathf.Abs(angleDiff) > ANGLE_THRESHOLD)
        {
            if (angleDiff > 180)
            {
                angleDiff -= 360;
            }
            else if (angleDiff < -180)
            {
                angleDiff += 360;
            }

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

        currentVelocity = Vector3FromAngleMagnitude((transform.eulerAngles.z + 90) * PI_180, currentSpeed);
        transform.position += currentVelocity * Time.deltaTime;
    }


    void setDestination(Vector2 mapPos)
    {
        Debug.Log("setDestination: " + mapPos);
        destination = new Vector3(mapPos.x, mapPos.y);
    }

    public void OnSelect()
    {
        Debug.Log(gameObject.name + ": I am selected");
        // probably show some bling or some shit here
    }

    public void OnDeselect()
    {
        Debug.Log(gameObject.name + ": No longer selected :(");
    }

    public void ActOnMap(Vector2 mapPos)
    {
        setDestination(mapPos);
    }

    public void ActOnEntity(Entity entity)
    {
        // Get this ship's available actions from blueprint


        // Get all actions that can act on this entity

        // For now, this ship can only mine

        Debug.Log("ActOnEntity");
        IMineable i = entity as IMineable;
        if (i != null)
        {
            uint mineAmount = (EmptyCargoSpace < MiningRate) ? EmptyCargoSpace : MiningRate;
            uint mined = i.OnMine(mineAmount);
            Debug.Log("mineAmount=" + mineAmount);
            Debug.Log("mined=" + mined);

            // Add to cargo
            ResourceType resourceType = i.GetResourceType();
            if (!cargo.ContainsKey(resourceType))
            {
                cargo.Add(resourceType, (uint)0);
            }
            cargo[resourceType] += mined;
            EmptyCargoSpace -= mined;
            Debug.Log("cargo");
            Debug.Log(DebugUtils.ToDebugString<ResourceType, uint>(cargo));
        }
    }

    Vector3 Vector3FromAngleMagnitude(float angle, float magnitude)
    {
        return new Vector3(Mathf.Cos(angle), Mathf.Sin(angle)) * magnitude;
    }

    private float getAnnoyingAngle(Vector2 v)
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
}
