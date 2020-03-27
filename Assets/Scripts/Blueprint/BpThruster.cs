using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Ship Parts/Thruster")]
public class BpThruster : BlueprintPart
{
    public float Force;  // Total force of thruster
    public float Isp;  // propellant efficiency
}
