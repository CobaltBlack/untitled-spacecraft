﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseShipComponent : MonoBehaviour
{
	protected Ship Ship;
	public float Mass { get; set; }
}
