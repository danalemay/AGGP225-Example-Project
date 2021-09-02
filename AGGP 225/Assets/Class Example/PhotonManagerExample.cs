using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Photon.Pun;
using Photon.Realtime;

public class PhotonManagerExample : MonoBehaviourPunCallbacks
{
    /// <summary>
    /// Current game version.
    /// </summary>
    string gameVersion = "1";    
    RoomOptions roomOptions = new RoomOptions();
    static string gameplayLevel = "GameplayExample";

    public static PhotonManagerExample instance { get; private set; }

    void Awake()
    {
        if (instance)
        {
            Destroy(this);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(this);
        }

        PhotonNetwork.AutomaticallySyncScene = true;
        roomOptions.MaxPlayers = 4;
    }

    void Start()
    {
        Connect();
    }

    /// <summary>
    /// Connects user to Master server.
    /// </summary>
    public void Connect()
    {
        if (!PhotonNetwork.IsConnected)
        {
            PhotonNetwork.ConnectUsingSettings();
            PhotonNetwork.GameVersion = gameVersion;
        }
    }

    public void CreateRoom()
    {
        Debug.Log("[PhotonManager][CreateRoom] Trying to create room...");
        PhotonNetwork.CreateRoom(null, roomOptions);
    }

    public void JoinRandomRoom()
    {
        Debug.Log("[PhotonManager][JoinRandomRoom] Trying to join random room...");
        PhotonNetwork.JoinRandomRoom();
    }

    #region Photon Callbacks
    public override void OnConnectedToMaster()
    {
        Debug.Log("[PhotonManager][OnConnectedToMaster]");
    }

    public override void OnCreatedRoom()
    {
        Debug.Log("[PhotonManager][OnCreatedRoom]");
    }

    public override void OnJoinedRoom()
    {
        Debug.Log("[PhotonManager][OnJoinedRoom]");

        //new
        PhotonNetwork.LoadLevel(gameplayLevel);
    }

    public override void OnDisconnected(DisconnectCause cause)
    {
        Debug.Log("[PhotonManager][OnDisconnected] " + cause);
    }

    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        Debug.Log("[PhotonManager][OnCreateRoomFailed]" + message);
        JoinRandomRoom();
    }
    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        Debug.Log("[PhotonManager][OnJoinedRoom] "+ message);
        CreateRoom();
    }

    #endregion

}
