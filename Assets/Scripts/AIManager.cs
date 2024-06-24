using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIManager : MonoBehaviour
{
    [SerializeField] private GameObject npcParent;
    [SerializeField] private Transform npc;
    bool isLive = false;
    public void CatButtonClickAction()
    {
        if (isLive) 
        {
            TurnOffNPC();
        }
        else 
        { 
            TurnOnNPC(); 
        }
    }
    private void TurnOnNPC()
    {
        npcParent.SetActive(true);
        npc.position = Vector3.zero;
        npc.eulerAngles = Vector3.up * 10;
        isLive = true;
    }
    private void TurnOffNPC()
    {
        npcParent.SetActive(false);
        isLive = false;
    }
}
