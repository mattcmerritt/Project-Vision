using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

// Outdated system for triggering enemy waypoints
// TODO: Remove when fully replaced
public abstract class EventObject : MonoBehaviour
{
    public Action triggerAction;

    public void Interact()
    {
        triggerAction?.Invoke();
    }
}
