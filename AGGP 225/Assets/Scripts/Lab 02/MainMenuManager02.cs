using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class MainMenuManager02 : MonoBehaviour
{
    #region UI Variables

    [SerializeField]
    Button createButton;
    [SerializeField]
    Button randomButton;

    [SerializeField]
    TMP_Text log;

    #endregion
    public static MainMenuManager02 Instance { get; private set; }

    void Awake()
    {
        if (Instance)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }

    private void Update()
    {
        //if(PhotonManager.Instance.canConnect)
        //{
        //    createButton.interactable = true;
        //    randomButton.interactable = true;
        //}
        //else
        //{
        //    createButton.interactable = false;
        //    randomButton.interactable = false;
        //}
    }

    public void UpdateLog(string _log)
    {
        if (_log == null)
            return;

        log.text = System.Environment.NewLine + _log;
    }

    public void OnCreateRoomClick()
    {
        if(PhotonManager.Instance != null)
        {
            PhotonManager.Instance.CreateRoom();
        }
        else
        {
            Debug.LogError("[MainMenuManager] Unable to Create Room - PhotonManager unable to be found");
        } 
    }

    public void OnJoinRandomClick()
    {
        if (PhotonManager.Instance != null)
        {
            PhotonManager.Instance.JoinRandomRoom();
        }
        else
        {
            Debug.LogError("[MainMenuManager] Unable to Join Random Room - PhotonManager unable to be found");
        }
    }

    public void OnJoinChatRoomClick()
    {
        if (PhotonManager.Instance != null)
        {
            PhotonManager.Instance.JoinRandomRoom();
        }
        else
        {
            Debug.LogError("[MainMenuManager] Unable to Join Random Room - PhotonManager unable to be found");
        }
    }

    public void QuitApplication()
    {
        Application.Quit();
    }
}
