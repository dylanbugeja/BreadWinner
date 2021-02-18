using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputHandler : MonoBehaviour
{

    private PlayerMovement mover;
    private PlayerInput playerInput;
    private void Awake()
    {  
        playerInput = GetComponent<PlayerInput>();
        var index = playerInput.playerIndex;
        var movers = FindObjectsOfType<PlayerMovement>();
        mover = movers.FirstOrDefault(m => m.GetPlayerIndex() == index);
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
            mover.Grapple();
         //mover.Grapple(context.performed);
    }
    public void OnUse(InputAction.CallbackContext context)
    {
        if (mover != null)
            mover.Use();
        //mover.Grapple(context.performed);
    }

}
