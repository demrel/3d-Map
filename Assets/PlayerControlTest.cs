using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControlTest : OVRPlayerController
{
    public PhotonView PV;
 
    // Update is called once per frame
    void Update()
    {
        if (PV.IsMine)
        {
            if (!playerControllerEnabled)
            {
                if (OVRManager.OVRManagerinitialized)
                {
                    OVRManager.display.RecenteredPose += ResetOrientation;

                    if (CameraRig != null)
                    {
                        CameraRig.UpdatedAnchors += UpdateTransform;
                    }
                    playerControllerEnabled = true;
                }
                else
                    return;
            }
            //Use keys to ratchet rotation
            if (Input.GetKeyDown(KeyCode.Q))
                buttonRotation -= RotationRatchet;

            if (Input.GetKeyDown(KeyCode.E))
                buttonRotation += RotationRatchet;
        }
    }
}
