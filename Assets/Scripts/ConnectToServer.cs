using UnityEngine.SceneManagement;
using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using TMPro;

public class ConnectToServer : MonoBehaviourPunCallbacks
{
    [Header("Texts")]
    public TextMeshProUGUI connectionStatusText;

    [Header("Buttons")]
    public Button connectButton;

    #region Unity default methods
    private void Awake()
    {
        UpdateConnectionStatus("Loading...!");   
    }

    void Start()
    {
        connectButton.onClick.AddListener(() => Connect());
;   }

    private void Update()
    {
        UpdateConnectionStatus(PhotonNetwork.NetworkClientState.ToString());
    }
    #endregion

    #region Pun callbacks override methods
    public override void OnConnectedToMaster()
    {
        //PhotonNetwork.JoinLobby();
        PhotonNetwork.JoinRandomRoom();
    }

    public override void OnJoinedLobby()
    {
        //SceneManager.LoadScene(Constants.lobbyScene);
        Debug.LogError($"Connected to a server, looking for a random room.");
    }

    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        Debug.LogError($"Joining random room failed because of {message}. Creating a new one.");
        PhotonNetwork.CreateRoom(null);
    }

    public override void OnJoinedRoom()
    {
        Debug.LogError($"Player {PhotonNetwork.LocalPlayer.ActorNumber} joined the room.");
        DisableConnectButton();
    }

    public override void OnPlayerEnteredRoom(Photon.Realtime.Player newPlayer)
    {
        Debug.LogError($"Player {newPlayer.ActorNumber} entered the room.");
    }
    #endregion

    #region Public methods
    public void Connect()
    {
        if(PhotonNetwork.IsConnected)
        {
            PhotonNetwork.JoinRandomRoom();
        }
        else
        {
            PhotonNetwork.ConnectUsingSettings();
        }
    }

    public string UpdateConnectionStatus(string message)
    {
        return connectionStatusText.text = "Connection status: "+message;
    }

    public void DisableConnectButton()
    {
        connectButton.gameObject.SetActive(false);
    }
    #endregion
}
