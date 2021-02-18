using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class characterSelection : MonoBehaviour
{
    private int selectedCharacterIndex;
    private Color desireColor;

    [Header("List of character")]
    [SerializeField] private List<Animation> characterList = new List<Animation>();

    [Header("UI References")]
    [SerializeField] private TextMeshProUGUI characterName;
    [SerializeField] private Image characterSplash;
    [SerializeField] private Image bgColor;

    [Header("Sounds")]
    [SerializeField] private AudioClip arrowClick;
    [SerializeField] private AudioClip characterSelectMusic;

    private void Start()
    {
        UpdateCharacterSelectionUI();
    }

    public void LeftArrow()
    {
       
        selectedCharacterIndex--;
      
        if(selectedCharacterIndex < 0)
            selectedCharacterIndex = characterList.Count - 1;

        UpdateCharacterSelectionUI();
        
    }

    public void RightArrow()
    {
       
        selectedCharacterIndex++;
        if (selectedCharacterIndex == characterList.Count)
            selectedCharacterIndex = 0;

        UpdateCharacterSelectionUI();
    }

   

    private void UpdateCharacterSelectionUI()
    {
        //splash, name, desired color
        //characterSplash.sprite =  characterList[selectedCharacterIndex].splash;
        //characterName.text = characterList[selectedCharacterIndex].cName;
       // desireColor = characterList[selectedCharacterIndex].cColor;
    }

    [System.Serializable]
    public class CharacterSelectObject
    {
        public Sprite splash;
        public string cName;
        public Color cColor;
    }
}
