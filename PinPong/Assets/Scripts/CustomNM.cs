using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class CustomNM : NetworkManager {

    public static CustomNM Instance
    {
        get;
        private set;
    }
    private GameObject ball;
    private GameObject player;

    public void Awake()
    {
        Instance = this;
    }

    IEnumerator InstantiateTheBall ()
    {
        yield return new WaitForSeconds(0.3f);
        ball = Instantiate(spawnPrefabs[0]);
        NetworkServer.Spawn(ball);
    }

    public override void OnServerAddPlayer(NetworkConnection conn, short playerControllerId)
    {
        int playersCount = NetworkServer.connections.Count;
        if (playersCount <= startPositions.Count)
        {
            if (playersCount == 2)
            {
                StartCoroutine(InstantiateTheBall());
            }
            player = Instantiate(playerPrefab, startPositions[playersCount - 1].position, Quaternion.identity);

            NetworkServer.AddPlayerForConnection(conn, player, playerControllerId);
        }
        else
        {
            conn.Disconnect();
        }
    }

    public override void OnClientConnect(NetworkConnection conn)
    {
        Debug.Log("OnClientConnect");

        if (string.IsNullOrEmpty(this.onlineScene) || this.onlineScene == this.offlineScene)
        {
            ClientScene.Ready(conn);
            if (this.autoCreatePlayer)
            {
                ClientScene.AddPlayer(conn, 0);
            }
        }
    }

    public override void OnServerDisconnect(NetworkConnection conn)
    {
        Destroy(player);
        Destroy(ball);
        conn.Disconnect();
       
    }

    public void OnClose()
    {
        SceneManager.LoadScene(0);
    }

}
