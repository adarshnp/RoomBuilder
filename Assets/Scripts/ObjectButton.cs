using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectButton : MonoBehaviour
{
    public int objectIndex;
    public PlacementSystem.StructureType spawningMode;
    public PlacementSystem placementSystem;
    public void OnClick()
    {
        placementSystem.StartPlacement(objectIndex,spawningMode);
    }
}
