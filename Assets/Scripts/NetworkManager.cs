using Photon.Pun;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class NetworkManager : MonoBehaviourPunCallbacks
{
    #region Class variables
    [Header("Player Properties")]
    [SerializeField] private GameObject playerPrefab;
    [SerializeField] private Transform spawnTransform;

    [Header("Texts")]
    public TextMeshProUGUI connectionStatusText;

    [Header("Buttons")]
    public Button connectButton;

    [Header("Disable On Connect")]
    [SerializeField] private GameObject mainCamera;
    #endregion

    #region Components
    [Header("Components dependancies")]
    [SerializeField]private PhotonView view;

    #endregion

    #region Unity default methods
    private void Awake()
    {
        GetRequiredComponentsReferences();
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
        Debug.LogError($"Player {PhotonNetwork.LocalPlayer.ActorNumber} joined the room.");

        // Private method to a spawn player and disable some gameobjects before spawing.
        SpawnPlayers();
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

    #region Private methods
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

    private void SpawnPlayers()
    {
        DisableConnectButton();
        DisableLobbyCamera();

        // Spawns the player when joined in a room.
        PhotonNetwork.Instantiate(playerPrefab.name, spawnTransform.position, Quaternion.identity);
    }

    private void DisableLobbyCamera()
    {
        mainCamera.gameObject.SetActive(false);
    }

    private void GetRequiredComponentsReferences()
    {
        view = playerPrefab.GetComponentInChildren<PhotonView>();
    }
    #endregion
}
