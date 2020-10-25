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
    public GameObject PlaceablePartPf;

    private InputManager inputManagerInstance;
    private GameObject selectedShipPart;
    private List<BlueprintPartPlaced> placedParts;

    void Awake()
    {
        inputManagerInstance = GetComponent<InputManager>();
        placedParts = new List<BlueprintPartPlaced>();
    }

    void Start()
    {
        
    }

    void Update()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 mousePos2D = new Vector2(mousePos.x, mousePos.y);
        Vector2 mouseGridLocation = GridLocation(mousePos.x, mousePos.y);
        RaycastHit2D hit = Physics2D.Raycast(mousePos2D, Vector2.zero);

        // Track selected ship part to cursor
        if (hit && selectedShipPart)
        {
            this.selectedShipPart.transform.position = mouseGridLocation;
        }

        // LEFT CLICK
        if (Input.GetMouseButtonDown(0))
        {
            if (hit && hit.collider.gameObject.tag == "ShipEditor" && this.selectedShipPart)
            {
                // TODO: part rotation
                PlaceShipPart(this.selectedShipPart.GetComponent<SEPlaceablePart>(), mouseGridLocation, BpPartOrientation.Up);

                // Clear selected part
                this.selectedShipPart = null;
            }
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

    private void PlaceShipPart(SEPlaceablePart placeablePart, Vector2 gridLocation, BpPartOrientation orientation)
    {
        // Create "placed" part
        var partPlaced = new BlueprintPartPlaced();
        partPlaced.PartId = placeablePart.Part.Id;
		partPlaced.X = (int)gridLocation.x;
		partPlaced.Y = (int)gridLocation.y;
		partPlaced.Orientation = orientation;
        placedParts.Add(partPlaced);
    }

    public void SelectShipPart(BlueprintPart bpPart)
    {
        Debug.Log("[SE] Selected part: " + bpPart.Id);

        //DeselectShipPart();
        this.selectedShipPart = Instantiate(PlaceablePartPf);
        var placeablePart = this.selectedShipPart.GetComponent<SEPlaceablePart>();
        placeablePart.Part = bpPart;
    }

    private void DeselectShipPart()
    {
        Destroy(this.selectedShipPart);
    }

    private Vector2 GridLocation(float x, float y)
    {
        return new Vector2(
            Mathf.Round(x / GRID_SIZE) * GRID_SIZE,
            Mathf.Round(y / GRID_SIZE) * GRID_SIZE
        );
    }
}
