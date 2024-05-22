using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EventObject : MonoBehaviour
{
    public Action triggerAction;

    // DEBUG
    [SerializeField] private KeyCode triggerKey;

    // DEBUG
    private void Update()
    {
        if (Input.GetKeyUp(triggerKey))
        {
            Interact();
            StartCoroutine(BlinkColor());
        }
    }

    // DEBUG
    private IEnumerator BlinkColor()
    {
        GetComponent<SpriteRenderer>().color = Color.green;
        yield return new WaitForSeconds(1);
        GetComponent<SpriteRenderer>().color = Color.red;
    }

    public void Interact()
    {
        triggerAction?.Invoke();
    }
}
