using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class Selector : MonoBehaviour
{
    [SerializeField] private int playerIndex = 0;
    [SerializeField] private Animator animator;
    [SerializeField] private GameObject wait;
    [SerializeField] private GameObject join;
    [SerializeField] private GameObject ready;



    private List<string> characters = new List<string>();
    private PlayerManager pm;
    private string currentState;
    private int index = 0;
    private string tempSelection;


    private float ignoreTime = 1f;
    private bool inputEnabled;

    private void Awake()
    {
        characters.Add("Loaf");
        characters.Add("Bagel");
        characters.Add("Swiss");
        characters.Add("Baguette");
        characters.Add("Croissant");

        pm = FindObjectOfType<PlayerManager>();
        currentState = characters[0];
        animator.GetComponentInChildren<Animator>();
    }

    public int GetPlayerIndex()
    {
        return playerIndex;
    }
    private void Update()
    {
        if (Time.time > ignoreTime)
        {
            inputEnabled = true;
        }
    }

    public void Left(bool performed)
    {
        if (performed && inputEnabled)
        {
            index--;
            if (index < 0)
            {
                index = characters.Count - 1;
            }
            Debug.Log(index);
            ChangeAnimation(characters[index]);
            tempSelection = characters[index];
        }
    }
    public void Right(bool performed)
    {
        if (performed && inputEnabled)
        {
            index++;
            if (index > characters.Count - 1)
            {
                index = 0;
            }
            Debug.Log(index);
            ChangeAnimation(characters[index]);
            tempSelection = characters[index];
        }
    }
    public void Select(bool performed)
    {
        if (inputEnabled && performed)
        {
            if (ready.activeSelf == false)
            {
                Debug.Log(characters[index]);
                pm.SetPlayerCharacter(playerIndex, characters[index]);
                pm.ReadyPlayer(playerIndex);
                join.SetActive(false);
                ready.SetActive(true);
            }
            else if (ready.activeSelf == true && playerIndex == 0)
            {
                //Switch to level select if more levels are added later!
                SceneManager.LoadScene("Kitchen");
            }
        }
    }
    public void Back(bool performed)
    {

    }
    private void ChangeAnimation(string newState)
    {
        if (currentState == newState) return; //Dylan: Stop animator from interrupting


        animator.Play(newState); //Dylan: Play new Animation

        currentState = newState; //reassign the current state
    }
    public void joined(PlayerInput pi)
    {
        ignoreTime = Time.time + ignoreTime;
        pm.HandlePlayerJoin(pi);
        inputEnabled = false;
        wait.SetActive(false);
        join.SetActive(true);
    }
}
