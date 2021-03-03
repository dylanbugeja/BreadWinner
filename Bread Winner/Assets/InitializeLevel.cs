using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InitializeLevel : MonoBehaviour
{
    [SerializeField] private Transform[] playerSpawns;
    [SerializeField] private GameObject[] playerPrefabs;
    private Dictionary<string, GameObject> characters = new Dictionary<string, GameObject>();
    [SerializeField] private GameObject LevelGenerator;
    [SerializeField] private TrackTarget track;

    // Start is called before the first frame update
    void Start()
    {
        //characters = new Dictionary<string, GameObject>();
        characters.Add("Loaf", playerPrefabs[0]);
        characters.Add("Bagel", playerPrefabs[1]);
        characters.Add("Baguette", playerPrefabs[2]);



        GameObject playerManager = FindObjectOfType<PlayerManager>().gameObject;
        playerManager.GetComponent<PlayerInputManager>().DisableJoining();

        foreach (PlayerInput p in playerManager.GetComponentsInChildren<PlayerInput>())
        {
            p.currentActionMap = p.actions.FindActionMap("Player");
        }

        var playerConfigs = PlayerManager.instance.GetPlayerConfigs().ToArray();
        
        for (int i = 0; i < playerConfigs.Length; i++)
        {
            //playerConfigs[i]
            var player = Instantiate(characters[playerConfigs[i].Character], playerSpawns[i].position, playerSpawns[i].rotation, gameObject.transform);
            track.AddTarget(player.transform);
            LevelGenerator.GetComponent<LevelGenerator>().Players.Add(player);
            player.GetComponent<PlayerInputHandler>().InitializePlayer(playerConfigs[i]);
        }
        track.enabled = true;
        LevelGenerator.SetActive(true);
    }
    
}
