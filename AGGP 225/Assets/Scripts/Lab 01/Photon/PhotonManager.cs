using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Photon.Pun;
using Photon.Realtime;

public class PhotonManager : MonoBehaviourPunCallbacks
{
    #region Room Settings

    /// <summary>
    /// Current game version.
    /// </summary>
    string gameVersion = "1";

    /// <summary>
    /// Stores the max number of players per room.
    /// </summary>
    [SerializeField]
    private byte maxPlayers = 4;

    RoomOptions roomOptions = new RoomOptions();
    #endregion

    #region Static Level Names
    public static string gameplayLevel = "Gameplay";
    public static string mainSceneName = "Main Menu";
    #endregion

    public bool canConnect;
    public static PhotonManager Instance { get; private set; }

    #region Unity Functions
    void Awake()
    {
        if (Instance)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(this);
        }

        PhotonNetwork.AutomaticallySyncScene = true;
        roomOptions.MaxPlayers = maxPlayers;        
    }

    void Start()
    {
        MainMenuManager.Instance.UpdateLog("Connecting to Master...");
        Connect();
    }
    #endregion

    #region Basic Methods
    public void Connect()
    {
        if (!PhotonNetwork.IsConnected)
        {
            PhotonNetwork.ConnectUsingSettings();
            PhotonNetwork.GameVersion = gameVersion;
        }
        else
        {
            canConnect = true;
        }
    }
    #endregion

    #region PhotonManager Public Calls
    /// <summary>
    /// Creates a room with a default filter.
    /// </summary>
    public void CreateRoom()
    {
        if (canConnect)
        {
            MainMenuManager.Instance.UpdateLog("Creating room...");
            PhotonNetwork.CreateRoom(null, roomOptions);
        }
    }

    public void JoinRandomRoom()
    {
        if (canConnect)
        {
            MainMenuManager.Instance.UpdateLog("Trying to join room...");
            PhotonNetwork.JoinRandomRoom();
        }
    }
    #endregion

    #region Photon Callbacks
    public override void OnConnectedToMaster()
    {
        Debug.Log("[PhotonManager][OnConnectedToMaster]");
        MainMenuManager.Instance.UpdateLog("Connected to Master");
        canConnect = true;
    }

    public override void OnDisconnected(DisconnectCause cause)
    {
        Debug.Log("[PhotonManager][OnDisconnected] " + cause);
        MainMenuManager.Instance.UpdateLog("<Color=Red>Disconnected</Color>");
        canConnect = false;
    }

    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        Debug.Log("[PhotonManager][OnCreateRoomFailed]");
        MainMenuManager.Instance.UpdateLog("<Color=Red>Create Room Failed </Color>"+message);
    }

    public override void OnJoinedRoom()
    {
        Debug.Log("[PhotonManager][OnJoinedRoom]");
        MainMenuManager.Instance.UpdateLog("Joined Room");
        PhotonNetwork.LoadLevel(gameplayLevel);
    }

    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        Debug.Log("[PhotonManager][OnJoinedRoom]");
        MainMenuManager.Instance.UpdateLog("<Color=Red>Join Random Room Failed </Color>"+ message);
    }

    public override void OnLeftRoom()
    {
        Debug.Log("[PhotonManager][OnLeftRoom]");
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        Debug.Log("[PhotonManager][OnPlayerEnteredRoom]");
    }

    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        Debug.Log("[PhotonManager][OnPlayerLeftRoom]");
    }
    #endregion
}
