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
    [SerializeField] private GameObject visionConeObject;
    private float viewDistance = 5;

    // room details
    private Room currentRoom;

    // detained player details
    private HashSet<PlayerMovement> playersDetained = new HashSet<PlayerMovement>();

    // detecting in front of the player
    private void Update()
    {
        // scanning in front of the enemies in the vision cone
        Vector2 direction = currentDirection == Direction.LEFT ? Vector2.left : Vector2.right;
        RaycastHit2D[] hits = Physics2D.RaycastAll(new Vector2(transform.position.x, transform.position.y) + direction * 0.5f, direction, viewDistance);

        HashSet<PlayerMovement> playersDetainedThisCycle = new HashSet<PlayerMovement>();
        foreach (RaycastHit2D hit in hits)
        {
            if (hit.collider.GetComponent<PlayerMovement>() && !hit.collider.GetComponent<PlayerMovement>().Hidden)
            {
                // previously would restart the level on being caught
                // GameManager.instance.FailAndRestartLevel();

                // now, the player is held in place and cannot move
                playersDetainedThisCycle.Add(hit.collider.GetComponent<PlayerMovement>());
            }
        }

        // detaining all caught players to restrict movement
        foreach (PlayerMovement player in playersDetainedThisCycle)
        {
            player.Detained = true;
        }
        // releasing any players from last cycle that were not detained this cycle
        foreach (PlayerMovement player in playersDetained)
        {
            if (!playersDetainedThisCycle.Contains(player))
            {
                player.Detained = false;
            }
        }
        // setting the players detained to the players detained at the end of this frame
        playersDetained = playersDetainedThisCycle;

        // disabling the vision cone graphic if the enemy is not in a visible room
        if (currentRoom != null)
        {
            visionConeObject.SetActive(currentRoom.Visible);
        }
        else
        {
            visionConeObject.SetActive(false);
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
            visionConeObject.transform.rotation = Quaternion.Euler(new Vector3(0, 180, 0));
        }
        else
        {
            rend.flipX = false;
            visionConeObject.transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
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

    // room detection for lighting purposes
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Room>())
        {
            currentRoom = collision.GetComponent<Room>();
        }
    }
}
