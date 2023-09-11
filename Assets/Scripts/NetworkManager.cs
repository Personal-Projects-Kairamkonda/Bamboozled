using UnityEngine.UI;
using UnityEngine;
using Photon.Pun;
using TMPro;

public class NetworkManager : MonoBehaviourPunCallbacks
{
    #region Class variables
    [Header("Texts")]
    public TextMeshProUGUI connectionStatusText;

    [Header("Buttons")]
    public Button connectButton;

    [Header("Enable if player has no camera")]
    public bool cameraView;
    #endregion

    #region Unity default methods
    private void Awake()
    {
        PhotonNetwork.AutomaticallySyncScene = true;
    }

    void Start()
    {
        connectButton.onClick.AddListener(() => Connect());
    }

    private void Update()
    {
        UpdateConnectionStatus(PhotonNetwork.NetworkClientState.ToString());
    }
    #endregion

    #region Pun callbacks override methods
    /// <summary>
    /// Connects to master server.
    /// </summary>
    public override void OnConnectedToMaster()
    {
        //PhotonNetwork.JoinLobby();

        // Creates a random room. if the room isn't already created, the thread calls "OnJoinRandomFailed()".
        PhotonNetwork.JoinRandomRoom();
    }

    public override void OnJoinedLobby()
    {
        //SceneManager.LoadScene(Constants.lobbyScene);

        Debug.LogError($"Connected to a server, looking for a random room.");
    }

    /// <summary>
    /// This function called, when room isn't created.
    /// </summary>
    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        Debug.LogError($"Joining random room failed because of {message}. Creating a new one.");

        // Creates an empty room to join.
        PhotonNetwork.CreateRoom(null);
    }

    /// <summary>
    /// Joins a room when it is created.
    /// </summary>
    public override void OnJoinedRoom()
    {
        // Disable some gameobjects before spawing.
        DisableConnectUI();
    }

    /// <summary>
    /// The information is updated when new player enters in to the same room.
    /// </summary>
    /// <param name="newPlayer">new player apart from host.</param>
    public override void OnPlayerEnteredRoom(Photon.Realtime.Player newPlayer)
    {
        Debug.LogError($"Player {newPlayer.ActorNumber} entered the room.");
    }
    #endregion

    #region Class methods
    private void Connect()
    {
        if(PhotonNetwork.IsConnected)
            // Joins a random room if it is connected.
            PhotonNetwork.JoinRandomRoom();
        else
            // Connects to server
            PhotonNetwork.ConnectUsingSettings();
    }

    private string UpdateConnectionStatus(string message)
    {
        return connectionStatusText.text = "Connection status: "+message;
    }

    private void DisableConnectButton()
    {
        connectButton.gameObject.SetActive(false);
    }

    private void DisableConnectUI()
    {
        DisableConnectButton();
        DisableLobbyCamera();

    }

    private void DisableLobbyCamera()
    {
        Camera.main.enabled = cameraView;
    }

  
    #endregion
}
