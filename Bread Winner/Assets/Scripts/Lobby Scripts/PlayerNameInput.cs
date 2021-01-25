using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerNameInput : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] private TMP_InputField nameInputField = null;
    [SerializeField] private Button continueButton = null;

    public static string DisplayName{ get; private set; }

    private const string PlayerPrefsNameKey = "PlayerName";

    private void Start() => SetUpInputField();

    private void SetUpInputField()
    {
        
        if (!PlayerPrefs.HasKey(PlayerPrefsNameKey)) return;

        //If the player has played before it loads the last name used
        string defaultName = PlayerPrefs.GetString(PlayerPrefsNameKey);

        //Sets the input field to the lastname used
        nameInputField.text = defaultName;
        SetPlayerName(defaultName);

    }
    public void SetPlayerName(string name)
    {
        //If the name is valid activates the continue button
        print(string.IsNullOrEmpty(name));
        continueButton.interactable = !string.IsNullOrEmpty(name);
    }
    public void SetPlayerName()
    {
        continueButton.interactable = !string.IsNullOrEmpty(nameInputField.text);
    }

    //Saves player's name when the continue button is pressed
    public void SavePlayerName()
    {
        //Sets the display name to what the player entered in the input field
        DisplayName = nameInputField.text;

        //Saves the name to the player prefs for next time
        PlayerPrefs.SetString(PlayerPrefsNameKey, DisplayName);
    }
}
