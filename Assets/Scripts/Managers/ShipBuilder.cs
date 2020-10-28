using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipBuilder : Singleton<ShipBuilder> {
  delegate void AddAttachment(Ship ship, ShipAttachment attachment);
  Dictionary<ShipAttachmentType, AddAttachment> AddAttachmentByType;

  void OnEnable() {
    AddAttachmentByType = new Dictionary<ShipAttachmentType, AddAttachment>() {
      {ShipAttachmentType.Construction,  AddConstructionAttachment},
      {ShipAttachmentType.Mining,  AddMiningAttachment},
      {ShipAttachmentType.Towing,  AddTowingAttachment},
      {ShipAttachmentType.SolarPanel,  AddSolarPanelAttachment},
      {ShipAttachmentType.Storage,  AddStorageAttachment},
    };
  }

  public void InitShipWithBlueprint(Ship ship, BlueprintNew bp) {
    ship.Id = IdGenerator.GenShipId();
    ship.Blueprint = bp;
    ShipClass shipClass = BlueprintManager.Instance.GetShipClass(bp.ShipClassId);
    ship.ShipClass = shipClass;

    // All ships have a cargo component
    CmpCargo cargo = ship.gameObject.AddComponent<CmpCargo>() as CmpCargo;
    cargo.MaxCargoSpace = shipClass.CargoSpace;
    ship.CmpCargo = cargo;

    foreach (string id in bp.ShipAttachmentIds) {
      var attachment = BlueprintManager.Instance.GetAttachment(id);
      AddAttachmentByType[attachment.Type](ship, attachment);
    }

    foreach (string id in bp.ShipThrusterIds) {

    }

    foreach (string id in bp.ShipEquipmentIds) {

    }
  }

  void AddCommonAttachment(Ship ship, ShipAttachment attachment) {
    
  }

  void AddConstructionAttachment(Ship ship, ShipAttachment attachment) {
    AddCommonAttachment(ship, attachment);
  }

  void AddMiningAttachment(Ship ship, ShipAttachment attachment) {
    GameObject shipObject = ship.gameObject;
    CmpMining cmp = shipObject.GetComponent<CmpMining>();
    if (!cmp) {
      cmp = shipObject.AddComponent<CmpMining>() as CmpMining;
      ship.CmpMining = cmp;
    }
    cmp.Add((MiningAttachment)attachment);
  }

  void AddTowingAttachment(Ship ship, ShipAttachment attachment) {
    AddCommonAttachment(ship, attachment);
  }

  void AddSolarPanelAttachment(Ship ship, ShipAttachment attachment) {
    AddCommonAttachment(ship, attachment);
  }

  void AddStorageAttachment(Ship ship, ShipAttachment attachment) {
    AddCommonAttachment(ship, attachment);
  }
}
