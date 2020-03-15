using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuiManager : MonoBehaviour
{
    public void OnButtonClick()
    {
        Debug.Log("Button clicked. Nice.");
        GeneralManager.Instance.DebugAddship();
    }
}
