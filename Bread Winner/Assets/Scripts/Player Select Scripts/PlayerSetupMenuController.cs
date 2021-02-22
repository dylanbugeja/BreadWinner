using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerSetupMenuController : MonoBehaviour
{
    private int playerIndex;
    [SerializeField] private TextMeshProUGUI nameText;
    [SerializeField] private GameObject setupPanel;
    [SerializeField] private GameObject readyPanel;
    [SerializeField] private Button readyButton; 

    private float ignoreTime = 1.5f;
    private bool inputEnabled;

    public void SetPlayerIndex(int pi)
    {
        playerIndex = pi;
        nameText.SetText("Player " + (pi + 1).ToString());
        ignoreTime = Time.time + ignoreTime;
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time > ignoreTime)
        {
            inputEnabled = true;
        }
    }
    public void setCharacter(string character)
    {
        if (!inputEnabled) { return; }

        PlayerManager.instance.SetPlayerCharacter(playerIndex, character);
        readyPanel.SetActive(true);
        readyButton.Select();
        setupPanel.SetActive(false);

    }
    public void ReadyPlayer()
    {
        if (!inputEnabled) { return; }

        PlayerManager.instance.ReadyPlayer(playerIndex);
        readyButton.gameObject.SetActive(false);
    }
}
