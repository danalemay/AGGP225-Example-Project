using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

using Photon.Pun;
using Photon.Realtime;

public class UsernameExample : MonoBehaviour
{
    public TMP_Text usernameText;

    public static UsernameExample instance;

    private void Awake()
    {
        instance = this;

       // usernameText.text = PhotonManager.Instance.username;
        PhotonManager.Instance.gameObject.GetPhotonView().RPC("UsernameRPC", RpcTarget.AllBuffered, PhotonManager.Instance.username.ToString(), "hello world");
    }
}
