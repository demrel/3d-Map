﻿using ExitGames.Client.Photon;
using Photon.Pun;
using Photon.Realtime;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PhotonRoom : MonoBehaviourPunCallbacks,IInRoomCallbacks
{
    public static PhotonRoom room;
    private PhotonView PV;

    public bool isGameLoaded;
    public int currentScene;

    private Player[] photonPlayers;
    public int playerInRoom;

    public int playersInGame;
    public int myNumberInRoom;


    private bool readyToCount;
    private bool readyToStart;
    public float startingTigme;
    private float lessThanMaxPlayers;
    private float atMaxPlayer;
    private float timeToStart;

    private void Awake()
    {
        if (room==null)
        {
            room = this;
        }
        else
        {
            if (room!=this)
            {
                Destroy(room.gameObject);
                room = this;
            }
        }
        DontDestroyOnLoad(this.gameObject);
    }
  
    public override void OnEnable()
    {
        base.OnEnable();
        PhotonNetwork.AddCallbackTarget(this);
        SceneManager.sceneLoaded += OnSceneFinisheddLoading;
    }
    public override void OnDisable()
    {
        base.OnDisable();
        PhotonNetwork.RemoveCallbackTarget(this);
        SceneManager.sceneLoaded -= OnSceneFinisheddLoading;
    }
    void Start()
    {
        PV = GetComponent<PhotonView>();
        readyToCount = false;
        readyToStart = false;
        lessThanMaxPlayers = startingTigme;
        atMaxPlayer = 6;
        timeToStart = startingTigme;

    }
    void Update()
    {
        if (MultiPlayerSetting.multiPlayerSetting.delayStart)
        {
            if (playerInRoom==1)
            {
                RestartTimer();
            }
            if (isGameLoaded)
            {
                if (readyToStart)
                {
                    atMaxPlayer -= Time.deltaTime;
                    lessThanMaxPlayers = atMaxPlayer;
                    timeToStart = atMaxPlayer;
                }
            }
            else if (readyToCount)
            {
                lessThanMaxPlayers -= Time.deltaTime;
                timeToStart = lessThanMaxPlayers;
            }
            Debug.Log("Display time to start to the players " + timeToStart);
            if (timeToStart<=0)
            {
                StartGame();
            }
        }
    }

    private void StartGame()
    {
        isGameLoaded = true;
        if (!PhotonNetwork.IsMasterClient)
        {
            return;
        }
        if (MultiPlayerSetting.multiPlayerSetting.delayStart)
        {
            PhotonNetwork.CurrentRoom.IsOpen = false;
        }
        PhotonNetwork.LoadLevel(MultiPlayerSetting.multiPlayerSetting.multiPlayerScene);

    }

    private void RestartTimer()
    {
        lessThanMaxPlayers = startingTigme;
        timeToStart = startingTigme;
        atMaxPlayer = 6;
        readyToCount = false;
        readyToStart = false;
    }

    private void OnSceneFinisheddLoading(Scene scene, LoadSceneMode mode)
    {
        currentScene = scene.buildIndex;
        if (currentScene==MultiPlayerSetting.multiPlayerSetting.multiPlayerScene)
        {
            isGameLoaded = true;
            if (MultiPlayerSetting.multiPlayerSetting.delayStart)
            {
                PV.RPC("RPC_LoadedGameScene", RpcTarget.MasterClient);
            }
            else
            {
                RPC_CreatePlayer();
            }
        }
    }

    [PunRPC]
    private void RPC_LoadedGameScene()
    {
        playersInGame++;
        if (playersInGame==PhotonNetwork.PlayerList.Length)
        {
            PV.RPC("RPC_CreatePlayer", RpcTarget.All);
        }
    }
    [PunRPC]
    private void RPC_CreatePlayer()
    {
      //  PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "PNPlayer"), transform.position, Quaternion.identity, 0);
      // PhotonNetwork.Instantiate("OVRPlayerController", transform.position, Quaternion.identity, 0);

    }

    public override void OnJoinedRoom()
    {
        base.OnJoinedRoom();
        Debug.Log("You ar now in the room");
        photonPlayers = PhotonNetwork.PlayerList;
        playerInRoom = photonPlayers.Length;
        myNumberInRoom = playerInRoom;
        PhotonNetwork.NickName = myNumberInRoom.ToString();
        if (MultiPlayerSetting.multiPlayerSetting.delayStart)
        {
            Debug.Log("Displayer players in room out of max players possible ("+playerInRoom+":"+MultiPlayerSetting.multiPlayerSetting.maxPlayers);
            if (playerInRoom>1)
            {
                readyToCount = true;
            }
            if (playerInRoom==MultiPlayerSetting.multiPlayerSetting.maxPlayers)
            {
                readyToStart = true;
                if (!PhotonNetwork.IsMasterClient)
                {
                    return;
                }
                PhotonNetwork.CurrentRoom.IsOpen = false;
            }
        }
        else
        {
            StartGame();

        }

    }
    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        base.OnPlayerEnteredRoom(newPlayer);
        Debug.Log("A new player has joined the room");
        photonPlayers = PhotonNetwork.PlayerList;
        playerInRoom++;
        if (MultiPlayerSetting.multiPlayerSetting.delayStart)
        {
            readyToCount = true;
        }
        if (playerInRoom==MultiPlayerSetting.multiPlayerSetting.maxPlayers)
        {
            readyToStart = true;
            if (!PhotonNetwork.IsMasterClient)
            {
                return;
            }
            PhotonNetwork.CurrentRoom.IsOpen = false;
        }
    }
    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        base.OnPlayerLeftRoom(otherPlayer);

        Debug.Log(otherPlayer.NickName + "Has Left the game");
        playersInGame--;
    }

}
