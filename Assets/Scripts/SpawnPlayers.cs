using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using TMPro;

public class SpawnPlayers : MonoBehaviour
{
    public GameObject playerPrefab;

    public float minX;
    public float maxX;
    public float minZ;
    public float maxZ;

    int playerCount = 1;

    Color[] vertexColor;

    void Start()
    {
        /*
        vertexColor[0] = Color.black;
        vertexColor[1] = Color.blue;
        vertexColor[2] = Color.red;*/

       Vector3 randomPosition = new Vector3(Random.Range(minX, maxX),0, Random.Range(minZ, maxZ));

       GameObject temp= PhotonNetwork.Instantiate(playerPrefab.name, randomPosition, Quaternion.identity);
       temp.GetComponentInChildren<TextMeshPro>().text = playerCount.ToString();
        playerCount++;
       //temp.GetComponentInChildren<TextMeshPro>().color = vertexColor[playerCount];
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
