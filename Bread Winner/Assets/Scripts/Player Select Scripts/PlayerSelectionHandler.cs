using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerSelectionHandler : MonoBehaviour
{
    private Selector selector;
    private PlayerInput playerInput;
    private void Awake()
    {
        playerInput = GetComponent<PlayerInput>();
        var index = playerInput.playerIndex;
        var selectors = FindObjectsOfType<Selector>();
        selector = selectors.FirstOrDefault(s => s.GetPlayerIndex() == index);
        selector.joined();
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
