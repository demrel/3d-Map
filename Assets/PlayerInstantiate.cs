using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInstantiate : MonoBehaviour
{
    
    void Start()
    {
        PhotonNetwork.Instantiate("OVRPlayerController", transform.position, Quaternion.identity, 0);
        PhotonNetwork.InstantiateSceneObject("brain (3)", new Vector3(0.459f, 1.2747f, 5.148f), new Quaternion(-26.105f, -147.434f,0,0), 0);

    }


}
