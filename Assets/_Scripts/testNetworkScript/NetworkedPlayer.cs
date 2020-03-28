using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NetworkedPlayer :MonoBehaviour, IPunObservable
{
    public GameObject avatar;

    public Transform playerGlobal;
    public Transform playerLocal;
    public PhotonView photonView;

    void Start()
    {
        Debug.Log("Player instantiated");

        if (photonView.IsMine)
        {
            Debug.Log("Player is mine");

            //playerGlobal = GameObject.Find("OVRPlayerController").transform;
            //playerLocal = playerGlobal.Find("OVRCameraRig/TrackingSpace/CenterEyeAnchor");

            this.transform.SetParent(playerLocal);
            this.transform.localPosition = Vector3.zero;
        }
    }

    void IPunObservable.OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            stream.SendNext(playerGlobal.position);
            stream.SendNext(playerGlobal.rotation);
            stream.SendNext(playerLocal.localPosition);
            stream.SendNext(playerLocal.localRotation);
        }
        else
        {
            this.transform.position = (Vector3)stream.ReceiveNext();
            this.transform.rotation = (Quaternion)stream.ReceiveNext();
            avatar.transform.localPosition = (Vector3)stream.ReceiveNext();
            avatar.transform.localRotation = (Quaternion)stream.ReceiveNext();
        }
    }
}
