using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class GeneralManager : MonoBehaviour
{
    // singleton boilerplate
    private static GeneralManager _instance;
    public static GeneralManager Instance { get { return _instance; } }
    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }
    }

    public GameObject ShipPrefab;

    private Entity selectedEntity;

    public void SelectEntity(Entity entity)
    {
        ISelectable i = entity as ISelectable;
        if (i != null && entity != selectedEntity)
        { 
            selectedEntity = entity;
            i.OnSelect();
        }
    }

    public void Unselect()
    {
        selectedEntity = null;

        // TODO: close selection interface
    }

    public void ActOnEntity(Entity entity)
    {
        if (selectedEntity == null || selectedEntity == entity)
        {
            return;
        }

        IHasEntityAction i = selectedEntity as IHasEntityAction;
        if (i != null)
        {
            i.ActOnEntity(entity);
        }
    }

    public void ActOnMap(Vector2 worldPos)
    {
        if (selectedEntity == null)
        {
            return;
        }

        (selectedEntity as IHasMapAction)?.ActOnMap(worldPos);
    }


    public void InstantiateShip(Vector2 position, Blueprint bp)
    {
        // Instantiate the ship in game

        // Load bp parts to add components to ship
        foreach (BlueprintPart part in bp.Parts)
        {

        }

        // Generate sprite

        // Set collider box to same as sprite

        // Add relevant components

        // Calculate ship properties

    }


    // DEBUG CODE BELOW
    public void DebugAddship()
    {
        InstantiateShip(new Vector3(0,0,0), DebugCreateBlueprint());
    }

    public Blueprint DebugCreateBlueprint()
    {
        Blueprint bp = new Blueprint();
        bp.Name = "Experimental Spacecraft";
        //bp.Parts.Add(new Part);
        return bp;
    }
}
