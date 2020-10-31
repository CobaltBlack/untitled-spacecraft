using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipBuilder : Singleton<ShipBuilder> {
  delegate void AddAttachment(Ship ship, ShipAttachment attachment);
  Dictionary<ShipAttachmentType, AddAttachment> AddAttachmentByType;

  delegate void AddEquipment(Ship ship, ShipEquipment equipment);
  Dictionary<ShipEquipmentType, AddEquipment> AddEquipmentByType;


  void OnEnable() {
    // TODO: Use custom struct to include more data per type
    AddAttachmentByType = new Dictionary<ShipAttachmentType, AddAttachment>() {
      {ShipAttachmentType.Construction,  AddConstructionAttachment},
      {ShipAttachmentType.Mining,  AddMiningAttachment},
      {ShipAttachmentType.Towing,  AddTowingAttachment},
      {ShipAttachmentType.SolarPanel,  AddSolarPanelAttachment},
      {ShipAttachmentType.Storage,  AddStorageAttachment},
    };

    AddEquipmentByType = new Dictionary<ShipEquipmentType, AddEquipment>() {
      {ShipEquipmentType.Engine,  AddEngineEquipment},
      {ShipEquipmentType.Power,  AddPowerEquipment},
      {ShipEquipmentType.Storage,  AddStorageEquipment},
      {ShipEquipmentType.Special,  AddSpecialEquipment},
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
      var thruster = BlueprintManager.Instance.GetThruster(id);
      AddThruster(ship, thruster);
    }

    foreach (string id in bp.ShipEquipmentIds) {
      var equipment = BlueprintManager.Instance.GetEquipment(id);
      AddEquipmentByType[equipment.Type](ship, equipment);
    }

    InitShipComponents(ship);
    FinalizeShip(ship);
  }

  void InitShipComponents(Ship ship) {
    InitCmpThruster(ship);
    //InitCmpCargo(ship);
    //InitCmpMining(ship);
  }

  // Add Attachments
  void AddConstructionAttachment(Ship ship, ShipAttachment attachment) {
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
  }

  void AddSolarPanelAttachment(Ship ship, ShipAttachment attachment) {
  }

  void AddStorageAttachment(Ship ship, ShipAttachment attachment) {
  }

  // Add Equipments
  void AddPowerEquipment(Ship ship, ShipEquipment equipment) {
    //GameObject shipObject = ship.gameObject;
    //CmpPower cmp = shipObject.GetComponent<CmpPower>();
    //if (!cmp) {
    //  cmp = shipObject.AddComponent<CmpPower>() as CmpPower;
    //  ship.CmpMining = cmp;
    //}
    //cmp.Add((PowerEquipment)attachment);
  }
  void AddStorageEquipment(Ship ship, ShipEquipment equipment) { }
  void AddEngineEquipment(Ship ship, ShipEquipment equipment) { }
  void AddSpecialEquipment(Ship ship, ShipEquipment equipment) { }

  // Add Thruster
  void AddThruster(Ship ship, ShipThruster thruster) {
    GameObject shipObject = ship.gameObject;
    CmpThruster cmp = shipObject.GetComponent<CmpThruster>();
    if (!cmp) {
      cmp = shipObject.AddComponent<CmpThruster>() as CmpThruster;
    }
    cmp.Add(thruster);
  }

  void InitCmpThruster(Ship ship) {
    CmpThruster cmp = ship.gameObject.GetComponent<CmpThruster>();
    if (!cmp) {
      return;
    }
    cmp.Init(ship);
  }

  void FinalizeShip(Ship ship) {
    // what to do here...
  }
}
