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

    // parent represents the thing that spawns this ship (eg. the ship assembler)
    public void InstantiateShip(Transform parent, Blueprint bp)
    {
        // Instantiate the ship in game
        GameObject shipInGame = Instantiate(ShipPrefab, parent);
        var ship = shipInGame.GetComponent<Ship>();
        ship.Blueprint = bp;

        // Load bp parts to add components to ship

        //foreach (BlueprintPart part in bp.Parts)
        //{
        //          Debug.Log(part.Name);
        //}

        // Generate sprite
        SpriteRenderer spriteR = shipInGame.GetComponent<SpriteRenderer>();
        spriteR.sprite = Resources.Load<Sprite>("Sprites/testShipSpriteResource");

        // Set collider box to same as sprite
        BoxCollider2D collider = shipInGame.GetComponent<BoxCollider2D>();
        collider.size = spriteR.bounds.size;

        // Add relevant components

        // Calculate ship properties

    }


    // DEBUG CODE BELOW

    public GameObject ShipPrefab;
    public GameObject DebugShipAssembler;
    public Blueprint DebugBlueprint;

    public void DebugAddship()
    {
        Blueprint bp = BlueprintUtils.ReadBlueprintFile(Application.persistentDataPath + "/" + BlueprintUtils.FormatBlueprintFilename(DebugBlueprint));
        InstantiateShip(DebugShipAssembler.transform, bp);
    }

    public string DebugSaveBlueprint()
    {
        return BlueprintUtils.SaveBlueprint(DebugBlueprint);
    }
}
