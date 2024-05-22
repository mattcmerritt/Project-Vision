using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public PlayerMovement movement;

    public void Start() 
    {
        PlayerMovement[] players = FindObjectsOfType<PlayerMovement>();
        int index = GetComponent<PlayerInput>().playerIndex;
        foreach (PlayerMovement p in players)
        {
            if (p.GetIndex() == index)
            {
                movement = p;
            }
        }
    }

    public void MovePlayer(InputAction.CallbackContext context)
    {
        if (movement != null)
        {
            movement.Move(context);
        }
    }
}
