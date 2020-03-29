using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandController : MonoBehaviour
{
    public PhotonView PV;
    public IPlayer Player{get;set;}
    public int handIndex { get; set; }
    // Start is called before the first frame update
    private void Start()
    {
        //if (PV.IsMine)
        //{
        //    transform.GetChild(0).GetComponent<OVRGrabber>().enabled=true;
        //}
        //else
        //{
        //    transform.GetChild(0).GetComponent<OVRGrabber>().enabled = false;
        //}
    }

    // Update is called once per frame
    void Update()
    {
        if (PV.IsMine)
        {
            transform.position = Player.GetHandPos(handIndex);
            transform.rotation = Player.GetHandRotation(handIndex);
        }
       
    }
    

    //void IPunObservable.OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    //{
    //    if (stream.IsWriting)
    //    {
    //        stream.SendNext(Player.GetHandPos(handIndex));
    //        stream.SendNext(Player.GetHandRotation(handIndex));
    //    }
    //    else
    //    {
    //        this.transform.position = (Vector3)stream.ReceiveNext();
    //        this.transform.rotation = (Quaternion)stream.ReceiveNext();
    //        //avatar.transform.localPosition = (Vector3)stream.ReceiveNext();
    //        //avatar.transform.localRotation = (Quaternion)stream.ReceiveNext();
    //    }
    //}
}
