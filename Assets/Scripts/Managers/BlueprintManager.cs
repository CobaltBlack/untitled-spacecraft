using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

public class BlueprintManager : Singleton<BlueprintManager> {
  private static Dictionary<string, BlueprintPart> BpPartMap = new Dictionary<string, BlueprintPart>();
  private static Dictionary<BpPartType, List<BlueprintPart>> BpPartsByType = new Dictionary<BpPartType, List<BlueprintPart>>();

  private static Dictionary<string, ShipClass> ShipClassById = new Dictionary<string, ShipClass>();
  private static Dictionary<string, ShipAttachment> ShipAttachmentById = new Dictionary<string, ShipAttachment>();
  private static Dictionary<string, ShipEquipment> ShipEquipmentById = new Dictionary<string, ShipEquipment>();
  private static Dictionary<string, ShipThruster> ShipThrusterById = new Dictionary<string, ShipThruster>();
  
  void OnEnable() {
    InitializeBlueprintParts();
  }

  void InitializeBlueprintParts() {
    var shipClasses = Resources.LoadAll("BlueprintPartsNew/ShipClasses", typeof(ShipClass));
    foreach (ShipClass shipClass in shipClasses) {
      ShipClassById.Add(shipClass.Id, shipClass);
    }

    var attachments = Resources.LoadAll("BlueprintPartsNew/Attachments", typeof(ShipAttachment));
    foreach (ShipAttachment attachment in attachments) {
      ShipAttachmentById.Add(attachment.Id, attachment);
    }

    var equipments = Resources.LoadAll("BlueprintPartsNew/Equipments", typeof(ShipEquipment));
    foreach (ShipEquipment equipment in equipments) {
      ShipEquipmentById.Add(equipment.Id, equipment);
    }

    var thrusters = Resources.LoadAll("BlueprintPartsNew/Thrusters", typeof(ShipThruster));
    foreach (ShipThruster thruster in thrusters) {
      ShipThrusterById.Add(thruster.Id, thruster);
    }
  }

  public ShipClass GetShipClass(string id) {
    return ShipClassById.ContainsKey(id) ? ShipClassById[id] : null;
  }

  public ShipAttachment GetAttachment(string id) {
    return ShipAttachmentById.ContainsKey(id) ? ShipAttachmentById[id] : null;
  }

  public ShipEquipment GetEquipment(string id) {
    return ShipEquipmentById.ContainsKey(id) ? ShipEquipmentById[id] : null;
  }

  public ShipThruster GetThruster(string id) {
    return ShipThrusterById.ContainsKey(id) ? ShipThrusterById[id] : null;
  }
}
