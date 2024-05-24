using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PauseManager : MonoBehaviour
{
    [SerializeField] private GameObject PauseMenuPrefab;
    [SerializeField] private PlayerInput P1, P2;

    private void Start()
    {
        DontDestroyOnLoad(gameObject);
    }

    private void Update()
    {
        // connect players if not all are connected
        if(P1 == null || P2 == null)
        {
            PlayerInput[] allPlayers = FindObjectsOfType<PlayerInput>();
            foreach (PlayerInput p in allPlayers)
            {
                if(p.playerIndex == 0)
                {
                    P1 = p;
                }
                else if(p.playerIndex == 1)
                {
                    P2 = p;
                }
            }
        }
    }
    
}
