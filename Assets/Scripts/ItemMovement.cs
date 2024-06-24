using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemMovement : MonoBehaviour
{
    Transform item;
    Grid grid;
    bool canMove;
    bool isInContact;
    ItemState itemState;
    public void DenyMovement() { isInContact=true; }
    public void AllowMovement() { isInContact = false; }
    private void Start()
    {
        canMove = false;
        isInContact = false;
        item = transform.parent;
        grid = FindObjectOfType<Grid>();
        itemState = GetComponent<ItemState>();
    }
    private void Update()
    {
        if (canMove)
            Move();
    }
    private void OnMouseDown()
    {
        Debug.Log("selected");
        if (PlacementSystem.instance.canPlace)
            return;
        canMove = true;
        itemState.ChangeState(ItemState.Status.moving);

    }
    private void OnMouseUp()
    {
        if (isInContact)
        {
            Debug.Log("denied");
            PlacementSystem.instance.PlacementWarning.SetActive(true);
        }
        else
        {
            canMove = false;
            itemState.ChangeState(ItemState.Status.placed);
        }
    }
    private void Move()
    {
        Vector3 mousePosition = GetWorldPosition();
        Vector3Int gridPosition = grid.WorldToCell(mousePosition);
        item.transform.position = grid.CellToWorld(gridPosition);
        Debug.Log("item moving : "+ item.transform.position);
    }
    Vector3 GetWorldPosition()
    {
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = Camera.main.nearClipPlane;
        Ray ray = Camera.main.ScreenPointToRay(mousePos);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, Mathf.Infinity, LayerMask.GetMask("Ground")))
        {
            Debug.Log("new position");
            return hit.point;
        }
        return Vector3.zero;
    }
}
