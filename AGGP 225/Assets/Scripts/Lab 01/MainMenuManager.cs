using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class MainMenuManager : MonoBehaviour
{
    #region UI Variables

    [SerializeField]
    Button createButton;

    [SerializeField]
    Button randomButton;

    [SerializeField]
    TMP_Text log;

    [SerializeField]
    TMP_InputField inputField;

    #endregion

    public string censorshipFilePath;
    List<string> censorship = new List<string>();
 

    public static MainMenuManager Instance { get; private set; }

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

    void OnEnable()
    {
        string _username = PlayerPrefs.GetString(PhotonManager.usernameKey);

        if(!string.IsNullOrEmpty(_username))
        {
            inputField.text = _username;
        }


        System.IO.StreamReader stream = new System.IO.StreamReader(censorshipFilePath);

        while (!stream.EndOfStream)
        {
            censorship.Add(stream.ReadLine());
        }
    }

    private void Update()
    {
        if(PhotonManager.Instance.canConnect)
        {
            createButton.interactable = true;
            randomButton.interactable = true;
        }
        else
        {
            createButton.interactable = false;
            randomButton.interactable = false;
        }
    }

    public void UpdateLog(string _log)
    {
        if (_log == null)
            return;

        log.text = System.Environment.NewLine + _log;
    }

    public void OnCreateRoomClick()
    {
        if (!UsernameCheck())
        {
            return;
        }

        if (PhotonManager.Instance != null)
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
        if (!UsernameCheck())
        {
            return;
        }

        if (PhotonManager.Instance != null)
        {
            PhotonManager.Instance.JoinRandomRoom();
        }
        else
        {
            Debug.LogError("[MainMenuManager] Unable to Join Random Room - PhotonManager unable to be found");
        }
    }

    public void OnJoinChatroom()
    {   
        if(!UsernameCheck())
        {
            return;
        }

        if (PhotonManager.Instance != null)
        {
            PhotonManager.Instance.JoinChatroom();
        }
        else
        {
            Debug.LogError("[MainMenuManager] Unable to Join Random Room - PhotonManager unable to be found");
        }
    }

    bool UsernameCheck()
    {
        if (string.IsNullOrEmpty(inputField.text))
        {
            log.text = System.Environment.NewLine + "<color=red> You must enter a username before playing. </color>";
            return false;
        }

        foreach (string s in censorship)
        {
            if (inputField.text.Contains(s))
            {
                log.text = System.Environment.NewLine + "<color=red> Your username has forbidden words. </color>";
                return false;
            }
        }

        PhotonManager.Instance.username = inputField.text;

        PlayerPrefs.SetString(PhotonManager.usernameKey, PhotonManager.Instance.username);
        PlayerPrefs.Save();
        return true;
    }

    public void QuitApplication()
    {
        Application.Quit();
    }
}
