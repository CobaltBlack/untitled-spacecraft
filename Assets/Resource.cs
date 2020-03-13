using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Resource :
    Entity,
    ISelectable,
    IMineable
{
    public uint resourceAvailable;
    public ResourceType type;

    public uint OnMine(uint amount)
    {
        if (resourceAvailable < amount)
        {
            amount = resourceAvailable;
        }
        resourceAvailable -= amount;
        return amount;
    }

    public ResourceType GetResourceType()
    {
        return type;
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
}
