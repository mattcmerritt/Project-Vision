using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public PlayerMovement movement;

    public void Start() 
    {
        DontDestroyOnLoad(gameObject);
        AttachPlayer();
    }

    public void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        AttachPlayer();
    }

    public void AttachPlayer()
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
        else
        {
            AttachPlayer(); // failsafe if player not attached yet
        }
    }

    public void TriggerInteract(InputAction.CallbackContext context)
    {
        if (movement != null)
        {
            movement.Interact(context);
        }
        else
        {
            AttachPlayer(); // failsafe if player not attached yet
        }
    }

    public void TriggerPause(InputAction.CallbackContext context)
    {
        if(context.started)
        {
            PauseManager pm = FindObjectOfType<PauseManager>();
            if(pm == null)
            {
                Debug.LogError("ERROR: The pause manager does not exist!");
            }
            else if(!pm.CheckIfNotUnpausable())
            {
                pm.TogglePause();
            }
        }
    }
}
