using Photon.Pun;
using Photon.Realtime;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhotonLoby : MonoBehaviourPunCallbacks, ILobbyCallbacks
{
    public static PhotonLoby lobby;

    public GameObject battleButton;
    public GameObject cancelButton;


    private void Awake()
    {
        lobby = this;
     
    }
    // Start is called before the first frame update
    void Start()
    {
        PhotonNetwork.ConnectUsingSettings();
    }
    public override void OnConnectedToMaster()
    {
        Debug.Log("Player has connected to the Photon master server");
        PhotonNetwork.AutomaticallySyncScene = true;
        battleButton.SetActive(true);
    }


    public void OnBattletButtonClicked()
    {
        battleButton.SetActive(false);
        cancelButton.SetActive(true);
        PhotonNetwork.JoinRandomRoom();
    }
    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        // base.OnJoinRandomFailed(returnCode, message);
        Debug.Log("Failed connect to room" + message);
        CreateRoom();
    }

   
    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        Debug.Log("Failed create room" + message);
        CreateRoom();
    }


    void CreateRoom()
    {
        int randomRoomName = UnityEngine.Random.Range(0, 100000);
        RoomOptions roomOptions = new RoomOptions() { IsVisible = true, IsOpen = true, MaxPlayers = 10 };
        PhotonNetwork.CreateRoom("Room" + randomRoomName, roomOptions);
    }
    public void OnCancelButtonClicked()
    {
        cancelButton.SetActive(false);
        battleButton.SetActive(true);
        PhotonNetwork.LeaveRoom();
    }

 
   
}
