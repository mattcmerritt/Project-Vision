using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    // motion details
    [SerializeField] private Waypoint currentWaypoint, previousWaypoint;
    [SerializeField] private Direction currentDirection;
    private float speed = 5;

    // current motion reference
    private Coroutine movementCoroutine;

    // vision cone details
    [SerializeField] private GameObject visionCone;
    private float viewDistance = 5;

    // detecting in front of the player
    private void Update()
    {
        Vector2 direction = currentDirection == Direction.LEFT ? Vector2.left : Vector2.right;
        RaycastHit2D[] hits = Physics2D.RaycastAll(new Vector2(transform.position.x, transform.position.y) + direction * 0.5f, direction, viewDistance);

        foreach (RaycastHit2D hit in hits)
        {
            if (hit.collider.GetComponent<PlayerMovement>())
            {
                GameManager.instance.FailAndRestartLevel();
            }
        }

    }

    public void SetTargetWaypoint(Waypoint newWaypoint)
    {
        // do not allow moving to the same point
        if (newWaypoint == currentWaypoint) return;

        previousWaypoint = currentWaypoint;
        currentWaypoint = newWaypoint;

        // calculate direction
        float xDisplacement = currentWaypoint.TrueLocation.x - previousWaypoint.TrueLocation.x;
        currentDirection = xDisplacement < 0 ? Direction.LEFT : Direction.RIGHT;

        // changing the sprite direction to reflect the facing direction
        SpriteRenderer rend = GetComponent<SpriteRenderer>();
        if (currentDirection == Direction.LEFT)
        {
            rend.flipX = true;
            visionCone.transform.rotation = Quaternion.Euler(new Vector3(0, 180, 0));
        }
        else
        {
            rend.flipX = false;
            visionCone.transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
        }

        // move between positions using coroutine
        if (movementCoroutine != null)
        {
            StopCoroutine(movementCoroutine);
        }        
        movementCoroutine = StartCoroutine(MoveToPosition(newWaypoint.TrueLocation));
    }

    private IEnumerator MoveToPosition(Vector3 target)
    {
        Vector3 startPosition = transform.position;
        Vector3 totalDistance = startPosition - target;
        float duration = totalDistance.magnitude / speed;

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
