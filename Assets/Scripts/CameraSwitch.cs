using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSwitch : MonoBehaviour
{
    public GameObject topViewCam;
    public GameObject firstPersonViewCam;
    bool isFPCamMode;
    public void TogglecamMode()
    {
        if (isFPCamMode)
            SwitchToTopView();
        else
            SwitchToFirstPersonView();
    }
    void SwitchToFirstPersonView()
    {
        //topViewCam.SetActive(false);
        //firstPersonViewCam.SetActive(true);
        isFPCamMode = true;
    }
    void SwitchToTopView()
    {
        //firstPersonViewCam.SetActive(false);
        //topViewCam.SetActive(true);
        isFPCamMode = false;
    }
}
