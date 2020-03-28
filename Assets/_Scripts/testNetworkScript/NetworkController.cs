using Photon.Pun;
using Photon.Realtime;
using UnityEngine;

public class NetworkController : MonoBehaviour
{
    string _room = "Tutorial_Convrge";
    void Start()
    {
        PhotonNetwork.ConnectUsingSettings();
    }

    void OnJoinedLobby()
    {
        Debug.Log("joined lobby");

        RoomOptions roomOptions = new RoomOptions() { };
        PhotonNetwork.JoinOrCreateRoom(_room, roomOptions, TypedLobby.Default);
    }

    void OnJoinedRoom()
    {
        PhotonNetwork.Instantiate("NetworkedPlayer", Vector3.zero, Quaternion.identity, 0);
    }
}
