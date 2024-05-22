using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProximityEvent : EventObject
{
    private void Update()
    {
        Collider2D[] overlappingColliders = Physics2D.OverlapCircleAll(transform.position, 1);

        Debug.Log(overlappingColliders);

        bool collisionDetected = false;
        foreach (Collider2D collider in overlappingColliders)
        {
            if (collider.gameObject.GetComponent<PlayerMovement>())
            {
                collisionDetected = true;
                Interact();
                break;
            }
        }

        if (collisionDetected)
        {
            GetComponent<SpriteRenderer>().color = Color.green;
        }
        else
        {
            GetComponent<SpriteRenderer>().color = Color.red;
        }
    }
}
