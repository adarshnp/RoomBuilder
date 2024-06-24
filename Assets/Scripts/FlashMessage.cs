using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashMessage : MonoBehaviour
{
    [SerializeField] private float duration; 
    void Start()
    {
        StartCoroutine(Flash());
    }
    IEnumerator Flash()
    {
        yield return new WaitForSecondsRealtime(duration);
        gameObject.SetActive(false);
    }
}
