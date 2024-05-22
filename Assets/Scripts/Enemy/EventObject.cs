using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EventObject : MonoBehaviour
{
    public UnityAction triggerAction;

    public void Interact()
    {
        triggerAction.Invoke();
    }
}
