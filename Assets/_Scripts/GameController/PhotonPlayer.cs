using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class PhotonPlayer : MonoBehaviour
{
    public PhotonView PV;
    public GameObject myAvatar;
    public int MyTeam;
    public void Awake()
    {
        PV = GetComponent<PhotonView>();

    }
    // Start is called before the first frame update
    void Start()
    {
        PV = GetComponent<PhotonView>();
        if (PV.IsMine)
        {
            PhotonNetwork.Instantiate("OVRPlayerController",new Vector3(2.459f, 1.303f, 6.755f),new Quaternion(0, -141.833f,0,0));
            
        }
        //if (PV.IsMine)
        //{
        //    PV.RPC("RPC_GetTeam", RpcTarget.MasterClient);
        //}
    }
    void Update()
    {
     
        //if (myAvatar == null && MyTeam != 0)
        //{
        //    if (MyTeam == 1)
        //    {
        //        int spawnPicker = Random.Range(0, GameSetup.GS.spawnPointsTeamOne.Length);
        //        if (PV.IsMine)
        //        {
        //            myAvatar = PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "PlayerAvatar"),
        //                GameSetup.GS.spawnPointsTeamOne[spawnPicker].position, GameSetup.GS.spawnPointsTeamOne[spawnPicker].rotation, 0);
        //        }
        //    }
        //    else
        //    {
        //        int spawnPicker = Random.Range(0, GameSetup.GS.spawnPointsTeamTwo.Length);
        //        if (PV.IsMine)
        //        {
        //            myAvatar = PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "PlayerAvatar"),
        //                GameSetup.GS.spawnPointsTeamTwo[spawnPicker].position, GameSetup.GS.spawnPointsTeamTwo[spawnPicker].rotation, 0);
        //        }
        //    }
        //}
    }

    [PunRPC]
    void RPC_GetTeam()
    {
        MyTeam = GameSetup.GS.nextPlayersTeam;
        GameSetup.GS.UpdateTeam();
        PV.RPC("RPC_SendTeam", RpcTarget.OthersBuffered, MyTeam);
    }

    [PunRPC]
    void RPC_SendTeam(int whichTeam)
    {
        MyTeam = whichTeam;
    }
}
