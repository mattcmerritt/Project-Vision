using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject failureScreen;

    public static GameManager instance;

    private void Start()
    {
        instance = this;
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