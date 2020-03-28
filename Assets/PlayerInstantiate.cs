using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInstantiate : MonoBehaviour
{
    
    void Start()
    {
        PhotonNetwork.Instantiate("OVRPlayerController", transform.position, Quaternion.identity, 0);
    }

    
}
