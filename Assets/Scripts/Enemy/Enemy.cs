using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private Waypoint[] waypoints;
    private Waypoint currentWaypoint;
    private Direction currentDirection;

    private void Start()
    {
        waypoints = GetComponentsInChildren<Waypoint>();
    }

    public void SetTargetWaypoint(Waypoint newWaypoint)
    {
        // calculate direction
        float xDisplacement = newWaypoint.TrueLocation.x - transform.position.x;
        currentDirection = xDisplacement < 0 ? Direction.LEFT : Direction.RIGHT;

        // changing the sprite direction to reflect the facing direction
        SpriteRenderer rend = GetComponent<SpriteRenderer>();
        if (currentDirection == Direction.LEFT)
        {
            rend.flipX = true;
        }
        else
        {
            rend.flipX = false;
        }

        // move between positions using coroutine
        StartCoroutine(MoveToPosition(newWaypoint.TrueLocation, 3));
    }

    private IEnumerator MoveToPosition(Vector3 target, float duration)
    {
        Vector3 startPosition = transform.position;
        float elapsedTime = 0f;
        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            transform.position = Vector3.Lerp(startPosition, target, elapsedTime / duration);
            yield return null;
        }

        // move the enemy exactly to the end
        transform.position = target;
    }
}
