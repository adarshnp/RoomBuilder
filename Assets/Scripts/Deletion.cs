using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deletion : MonoBehaviour
{
    [SerializeField]
    private Camera sceneCamera;
    public InputManager inputManager;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip eraseSound;
    private void Start()
    {
        inputManager.OnRMB += Delete;
    }
    void Delete()
    {
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = sceneCamera.nearClipPlane;
        Ray ray = sceneCamera.ScreenPointToRay(mousePos);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            if (hit.collider.gameObject.tag == "Items")
            {
                if (hit.collider.GetComponentInChildren<ItemState>().itemState == ItemState.Status.placed)
                {
                    PlacementSystem.instance.items.Remove(hit.collider.transform.parent.gameObject);
                    Destroy(hit.collider.transform.parent.gameObject);
                    audioSource.PlayOneShot(eraseSound,1);
                }
            }
        }
    }
}
