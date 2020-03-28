using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameSetup : MonoBehaviour
{
    public static GameSetup GS;

    public Transform[] spawnPointsTeamOne;
    public Transform[] spawnPointsTeamTwo;

    public int nextPlayersTeam;
    public Text healthDisplay;
    private void OnEnable()
    {
        if (GS==null)
        {
            GS = this;
        }
    }

    public void DisconetPlayer()
    {
        StartCoroutine(DisconectAndLoad());
    }
    IEnumerator DisconectAndLoad()
    {
       // PhotonNetwork.Disconnect();
        PhotonNetwork.LeaveRoom();
        //    while (PhotonNetwork.IsConnected)
        while (PhotonNetwork.InRoom)
            yield return null;
        SceneManager.LoadScene(MultiPlayerSetting.multiPlayerSetting.menuScene);
    }

    public void UpdateTeam()
    {
        if (nextPlayersTeam == 1)
            nextPlayersTeam = 2;
        else
            nextPlayersTeam = 1;

        // nextPlayersTeam = nextPlayersTeam == 1 ? 2 : 1;

    }

}
