using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerSelectionHandler : MonoBehaviour
{
    private Selector selector;
    private PlayerInput playerInput;
    private PlayerControls controls;
    private void Awake()
    {
        playerInput = GetComponent<PlayerInput>();
        var index = playerInput.playerIndex;
        var selectors = FindObjectsOfType<Selector>();
        selector = selectors.FirstOrDefault(s => s.GetPlayerIndex() == index);
        selector.joined(playerInput);
        controls = new PlayerControls();
        playerInput.onActionTriggered += PlayerInput_onActionTriggered;

    }

    private void PlayerInput_onActionTriggered(InputAction.CallbackContext obj)
    {
        if (obj.action.name == controls.PlayerSelect.NavigateLeft.name)
        {
            OnLeft(obj);
        }
        else if (obj.action.name == controls.PlayerSelect.NavigateRight.name)
        {
            OnRight(obj);
        }
        else if (obj.action.name == controls.PlayerSelect.Confirm.name)
        {
            OnSelect(obj);
        }
        else if (obj.action.name == controls.PlayerSelect.Back.name)
        {
            OnBack(obj);
        }
    }

    public void OnLeft(InputAction.CallbackContext context)
    {
        if (selector != null)
            selector.Left(context.performed);
    }
    public void OnRight(InputAction.CallbackContext context)
    {
        if (selector != null)
            selector.Right(context.performed);
    }
    public void OnSelect(InputAction.CallbackContext context)
    {
        if (selector != null)
            selector.Select(context.performed);
    }
    public void OnBack(InputAction.CallbackContext context)
    {
        if (selector != null)
            selector.Back(context.performed);
    }
}