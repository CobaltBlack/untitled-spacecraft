using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : MonoBehaviour
{
    public delegate void MapAction(Vector2 mapPos);
    public static event MapAction OnMapAction;
}
