using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float speed;

    public void Move(InputAction.CallbackContext context)
    {
        if(context.started)
        {
            // Debug.Log("started");
        }
        else if(context.performed)
        {
            // Debug.Log("performed");
            Vector2 direction = context.ReadValue<Vector2>();
            GetComponent<Rigidbody2D>().velocity = direction * speed;
        }
        else if(context.canceled)
        {
            // Debug.Log("canceled");
            GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        }
    }
}
