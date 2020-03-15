using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

/*
 * Ship Editor UI Controller
 * 
 * Must be placed in same gameObject as ShipEditor.
 * Assign building block gameObjects in the inspector to populate the UI.
 */
public class ShipEditorUIController : MonoBehaviour
{
    [Header("Building blocks")]
    [SerializeField]
    private GameObject[] engines;
    [SerializeField]
    private GameObject[] cargos;

    [Header("Selection")]
    [SerializeField]
    private RectTransform masterPanel;
    [SerializeField]
    private RectTransform subPanelEngines;
    [SerializeField]
    private RectTransform subPanelCargos;

    [Header("Button")]
    [SerializeField]
    private GameObject button;

    private ShipEditor shipEditorInstance;

    void Start()
    {
        shipEditorInstance = GetComponent<ShipEditor>();
        InitializeMasterPanel();
        InitailizeEnginePanel();
        InitailizeCargoPanel();
    }

    void Update()
    {
        
    }

    public void ToggleShipEditor()
    {
        CloseAllSubPanels();
        masterPanel.gameObject.SetActive(!masterPanel.gameObject.activeSelf);
    }

    public void ToggleSubPanelEngines()
    {
        if (subPanelEngines.gameObject.activeSelf)
        {
            subPanelEngines.gameObject.SetActive(false);
        }
        else
        {
            CloseAllSubPanels();
            subPanelEngines.gameObject.SetActive(true);
        }
    }

    public void ToggleSubPanelCargos()
    {
        if (subPanelCargos.gameObject.activeSelf)
        {
            subPanelCargos.gameObject.SetActive(false);
        }
        else
        {
            CloseAllSubPanels();
            subPanelCargos.gameObject.SetActive(true);
        }
    }

    private void InitializeMasterPanel()
    {
        masterPanel.gameObject.SetActive(false);

        GameObject currentButton = Instantiate(button, masterPanel);
        currentButton.GetComponent<Button>().onClick.AddListener(ToggleSubPanelEngines);
        currentButton.GetComponentInChildren<TextMeshProUGUI>().SetText("E");

        currentButton = Instantiate(button, masterPanel);
        currentButton.GetComponent<Button>().onClick.AddListener(ToggleSubPanelCargos);
        currentButton.GetComponentInChildren<TextMeshProUGUI>().SetText("C");
    }

    private void InitailizeEnginePanel()
    {
        subPanelEngines.gameObject.SetActive(false);
        for (int i = 0;  i < engines.Length; i++)
        {
            int index = i;
            GameObject currentButton = Instantiate(button, subPanelEngines);
            currentButton.GetComponent<Button>().onClick.AddListener(delegate { shipEditorInstance.SelectShipPart(engines[index]); });
            currentButton.GetComponentInChildren<TextMeshProUGUI>().SetText("^");
        }
    }

    private void InitailizeCargoPanel()
    {
        subPanelCargos.gameObject.SetActive(false);
        for (int i = 0; i < cargos.Length; i++)
        {
            int index = i;
            GameObject currentButton = Instantiate(button, subPanelCargos);
            currentButton.GetComponent<Button>().onClick.AddListener(delegate { shipEditorInstance.SelectShipPart(cargos[index]); });
            currentButton.GetComponentInChildren<TextMeshProUGUI>().SetText("#");
        }
    }

    public void CloseAllSubPanels()
    {
        this.subPanelEngines.gameObject.SetActive(false);
        this.subPanelCargos.gameObject.SetActive(false);
    }
}
