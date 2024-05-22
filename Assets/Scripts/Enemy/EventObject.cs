using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public abstract class EventObject : MonoBehaviour
{
    public Action triggerAction;

    public void Interact()
    {
        triggerAction?.Invoke();
    }
}
