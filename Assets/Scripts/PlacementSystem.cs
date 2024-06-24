using System.Collections.Generic;
using UnityEngine;

public class PlacementSystem : MonoBehaviour
{
    [SerializeField] private Grid grid;
    [SerializeField] private InputManager inputManager;
    [SerializeField] private GameObject cellIndicator;
    [SerializeField] private List<GameObject> ObjectTemplateList = new List<GameObject>();
    [SerializeField] private Transform SpawnParentTransform;
    public enum StructureType { Discreet, Continuos };
    StructureType spawningMode;
    [HideInInspector]public bool canPlace;
    [HideInInspector] public bool placementOverlaping;
    [SerializeField] private GameObject gridVisualization;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip dropSound;
    public static PlacementSystem instance;
    public GameObject PlacementWarning;
    [HideInInspector] public List<GameObject> items = new List<GameObject>();
    private void Awake()
    {
        if (instance == null) { instance = this; }
    }
    void Start()
    {
        inputManager.OnClicked += PlaceStructure;
        inputManager.OnExit += StopPlacement;
        inputManager.OnTab += Changeform;
        placementOverlaping = false;
    }
    public void StartPlacement(int index, StructureType spawningMode)
    {
        StopPlacement();
        this.spawningMode = spawningMode;
        gridVisualization.SetActive(true);
        cellIndicator = ObjectTemplateList[index];
        cellIndicator.SetActive(true);
        canPlace = true;
    }
    void StopPlacement()
    {
        canPlace = false;
        cellIndicator.SetActive(false);
        spawningMode = StructureType.Discreet;
        gridVisualization.SetActive(false);
    }
    void Changeform()
    {
        if (cellIndicator.GetComponentInChildren<ItemState>().type == ItemState.Item.WallType)
        {
            if (cellIndicator.transform.eulerAngles.y == 0)
            {
                cellIndicator.transform.eulerAngles = new Vector3(0, -90, 0);
            }
            else
            {
                cellIndicator.transform.eulerAngles = new Vector3(0, 0, 0);
            }
        }
        else
        {
            cellIndicator.transform.eulerAngles += new Vector3(0, 90, 0);
        }
    }
    private void Update()
    {
        if (canPlace)
        {
            Vector3 mousePosition = inputManager.GetSelectedMapPosition();
            Vector3Int gridPosition = grid.WorldToCell(mousePosition);
            cellIndicator.transform.position = grid.CellToWorld(gridPosition);
        }
    }
    public void PlaceStructure()
    {
        //if (placementOverlaping)
        //{
        //    PlacementWarning.SetActive(true);
        //    return;
        //}
        if (canPlace)
        {
            if (inputManager.IsPointerOverUI())
            {
                return;
            }

            Vector3 mousePosition = inputManager.GetSelectedMapPosition();
            Vector3Int gridPosition = grid.WorldToCell(mousePosition);
            cellIndicator.transform.position = grid.CellToWorld(gridPosition);
            ItemState cursorItemstate = cellIndicator.GetComponentInChildren<ItemState>();
            cursorItemstate.PlacementIndication(true);

            GameObject obj = Instantiate(cellIndicator, SpawnParentTransform);
            obj.transform.position = cellIndicator.transform.position;
            obj.transform.rotation = cellIndicator.transform.rotation;
            obj.transform.localScale = Vector3.one;
            ItemState itemstate = obj.GetComponentInChildren<ItemState>();
            itemstate.ChangeState(ItemState.Status.placed);
            items.Add(obj);
            audioSource.PlayOneShot(dropSound,1);
            if (spawningMode == StructureType.Discreet)
            {
                StopPlacement();
            }
        }
    }
}
