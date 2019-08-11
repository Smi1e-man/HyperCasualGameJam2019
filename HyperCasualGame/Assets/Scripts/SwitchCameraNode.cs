using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchCameraNode : SwitchNode
{
    private bool isSwitchCam;

    public override void SwitchCam()
    {
        base.SwitchCam();
        if (!isSwitchCam)
        {
            CameraManager.Instance.SwitchCamera();
            isSwitchCam = true;
        }
    }
}
