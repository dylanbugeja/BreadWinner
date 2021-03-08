using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WInner : MonoBehaviour
{
    private string currentState;
    [SerializeField] private Animator animator;
    private void Start()
    {
        ChangeAnimation(FindObjectOfType<PlayerManager>().Winner); 
    }
    private void ChangeAnimation(string newState)
    {
        if (currentState == newState) return; //Dylan: Stop animator from interrupting


        animator.Play(newState); //Dylan: Play new Animation

        currentState = newState; //reassign the current state
    }
}
