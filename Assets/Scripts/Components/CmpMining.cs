using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Ship component for all kinds of mining
public class CmpMining : BaseShipComponent
{
    public override void AddToShip(Ship ship)
    {
        base.AddToShip(ship);
        ship.CmpMining = this;
    }

    public void AddMiningPart(BpMiningLaser mining)
    {
        // Calculate mining rate
    }
}
