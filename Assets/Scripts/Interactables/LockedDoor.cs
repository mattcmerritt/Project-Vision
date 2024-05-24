using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockedDoor : InteractableElement
{
    [SerializeField] private GameObject unlockInterface;
    [SerializeField] private RectTransform unlockHandleTransform, startHandleTransform, endHandleTransform;
    [SerializeField] private float durationLocked, durationUnlocked;

    private bool unlockAllowed;

    private void Start()
    {
        float totalDuration = durationLocked + durationUnlocked;
        startHandleTransform.rotation = Quaternion.Euler(0, 0, 0);
        endHandleTransform.rotation = Quaternion.Euler(0, 0, -durationLocked / totalDuration * 360);
    }

    public override void Interact(GameObject player)
    {
        interactUI.SetActive(false);
        unlockInterface.SetActive(true);
        player.GetComponent<PlayerMovement>().MovementLocked = true;
        StartCoroutine(UnlockMinigameCycle());
    }

    private IEnumerator UnlockMinigameCycle()
    {
        float totalDuration = durationLocked + durationUnlocked;
        float elapsedTime = 0f;
        while (elapsedTime < durationLocked)
        {
            unlockAllowed = false;
            elapsedTime += Time.deltaTime;
            unlockHandleTransform.rotation = Quaternion.Euler(0, 0, -elapsedTime / totalDuration * 360);
            yield return null;
        }
        while (elapsedTime < totalDuration)
        {
            unlockAllowed = true;
            elapsedTime += Time.deltaTime;
            unlockHandleTransform.rotation = Quaternion.Euler(0, 0, -elapsedTime / totalDuration * 360);
            yield return null;
        }
        unlockHandleTransform.rotation = Quaternion.Euler(0, 0, 0);
        StartCoroutine(UnlockMinigameCycle());
    }

    public void AttemptUnlock(GameObject player)
    {
        if (unlockAllowed)
        {
            player.GetComponent<PlayerMovement>().MovementLocked = false;
            gameObject.SetActive(false); // TODO: animate door opening and change only collider
        }
    }
}
