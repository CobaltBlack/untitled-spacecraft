using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdGenerator : MonoBehaviour
{
  private static uint LastId;

  public static string GenShipId() {
    LastId += 1;
    return LastId.ToString();
  }
}
