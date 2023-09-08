using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using TMPro;

public class SpawnPlayers : MonoBehaviour
{
    [Header("Main Player")]
    public GameObject playerPrefab;

    [Header("Spawn borders")]
    public float minX;
    public float maxX;
    public float minZ;
    public float maxZ;


    #region Unity Methods
    void Start()
    {
       Vector3 randomPosition = new Vector3(Random.Range(minX, maxX),0, Random.Range(minZ, maxZ));
        GameObject temp = PhotonNetwork.Instantiate(playerPrefab.name, randomPosition, Quaternion.identity);
        temp.GetComponent<PlayerSync>().IsLocalPlayer();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    #endregion Unity methods
}
