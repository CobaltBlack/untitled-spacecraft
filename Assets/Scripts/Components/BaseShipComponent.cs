using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseShipComponent : MonoBehaviour
{
	protected Ship ship;

	public virtual void AddToShip(Ship ship)
	{
		this.ship = ship;
	}
}
