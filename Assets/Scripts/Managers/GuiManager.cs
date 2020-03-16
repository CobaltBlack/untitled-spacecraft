using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuiManager : MonoBehaviour
{
    public void OnAddShip()
    {
        GeneralManager.Instance.DebugAddship();
    }

    public void OnSaveBp()
    {
        GeneralManager.Instance.DebugSaveBlueprint();
    }
}
