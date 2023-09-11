using Photon.Pun;
using UnityEngine;

public class NetworkSpawnPlayer : MonoBehaviourPunCallbacks
{
    #region Class variables
    [Header("Player Properties")]
    [SerializeField] private GameObject playerPrefab;
    [SerializeField] private Vector3 spawnPosition;
    private int index;
    #endregion

    #region Components dependancies
    [Header("Components dependancies")]
    [SerializeField] private PhotonView view;
    #endregion

    #region Unity default methods
    private void Awake()
    {
        GetRequiredComponentsReferences();
        GetDefaultVariableValues();
    }
    #endregion

    #region Pun call back override methods
    /// <summary>
    /// Joins a room when it is created.
    /// </summary>
    public override void OnJoinedRoom()
    {
        base.OnJoinedRoom();

        Debug.LogError($"Player {PhotonNetwork.LocalPlayer.ActorNumber} joined the room.");

        // Private method to a spawn player.
        SpawnPlayer();
    }
    #endregion

    #region Class methods
    private void SpawnPlayer()
    {
        // Spawns the player when joined in a room.
        GameObject temp=PhotonNetwork.Instantiate(playerPrefab.name, spawnPosition, Quaternion.identity);
        temp.name = $"Player {index++}";
    }

    private void GetRequiredComponentsReferences()
    {
        view = playerPrefab.GetComponentInChildren<PhotonView>();
    }

    private void GetDefaultVariableValues()
    {
        spawnPosition = this.transform.position + Vector3.up * 10;
        index = 1;
    }
    #endregion
}
