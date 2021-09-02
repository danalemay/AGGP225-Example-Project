using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuUI : MonoBehaviour
{
    public void OnCreateRoomClick()
    {
        PhotonManagerExample.instance.CreateRoom();
    }
    public void OnJoinRoomClick()
    {
        PhotonManagerExample.instance.JoinRandomRoom();
    }
}
