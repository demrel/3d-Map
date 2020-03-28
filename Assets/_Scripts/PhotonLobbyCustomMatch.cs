using Photon.Pun;
using Photon.Realtime;
using System;
using System.Collections.Generic;
using UnityEngine;

public class PhotonLobbyCustomMatch : MonoBehaviourPunCallbacks
{
    public static PhotonLobbyCustomMatch lobby;

    //public GameObject battleButton;
    //public GameObject cancelButton;

    public string roomName;
    public int roomSize;
    public Transform roomsPanel;
    public GameObject roomListingPrefab;


    public List<RoomInfo> roomListings;
    private void Awake()
    {
        lobby = this;
     
    }
    // Start is called before the first frame update
    void Start()
    {
        PhotonNetwork.ConnectUsingSettings();
        roomListings = new List<RoomInfo>();
    }
    public override void OnConnectedToMaster()
    {
        Debug.Log("Player has connected to the Photon master server");
        PhotonNetwork.AutomaticallySyncScene = true;
        PhotonNetwork.NickName = "Player " + UnityEngine.Random.Range(0, 1000);
        // battleButton.SetActive(true);
    }

    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        base.OnRoomListUpdate(roomList);
        //RemoveRoomListings();

        int tempIndex;
        foreach (var room in roomList)
        {
            if (roomListings!=null)
            {
                tempIndex = roomListings.FindIndex(ByName(room.Name));
            }
            else
            {
                tempIndex = -1;
            }

            if (tempIndex!=-1)
            {
                roomListings.RemoveAt(tempIndex);
                Destroy(roomsPanel.GetChild(tempIndex).gameObject);
            }
            else
            {
                roomListings.Add(room);
                ListRoom(room);
            }
           
        }
    }

   static System.Predicate<RoomInfo> ByName(string name)
    {
        return delegate (RoomInfo room)
        {
            return room.Name == name;
        };
    }

    void RemoveRoomListings()
    {
        int i = 0;
        while (roomsPanel.childCount != 0) 
        {
            Destroy(roomsPanel.GetChild(i).gameObject);
            i++;
        }
       
    }
    void ListRoom(RoomInfo room)
    {
        if (room.IsOpen && room.IsVisible)
        {
            GameObject templisting = Instantiate(roomListingPrefab, roomsPanel);
            RoomButton tempButton = templisting.GetComponent<RoomButton>();
            tempButton.roomName = room.Name;
            tempButton.roomSize = room.MaxPlayers;
            tempButton.SetRoom();
        }
    }


   
 
   
    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        Debug.Log("Failed create room" + message);
        //CreateRoom();
    }


    public void CreateRoom()
    {
        //int randomRoomName = UnityEngine.Random.Range(0, 100000);
        RoomOptions roomOptions = new RoomOptions() { IsVisible = true, IsOpen = true, MaxPlayers = (byte)roomSize };
        PhotonNetwork.CreateRoom("Room: " + roomName, roomOptions);
    }
   
    public void OnRoomNameChanged(string nameIn)
    {
        roomName = nameIn;
    }

    public void OnRoomSizeChanged(string sizeIn)
    {
        roomSize = int.Parse(sizeIn);
    }
    public void JoinLobbyOnClick()
    {
        if (!PhotonNetwork.InLobby)
        {
            PhotonNetwork.JoinLobby();
        }
    }
}
