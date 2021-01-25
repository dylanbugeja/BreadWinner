using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using Mirror;
using System.Linq;

public class NetworkManagerBreadWinner : NetworkManager
{
    [SerializeField] private int minPlayers = 2;
    [Scene] [SerializeField] private string menuScene = string.Empty;

    [Header("Room")]
    [SerializeField] private NetworkRoomPlayerBreadWinner roomPlayerPrefab = null;

    public static event Action OnClientConnected;
    public static event Action OnClientDisconnected;


    public List<NetworkRoomPlayerBreadWinner> RoomPlayers { get; } = new List<NetworkRoomPlayerBreadWinner>();

    //Need to register prefabs on the network, this is to register and load all spawnable objects
    public override void OnStartServer() => spawnPrefabs = Resources.LoadAll<GameObject>("SpawnablePrefabs").ToList();

    public override void OnStartClient()
    {
        //Loads all spawnable objects
        var spawnablePrefabs = Resources.LoadAll<GameObject>("SpawnablePrefabs");

        //Registers each on the network
        foreach (var prefab in spawnablePrefabs)
        {
            ClientScene.RegisterPrefab(prefab);
        }
    }


    public override void OnClientConnect(NetworkConnection conn)
    {
        //Uses base logic from OnClientConnect from default network manager
        base.OnClientConnect(conn);

        //Allows us to do things in the future when the client connects
        OnClientConnected?.Invoke();
    }

    public override void OnClientDisconnect(NetworkConnection conn)
    {
        //Uses base logic from OnClientDisconnect from default network manager
        base.OnClientDisconnect(conn);

        //Allows us to do something in the future when the client disconnects
        OnClientDisconnected?.Invoke();
    }

    public override void OnServerConnect(NetworkConnection conn)
    {
        //Checks if the number of players is more than the max players
        if (numPlayers >= maxConnections)
        {
            //If so doesn't let the player connect to the server
            conn.Disconnect();
            return;
        }

        //This checks if the game has started yet
        if (SceneManager.GetActiveScene().path != menuScene)
        {
            //if so don't let the player join
            conn.Disconnect();
            return;
        }
    }

    public override void OnServerAddPlayer(NetworkConnection conn)
    {

        if (SceneManager.GetActiveScene().path == menuScene)
        {
            bool isLeader = RoomPlayers.Count == 0;

            NetworkRoomPlayerBreadWinner roomPlayerInstance = Instantiate(roomPlayerPrefab);

            roomPlayerInstance.IsLeader = isLeader;

            NetworkServer.AddPlayerForConnection(conn, roomPlayerInstance.gameObject);
        }
    }

    public override void OnServerDisconnect(NetworkConnection conn)
    {
        if (conn.identity != null)
        {
            var player = conn.identity.GetComponent<NetworkRoomPlayerBreadWinner>();

            RoomPlayers.Remove(player);

            NotifyPlayersOfReadyState();
        }

        base.OnServerDisconnect(conn);
    }

    public override void OnStopServer()
    {
        RoomPlayers.Clear();
    }

    public void NotifyPlayersOfReadyState()
    {
        foreach (var player in RoomPlayers)
        {
            player.HandleReadyToStart(IsReadyToStart());
        }
    }
    private bool IsReadyToStart()
    {
        if (numPlayers < minPlayers) { return false; }

        foreach (var player in RoomPlayers)
        {
            if (!player.IsReady) { return false; }
        }

        return true;
    }
}
