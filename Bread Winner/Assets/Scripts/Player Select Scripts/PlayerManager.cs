using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using System.Linq;
using UnityEngine.SceneManagement;

public class PlayerManager : MonoBehaviour
{
    private List<PlayerConfiguration> playerConfigs;

    [SerializeField] private int maxPlayers = 2;

    public static PlayerManager instance { get; private set; }

    private void Awake()
    {
        if (instance != null)
        {
            Debug.Log("Player Config already exists!");
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(instance);
            playerConfigs = new List<PlayerConfiguration>();
        }
    }
    //Temp will need to adjust to a character class for different animations later
    public void SetPlayerCharacter(int index, Sprite character)
    {
        playerConfigs[index].Character = character;
    }
    public void ReadyPlayer(int index) 
    {
        playerConfigs[index].IsReady = true;
        if (playerConfigs.Count == maxPlayers && playerConfigs.All( p => p.IsReady == true))
        {
            //Will switch to level select screen later
            SceneManager.LoadScene("Main");
        }
    } 
    public void HandlePlayerJoin(PlayerInput pi)
    {
        Debug.Log("Player Joined " + pi.playerIndex);
        pi.transform.SetParent(transform);
        if(playerConfigs.Any(p => p.PlayerIndex == pi.playerIndex))
        {
            pi.transform.SetParent(transform);
            playerConfigs.Add(new PlayerConfiguration(pi));
        }
    }
}

public class PlayerConfiguration
{
    public PlayerConfiguration(PlayerInput pi)
    {
        PlayerIndex = pi.playerIndex;
        input = pi;
    }
    public PlayerInput input { get; set; }

    public int PlayerIndex { get; set; }

    public bool IsReady { get; set; }

    public Sprite Character { get; set; }

}
