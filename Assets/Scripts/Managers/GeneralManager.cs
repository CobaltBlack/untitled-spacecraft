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
        GameObject shipObject = Instantiate(ShipPrefab, parent);
        var ship = shipObject.GetComponent<Ship>();
        ship.Blueprint = bp;

        // Load bp parts to add components to ship
        var shipPartsMap = new Dictionary<BpPartType, List<BlueprintPart> >();
		foreach (var partPlaced in bp.Parts)
		{
            Debug.Log(partPlaced.PartId);
            var part = BlueprintManager.Instance.GetBpPartById(partPlaced.PartId);
            if (!shipPartsMap.ContainsKey(part.Type))
            {
                shipPartsMap.Add(part.Type, new List<BlueprintPart>());
            }

            shipPartsMap[part.Type].Add(part);

            // Add common values to ship
            ship.AddBlueprintPart(part);
        }

        foreach (BpPartType bpType in shipPartsMap.Keys)
        {
            switch (bpType)
            {
                case BpPartType.Cargo:
                    AddCargoPartsToShip(ship, shipPartsMap[bpType]);
                    break;
                case BpPartType.Thruster:
                    AddThrusterPartsToShip(ship, shipPartsMap[bpType]);
                    break;
                case BpPartType.Mining:
                    AddMiningPartsToShip(ship, shipPartsMap[bpType]);
                    break;
                default:
                    Debug.Log("Unsupported part type: " + bpType);
                    break;
            }
        }

        // Generate sprite from bp parts
        SpriteRenderer spriteR = shipObject.GetComponent<SpriteRenderer>();
        spriteR.sprite = Resources.Load<Sprite>("Sprites/testShipSpriteResource");

        // Set collider box to same as sprite
        BoxCollider2D collider = shipObject.GetComponent<BoxCollider2D>();
        collider.size = spriteR.bounds.size;
    }


    void AddCargoPartsToShip(Ship ship, List<BlueprintPart> parts)
    {
        GameObject shipObject = ship.gameObject;
        var cmpCargo = shipObject.AddComponent<CmpCargo>() as CmpCargo;
        cmpCargo.AddToShip(ship);
        foreach (BpCargo cargo in parts)
        {
            if (!cargo) continue;
            Debug.Log(cargo.Name);
            cmpCargo.AddCargoPart(cargo);
        }
    }


    void AddThrusterPartsToShip(Ship ship, List<BlueprintPart> parts)
    {
        GameObject shipObject = ship.gameObject;
        var cmpThruster = shipObject.AddComponent<CmpThruster>() as CmpThruster;
        cmpThruster.AddToShip(ship);
        foreach (BpThruster thruster in parts)
        {
            if (thruster)
            {
                Debug.Log(thruster.Name);
                cmpThruster.AddThrusterPart(thruster);
            }
        }
        cmpThruster.UpdateByMass(ship.Mass);
    }


    void AddMiningPartsToShip(Ship ship, List<BlueprintPart> parts)
    {
        GameObject shipObject = ship.gameObject;
        var cmpMining = shipObject.AddComponent<CmpMining>() as CmpMining;
        cmpMining.AddToShip(ship);
        foreach (BpMiningLaser mining in parts)
        {
            if (!mining) continue;
            cmpMining.AddMiningPart(mining);
        }
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
