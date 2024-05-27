using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Documents : Collectible
{
    [SerializeField] private string nextLevelScene;
    [SerializeField] private float sceneTransitionDelay;

    public override void Interact(GameObject player)
    {
        base.Interact(player);

        // TODO: play some sort of post mission briefing here

        // loads the next level
        GameManager.instance.LoadNextLevel(nextLevelScene, sceneTransitionDelay);

        // TODO: consider adding an extraction component to levels
    }
}
