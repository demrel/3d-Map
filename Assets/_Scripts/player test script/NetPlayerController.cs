using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class NetPlayerController : MonoBehaviour, IPlayer
{
    public List<Component> components;
    public PhotonView pv;
    public GameObject CenterEye;
    public Transform LeftHand;
    public Transform RightHand;
    public GameObject CamRIg;
    // Start is called before the first frame update
    void Start()
    {
        if (pv.IsMine)
        {
            //gameObject.AddComponent<CharacterController>();
            //gameObject.AddComponent<OVRPlayerController>();
            //pv.RPC("AddComponent", RpcTarget.All);
            
            var LefthandOBj = PhotonNetwork.Instantiate("HandLeft", transform.position, transform.rotation, 0);
            var RighthandOBj = PhotonNetwork.Instantiate("HandRight", transform.position, transform.rotation, 0);

            LefthandOBj.transform.GetChild(0).GetComponent<OVRGrabber>().m_player = gameObject;
            RighthandOBj.transform.GetChild(0).GetComponent<OVRGrabber>().m_player = gameObject;

            var Lefthand = LefthandOBj.GetComponent<HandController>();
            var Righthand = RighthandOBj.GetComponent<HandController>();

            Lefthand.Player = this;
            Righthand.Player = this;

            Lefthand.handIndex = 0;
            Righthand.handIndex = 1;
        }
        else
        {
            CamRIg.SetActive(false);
        }
    }

    public Vector3 GetHandPos(int gunIndex)
    {
        return gunIndex == 0 ? LeftHand.position : RightHand.position;
    }

    public  Quaternion GetHandRotation(int gunIndex)
    {
        return gunIndex == 0 ? LeftHand.rotation : RightHand.rotation;
    }

    //[PunRPC]
    //void AddComponent()
    //{
    //    gameObject.AddComponent<CharacterController>();
    //    gameObject.AddComponent<OVRPlayerController>();
    //}


}
