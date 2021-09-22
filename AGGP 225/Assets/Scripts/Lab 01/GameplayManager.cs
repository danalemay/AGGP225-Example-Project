using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Photon.Pun;
using Photon.Realtime;

public class GameplayManager : MonoBehaviour
{
    public GameObject playerPrefab;

    public List<GameObject> spawnPoints = new List<GameObject>();

    private float time;
    private float endTime = 30f;

    void Start()
    {
        if (!PhotonNetwork.IsConnected)
        {
            SceneManager.LoadScene(PhotonManager.mainSceneName);

            return;
        }        

        if (playerPrefab == null)
        {
            Debug.LogError("<Color=Red><b>Missing</b></Color> playerPrefab Reference. Please set it up in GameObject 'Game Manager'", this);
        }
        else
        {
            GameObject _spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Count - 1)];

            playerPrefab = PhotonNetwork.Instantiate(this.playerPrefab.name, _spawnPoint.transform.localPosition, _spawnPoint.transform.localRotation, 0);
        }
    }

    private void Update()
    {
        time += Time.deltaTime;
        if(time >= endTime)
        {

        }
    }
}
