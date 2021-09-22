using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

using Photon.Pun;
using Photon.Realtime;

public class ChatroomManager : MonoBehaviour
{
    public TMP_InputField input;
    public TMP_Text field;

    public static ChatroomManager instance;

    private void Awake()
    {
        instance = this;
    }
    
    public void Submit()
    {
        if (!string.IsNullOrEmpty(input.text))
        {
            gameObject.GetPhotonView().RPC("UpdateChatroom", RpcTarget.AllBuffered, PhotonManager.Instance.username, input.text);
            input.text = "";
        }
    }

    [PunRPC]
    void UpdateChatroom(string _username, string _chat)
    {
        Debug.Log(_username + ":  " + _chat + "\n");
        field.text += _username + ":  " + _chat + "\n";
    }

}
