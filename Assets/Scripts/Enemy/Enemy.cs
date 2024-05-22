using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private Waypoint[] waypoints;
    private Waypoint currentWaypoint;

    public void SetTargetWaypoint(Waypoint newWaypoint)
    {
        // calculate direction
        Vector3 targetLocation = newWaypoint.TrueLocation;
    }
}
