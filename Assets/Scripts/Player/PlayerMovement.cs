using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private int index;
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
            // GetComponent<Rigidbody2D>().velocity = direction * speed;
            GetComponent<Rigidbody2D>().velocity = new Vector2(direction.x * speed, GetComponent<Rigidbody2D>().velocity.y);
        }
        else if(context.canceled)
        {
            // Debug.Log("canceled");
            GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        }
    }

    public int GetIndex()
    {
        return index;
    }
}
