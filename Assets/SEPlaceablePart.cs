using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SEPlaceablePart : MonoBehaviour
{
    private BlueprintPart _part;
    public BlueprintPart Part 
    {
        get { return _part; }
        set
        {
            this._part = value;
            this.gameObject.GetComponent<SpriteRenderer>().sprite = value.Sprite;
        }
    }
}
