using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Entity : MonoBehaviour
{
    public string Id { get; set; }
    public string Name { get; set; }
    public float Mass { get; set; }
}
