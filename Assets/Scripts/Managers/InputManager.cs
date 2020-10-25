using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InputManager : MonoBehaviour {
  // Update is called once per frame
  void Update() {
    Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    Vector2 mousePos2D = new Vector2(mousePos.x, mousePos.y);
    RaycastHit2D hit = Physics2D.Raycast(mousePos2D, Vector2.zero);
    if (hit.collider == null) {
      return;
    }

    Entity e = hit.collider.GetComponent<Entity>();
    if (Input.GetMouseButtonDown(0)) {
      // LEFT CLICK
      if (e) {
        GeneralManager.Instance.SelectEntity(e);
      } else {
        GeneralManager.Instance.Unselect();
      }
    } else if (Input.GetMouseButtonDown(1)) {
      // RIGHT CLICK
      if (e) {
        Debug.Log("Hit entity: " + e.gameObject.name);
        GeneralManager.Instance.ActOnEntity(e);
      } else {
        GeneralManager.Instance.ActOnMap(mousePos2D);
      }
    } else if (Input.GetMouseButtonDown(2)) {
      // MIDDLE CLICK
    } else {
      // HOVER
    }
  }
}
