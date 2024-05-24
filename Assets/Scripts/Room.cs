using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class Room : MonoBehaviour
{
    [SerializeField] private Light2D roomLight;
    private float lightTransitionDuration = 0.5f;

    private bool visible;
    public bool Visible
    {
        get { return this.visible; }
        private set { this.visible = value; }
    }

    private int playersInRoom = 0;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<PlayerMovement>())
        {
            if (playersInRoom == 0)
            {
                StartCoroutine(EnableLight());
            }
            playersInRoom++;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.GetComponent<PlayerMovement>())
        {
            playersInRoom--;
            if (playersInRoom == 0)
            {
                StartCoroutine(DisableLight());
            }
        }
    }

    private IEnumerator EnableLight()
    {
        float elapsedTime = 0f;
        while (elapsedTime < lightTransitionDuration)
        {
            elapsedTime += Time.deltaTime;
            roomLight.intensity = Mathf.Lerp(0f, 1f, elapsedTime / lightTransitionDuration);
            yield return null;
        }
        roomLight.intensity = 1f;
        Visible = true;
    }

    private IEnumerator DisableLight()
    {
        Visible = false;
        float elapsedTime = 0f;
        while (elapsedTime < lightTransitionDuration)
        {
            elapsedTime += Time.deltaTime;
            roomLight.intensity = Mathf.Lerp(1f, 0f, elapsedTime / lightTransitionDuration);
            yield return null;
        }
        roomLight.intensity = 0f;
    }
}
