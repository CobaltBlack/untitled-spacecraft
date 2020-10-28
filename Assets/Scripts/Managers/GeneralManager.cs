using System.Collections;
using System.Collections.Generic;
using System.Net.Mail;
using UnityEngine;
using UnityEngine.EventSystems;

public class GeneralManager : MonoBehaviour {
  // singleton boilerplate
  private static GeneralManager _instance;
  public static GeneralManager Instance { get { return _instance; } }
  private void Awake() {
    if (_instance != null && _instance != this) {
      Destroy(this.gameObject);
    } else {
      _instance = this;
    }
  }

  private Entity selectedEntity;

  public void SelectEntity(Entity entity) {
    ISelectable i = entity as ISelectable;
    if (i != null && entity != selectedEntity) {
      selectedEntity = entity;
      i.OnSelect();
    }
  }

  public void Unselect() {
    selectedEntity = null;

    // TODO: close selection interface
  }

  public void ActOnEntity(Entity entity) {
    if (selectedEntity == null || selectedEntity == entity) {
      return;
    }

    IHasEntityAction i = selectedEntity as IHasEntityAction;
    if (i != null) {
      i.ActOnEntity(entity);
    }
  }

  public void ActOnMap(Vector2 worldPos) {
    if (selectedEntity == null) {
      return;
    }

      (selectedEntity as IHasMapAction)?.ActOnMap(worldPos);
  }

  private Dictionary<string, Ship> AllShips;

  // parent represents the thing that spawns this ship (eg. the ship assembler)
  public void InstantiateShip(Transform parent, BlueprintNew bp) {
    // Instantiate the ship in game
    GameObject shipObject = Instantiate(ShipPrefab, parent);
    var ship = shipObject.GetComponent<Ship>();

    ShipBuilder.InitShipWithBlueprint(ship, bp);

    // All ships register to central manager
    AllShips.Add(ship.Id, ship);

    // TODO: Generate sprite from bp parts
    SpriteRenderer spriteR = shipObject.GetComponent<SpriteRenderer>();
    spriteR.sprite = Resources.Load<Sprite>("Sprites/testShipSpriteResource");

    // Set collider box to same as sprite
    BoxCollider2D collider = shipObject.GetComponent<BoxCollider2D>();
    collider.size = spriteR.bounds.size;
  }


  // DEBUG CODE BELOW

  public GameObject ShipPrefab;
  public GameObject DebugShipAssembler;
  public Blueprint DebugBlueprint;
  public BlueprintNew DebugBlueprintNew;

  public void DebugAddship() {
    BlueprintNew bp = BlueprintUtils.ReadBlueprintFile(
      Application.persistentDataPath + "/" + BlueprintUtils.FormatBlueprintFilename(DebugBlueprintNew)
    );
    InstantiateShip(DebugShipAssembler.transform, bp);
  }

  public string DebugSaveBlueprint() {
    return BlueprintUtils.SaveBlueprint(DebugBlueprintNew);
  }
}
