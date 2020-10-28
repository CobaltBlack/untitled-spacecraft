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
    [Header("Selection")]
    [SerializeField]
    private RectTransform masterPanel;
    [SerializeField]
    private List<GameObject> subPanels;
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
        foreach (var subPanel in subPanels)
        {
            InitSubPanel(subPanel);
        }
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


        foreach (var subPanel in subPanels)
        {
            GameObject currentButton = Instantiate(button, masterPanel);
            currentButton.GetComponent<Button>().onClick.AddListener(() =>
            {
                ToggleSubPanel(subPanel);
            });
            currentButton.GetComponentInChildren<TextMeshProUGUI>().SetText(
                subPanel.GetComponent<SESubPanel>().Name
            );
        }
    }


    private void InitSubPanel(GameObject subPanel)
    {
        subPanel.SetActive(false);
        var subPanelData = subPanel.GetComponent<SESubPanel>();
        var rect = subPanel.GetComponent<RectTransform>();
        //var parts = BlueprintManager.Instance.GetBpPartsListByType(subPanelData.Type);
        //foreach (var part in parts)
        //{
        //    GameObject currentButton = Instantiate(button, rect);
        //    currentButton.GetComponent<Button>().onClick.AddListener(delegate {
        //        shipEditorInstance.SelectShipPart(part);
        //    });
        //    currentButton.GetComponentInChildren<TextMeshProUGUI>().SetText(part.Id);
        //}
    }

    private void ToggleSubPanel(GameObject subPanel)
    {
        if (subPanel.activeSelf)
        {
            subPanel.SetActive(false);
        }
        else
        {
            CloseAllSubPanels();
            subPanel.SetActive(true);
        }
    }

    public void CloseAllSubPanels()
    {
        foreach (var subPanel in this.subPanels)
        {
            subPanel.SetActive(false);
        }
    }
}
