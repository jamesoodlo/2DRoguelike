using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputHandle : MonoBehaviour
{
    public bool attack, run, dash, interact, item1, item2, item3, escape;
    public Vector2 move;
    
    public void OnMove(InputAction.CallbackContext context)
    {
        move = context.ReadValue<Vector2>();
    }

    public void OnAttack(InputAction.CallbackContext context)
    {
        if(context.started)
        {
            attack = true;
        }
        if(context.performed)
        {
            attack = true;
        }
        if(context.canceled)
        {
            attack = false;
        }
    }

    public void OnRun(InputAction.CallbackContext context)
    {
        if(context.started)
        {
            run = true;
        }
        if(context.performed)
        {
            run = true;
        }
        if(context.canceled)
        {
            run = false;
        }
    }

    public void OnDash(InputAction.CallbackContext context)
    {
        if(context.started)
        {
            dash = true;
        }
        if(context.performed)
        {
            dash = true;
        }
        if(context.canceled)
        {
            dash = false;
        }
    }

    public void OnInteract(InputAction.CallbackContext context)
    {
        if(context.started)
        {
            interact = true;
        }
        if(context.performed)
        {
            interact = true;
        }
        if(context.canceled)
        {
            interact = false;
        }
    }

    public void OnItem1(InputAction.CallbackContext context)
    {
        if(context.started)
        {
            item1 = true;
        }
        if(context.performed)
        {
            item1 = true;
        }
        if(context.canceled)
        {
            item1 = false;
        }
    }

    public void OnItem2(InputAction.CallbackContext context)
    {
        if(context.started)
        {
            item2 = true;
        }
        if(context.performed)
        {
            item2 = true;
        }
        if(context.canceled)
        {
            item2 = false;
        }
    }

    public void OnItem3(InputAction.CallbackContext context)
    {
        if(context.started)
        {
            item3 = true;
        }
        if(context.performed)
        {
            item3 = true;
        }
        if(context.canceled)
        {
            item3 = false;
        }
    }

    public void OnEscape(InputAction.CallbackContext context)
    {
        if(context.started)
        {
            escape = true;
        }
        if(context.performed)
        {
            escape = true;
        }
        if(context.canceled)
        {
            escape = false;
        }
    }
}
