using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    // components
    [SerializeField] private GameObject failureScreen;
    [SerializeField] private GameObject pauseManagerPrefab;

    // data
    private PlayerMovement[] players;

    public static GameManager instance;

    // light info
    public LightTime lightTime;
    private float CurrentDayRatio;
    [SerializeField]
    private TimeSlot currentTimeSlot;

    private void Start()
    {
        instance = this;
        if (FindObjectOfType<PauseManager>() == null)
        {
            Instantiate(pauseManagerPrefab);
        }

        players = FindObjectsOfType<PlayerMovement>();
        ProgressTime();
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
        //debug time progression
        if (Input.GetKeyDown(KeyCode.T))
        {
            currentTimeSlot = currentTimeSlot + 1;
            ProgressTime();
        }
    }

    public void LoadNextLevel(string sceneName, float delay)
    {
        StartCoroutine(LoadNextLevelCoroutine(sceneName, delay));
    }

    public IEnumerator LoadNextLevelCoroutine(string sceneName, float delay)
    {
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene(sceneName);
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

    public void ProgressTime() //call this function to progress to the selected timeslot. if you want to update the time through smaller increments, just call lightTime.Tick with your custom time.
    {
        switch (currentTimeSlot)
        {
            case TimeSlot.Noon:
                CurrentDayRatio = .5f;
                break;
            case TimeSlot.Afternoon:
                CurrentDayRatio = .63f;
                break;
            case TimeSlot.Evening:
                CurrentDayRatio = 0.76f;
                break;
            case TimeSlot.Night:
                CurrentDayRatio = 0.96f;
                break;
            default:
                currentTimeSlot = TimeSlot.Noon;
                CurrentDayRatio = .5f;
                Debug.Log("Time progressed past night, resetting to noon!");
                break;
        }
        lightTime.Tick(CurrentDayRatio);
    }

    public enum TimeSlot
    {
        Noon,
        Afternoon,
        Evening,
        Night
    }
}
