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
  
  void Start() {
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
    if (!ShipClassById.ContainsKey(id)) {
      Debug.LogError($"Cannot find ShipClass with ID={id}");
    }
    return ShipClassById[id];
  }

  public ShipAttachment GetAttachment(string id) {
    if (!ShipAttachmentById.ContainsKey(id)) {
      Debug.LogError($"Cannot find Attachment with ID={id}");
    }
    return ShipAttachmentById[id];
  }

  public ShipEquipment GetEquipment(string id) {
    if (!ShipEquipmentById.ContainsKey(id)) {
      Debug.LogError($"Cannot find Equipment with ID={id}");
    }
    return ShipEquipmentById[id];
  }

  public ShipThruster GetThruster(string id) {
    if (!ShipThrusterById.ContainsKey(id)) {
      Debug.LogError($"Cannot find Thruster with ID={id}");
    }
    return ShipThrusterById[id];
  }
}
