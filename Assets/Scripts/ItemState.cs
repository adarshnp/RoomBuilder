using System.Collections.Generic;
using UnityEngine;

public class ItemState : MonoBehaviour
{
    public enum Item { WallType, ItemType }
    public Item type;
    public enum Status { cursor, placed,moving }
    public Status itemState;
    public List<Renderer> renderers = new List<Renderer>();
    private Color normalColor;
    private void Start()
    {
        normalColor = renderers[0].material.color;
    }
    public void ChangeState(Status status)
    {
        itemState = status;
    }
    public void PlacementIndication(bool canPlace)
    {
        //change color to red
        foreach (Renderer rend in renderers)
        {
            var materials = rend.materials;
            foreach (Material mat in materials)
            {
                if (canPlace)
                {
                    mat.color = normalColor;
                    //if (PlacementSystem.instance.canPlace)
                    //{
                    //    PlacementSystem.instance.placementOverlaping = false;
                    //}
                }
                else
                {
                    mat.color = Color.red;
                    //if (PlacementSystem.instance.canPlace)
                    //{
                    //    PlacementSystem.instance.placementOverlaping = true;
                    //}
                }
            }
            rend.materials = materials;
        }
    }
    private void OnTriggerEnter(Collider collider)
    {
        if (collider.tag == "Items")
        {
            ItemState HitItemState = collider.gameObject.GetComponentInChildren<ItemState>();
            if (itemState == Status.cursor && HitItemState.itemState == Status.placed)
            {
                if ((HitItemState.type == Item.WallType && type == Item.WallType))
                {
                    if (transform.parent.position == collider.transform.parent.position)
                        PlacementIndication(false);
                }
                else
                {
                    PlacementIndication(false);
                }
            }
            if (itemState == Status.moving && HitItemState.itemState == Status.placed)
            {
                GetComponent<ItemMovement>().DenyMovement();
            }
        }
    }
    private void OnTriggerExit(Collider collider)
    {
        if (collider.tag == "Items")
        {
            ItemState HitItemState = collider.gameObject.GetComponentInChildren<ItemState>();
            if (itemState == Status.cursor && HitItemState.itemState == Status.placed)
            {
                PlacementIndication(true);
            }
            if (itemState == Status.moving && HitItemState.itemState == Status.placed)
            {
                GetComponent<ItemMovement>().AllowMovement();
            }
        }
    }
}
