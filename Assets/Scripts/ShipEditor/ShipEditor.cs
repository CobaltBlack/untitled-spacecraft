using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Ship Editor
 */
public class ShipEditor : MonoBehaviour
{
    [SerializeField]
    private int GRID_SIZE = 2;

    private InputManager inputManagerInstance;
    private GameObject selectedShipPart;

    void Awake()
    {
        inputManagerInstance = GetComponent<InputManager>();
    }

    void Start()
    {
        
    }

    void Update()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 mousePos2D = new Vector2(mousePos.x, mousePos.y);
        RaycastHit2D hit = Physics2D.Raycast(mousePos2D, Vector2.zero);

        // Track selected ship part to cursor
        if (hit && selectedShipPart)
        {
            Vector2 gridLocation = new Vector2(Mathf.Round(hit.point.x / GRID_SIZE) * GRID_SIZE, Mathf.Round(hit.point.y / GRID_SIZE) * GRID_SIZE);
            this.selectedShipPart.transform.position = gridLocation;
        }
        // LEFT CLICK
        if (Input.GetMouseButtonDown(0) && hit && hit.collider.gameObject.tag == "ShipEditor")
        {
            PlaceShipPart();
        }
        // RIGHT CLICK
        else if (Input.GetMouseButtonDown(1))
        {
            DeselectShipPart();
        }
        // MIDDLE CLICK
        else if (Input.GetMouseButtonDown(2))
        {

        }
    }

    private void PlaceShipPart()
    {
        this.selectedShipPart = null;
    }

    public void SelectShipPart(GameObject shipPart)
    {
        DeselectShipPart();
        this.selectedShipPart = Instantiate(shipPart);
        this.selectedShipPart.gameObject.SetActive(true);
    }

    private void DeselectShipPart()
    {
        Destroy(this.selectedShipPart);
    }
}
