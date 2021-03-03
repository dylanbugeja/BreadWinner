using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputHandler : MonoBehaviour
{
    private PlayerConfiguration playerConfig;
    //private PlayerInput pi;
    private PlayerMovement mover;
    private Animator animator;

    //[SerializeField] private ;

    private PlayerControls controls;
    private void Awake()
    {  
        mover = GetComponent<PlayerMovement>();
        animator = GetComponent<Animator>();
        controls = new PlayerControls();
    }
    public void InitializePlayer(PlayerConfiguration pc)
    {
        playerConfig = pc;
        playerConfig.input.onActionTriggered += Input_onActionTriggered;

    }

    private void Input_onActionTriggered(InputAction.CallbackContext obj)
    {
        Debug.Log(obj.action.name);
        if (obj.action.name == controls.Player.Move.name)
        {
            OnMove(obj);
        }
        else if (obj.action.name == controls.Player.Jump.name)
        {
            OnJump(obj);
        }
        else if (obj.action.name == controls.Player.Grapple.name)
        {
            OnGrapple(obj);
        }
        else if (obj.action.name == controls.Player.Use.name)
        {
            OnUse(obj);
        }
        
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        if (mover != null)
        mover.SetInputValue(context.ReadValue<Vector2>());
    }
    public void OnJump(InputAction.CallbackContext context)
    {
        if (mover != null)
        mover.Jump(context.performed);
    }
    public void OnGrapple(InputAction.CallbackContext context)
    {
        if (mover != null)
            //mover.Grapple();
            mover.Grapple(context.performed);
    }
    public void OnUse(InputAction.CallbackContext context)
    {
        if (mover != null)
            //mover.Use();
            mover.Use(context.performed);
    }

}
