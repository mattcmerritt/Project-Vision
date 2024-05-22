using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Waypoint : MonoBehaviour
{
    // Reference for moving the enemy around
    private Enemy parent;

    // Reference for the object that causes the enemy to walk to this point
    [SerializeField] private EventObject triggerObject;

    // The object's true world position
    private Vector3 trueLocation;
    public Vector3 TrueLocation
    {
        get { return this.trueLocation; }
        private set { this.trueLocation = value; }
    }

    private void Start()
    {
        parent = GetComponentInParent<Enemy>();

        TrueLocation = transform.position;

        if (triggerObject)
        {
            triggerObject.triggerAction += IssueMoveCommand;
        }
    }

    private void IssueMoveCommand()
    {
        parent.SetTargetWaypoint(this);
    }
}