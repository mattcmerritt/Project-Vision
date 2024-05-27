using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class Room : MonoBehaviour
{
    [SerializeField] private Light2D roomLight;
    private float lightTransitionDuration = 0.5f;

    // data for whether a player is in the room
    private bool visible;
    public bool Visible
    {
        get { return this.visible; }
        private set { this.visible = value; }
    }
    private int playersInRoom = 0;

    // data to control when the lights are off (still need to be slightly on to show room)
    [SerializeField] private bool lightsOn = true;
    public bool LightsOn
    {
        get { return this.lightsOn; }
        set { 
            this.lightsOn = value; 
            if (this.visible)
            {
                StartCoroutine(AdjustLight());
            }
        }
    }
    private float lightsOffIntensity = 0.3f;

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
            roomLight.intensity = Mathf.Lerp(0f, LightsOn ? 1f : lightsOffIntensity, elapsedTime / lightTransitionDuration);
            yield return null;
        }
        roomLight.intensity = LightsOn ? 1f : lightsOffIntensity;
        Visible = true;
    }

    private IEnumerator DisableLight()
    {
        Visible = false;
        float elapsedTime = 0f;
        while (elapsedTime < lightTransitionDuration)
        {
            elapsedTime += Time.deltaTime;
            roomLight.intensity = Mathf.Lerp(LightsOn ? 1f : lightsOffIntensity, 0f, elapsedTime / lightTransitionDuration);
            yield return null;
        }
        roomLight.intensity = 0f;
    }

    public IEnumerator AdjustLight()
    {
        float elapsedTime = 0f;
        while (elapsedTime < lightTransitionDuration)
        {
            elapsedTime += Time.deltaTime;
            roomLight.intensity = Mathf.Lerp(LightsOn ? lightsOffIntensity : 1f, LightsOn ? 1f : lightsOffIntensity, elapsedTime / lightTransitionDuration);
            yield return null;
        }
        roomLight.intensity = lightsOffIntensity;
    }
}
