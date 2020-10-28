using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

// NOTE: Structures may also have cargo
// This component needs to be generic enough

// Ship component for carrying cargos of resources
public class CmpCargo : BaseShipComponent
{
    // Inventory
    public uint MaxCargoSpace;
    public uint EmptyCargoSpace;
    private Dictionary<ResourceType, uint> cargo;

    void Start()
    {
        cargo = new Dictionary<ResourceType, uint>();
    }

    //public override void AddToShip(Ship ship)
    //{
    //    base.AddToShip(ship);
    //    ship.CmpCargo = this;
    //}

    public void AddCargoPart(BpCargo cargo)
    {
        MaxCargoSpace += cargo.MaxCargoSpace;
        EmptyCargoSpace = MaxCargoSpace;
    }

    // Returns amount loaded
    public uint LoadCargo(ResourceAmount amount)
    {
        uint amountToLoad = amount.Amount > EmptyCargoSpace ? EmptyCargoSpace : amount.Amount;

        if (!cargo.ContainsKey(amount.Type))
        {
            cargo.Add(amount.Type, 0);
        }
        cargo[amount.Type] += amountToLoad;
        EmptyCargoSpace -= amountToLoad;
        Assert.IsTrue(EmptyCargoSpace <= MaxCargoSpace);

        // Update mass

        Debug.Log(DebugUtils.ToDebugString<ResourceType, uint>(cargo));

        return amountToLoad;
    }

    // Returns amount unloaded
    public uint UnloadCargo(ResourceAmount amount)
    {
        if (!cargo.ContainsKey(amount.Type))
        {
            return 0;
        }

        uint amountToUnload = amount.Amount > cargo[amount.Type] ? cargo[amount.Type] : amount.Amount;
        cargo[amount.Type] -= amountToUnload;
        EmptyCargoSpace += amountToUnload;
        Assert.IsTrue(EmptyCargoSpace <= MaxCargoSpace);

        // Update mass


        return amountToUnload;
    }
}
