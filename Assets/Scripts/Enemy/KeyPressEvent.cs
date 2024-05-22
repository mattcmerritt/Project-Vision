using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// DEBUG
public class KeyPressEvent : EventObject
{    
    [SerializeField] private KeyCode triggerKey;

    private void Update()
    {
        if (Input.GetKeyUp(triggerKey))
        {
            Interact();
            StartCoroutine(BlinkColor());
        }
    }

    private IEnumerator BlinkColor()
    {
        GetComponent<SpriteRenderer>().color = Color.green;
        yield return new WaitForSeconds(1);
        GetComponent<SpriteRenderer>().color = Color.red;
    }
}
