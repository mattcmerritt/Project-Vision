using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    // components
    [SerializeField] private GameObject failureScreen;

    // data
    private PlayerMovement[] players;

    public static GameManager instance;

    private void Start()
    {
        instance = this;

        players = FindObjectsOfType<PlayerMovement>();
    }

    private void Update()
    {
        bool canContinue = false;
        foreach (PlayerMovement player in players)
        {
            if (!player.Detained)
            {
                canContinue = true;
                break;
            }
        }

        if (!canContinue)
        {
            FailAndRestartLevel();
        }
    }

    public void FailAndRestartLevel()
    {
        StartCoroutine(FailAndRestartLevelCoroutine());
    }

    private IEnumerator FailAndRestartLevelCoroutine()
    {
        failureScreen.SetActive(true);
        yield return new WaitForSeconds(2);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
