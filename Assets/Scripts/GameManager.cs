using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public GameObject loadingScreen0;
    public GameObject loadingScreen1;
    public GameObject loadingScreen2;
    public GameObject loadingScreen3;
    public GameObject loadingScreen4;
    public GameObject goScreen;
    public GameObject gameOverScreen;
    public GameObject youWinScreen; // Assign in Inspector
    public AudioClip smoochClip;     // Assign in Inspector

    public Text scoreText;

    private int score = 0;
    public bool IsGameActive;

    private AudioManager audioManager;
    private AudioSource audioSource;
    public AudioClip taDa;

    void Start()
    {
        audioSource = GetComponentInChildren<AudioSource>();

        Time.timeScale = 0;
        goScreen.SetActive(false);
        gameOverScreen.SetActive(false);
        youWinScreen.SetActive(false);

        loadingScreen1.SetActive(false);
        loadingScreen2.SetActive(false);
        loadingScreen3.SetActive(false);
        loadingScreen4.SetActive(false);

        score = 0; 
        UpdateScoreText();

        StartCoroutine(LoadingSequence());
    }

    void Update()
    {
        if ((gameOverScreen.activeSelf || youWinScreen.activeSelf) && Input.GetKeyDown(KeyCode.Space))
        {
            Time.timeScale = 1;
            SceneManager.LoadScene("BootScene");
        }

        if (Input.GetKeyDown(KeyCode.Escape)) {
            SceneManager.LoadScene("BootScene");
        }
    }


    public void LoadLevel(string levelName)
    {
        Time.timeScale = 1;
        IsGameActive = false;
        SceneManager.LoadScene(levelName);
    }

    private IEnumerator LoadingSequence()
{
    audioManager = FindObjectOfType<AudioManager>();
    if (audioManager != null)
    {
        string currentScene = SceneManager.GetActiveScene().name;
        if (currentScene == "Level_0") audioManager.PlayLevelZeroMusic();
        if (currentScene == "Level_1") audioManager.PlayLevelOneMusic();
    }

    string sceneName = SceneManager.GetActiveScene().name; // Move currentScene outside of foreach
    GameObject[] loadingScreens = { loadingScreen0, loadingScreen1, loadingScreen2, loadingScreen3 };

    for (int i = 0; i < loadingScreens.Length; i++)
    {
        GameObject screen = loadingScreens[i];
        screen.SetActive(true);

        float timer = 0f;
        while (timer < 5f)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                foreach (GameObject s in loadingScreens)
                {
                    s.SetActive(false);
                }

                goScreen.SetActive(true);
                StartCoroutine(HandleGoScreen());
                yield break;
            }

            timer += Time.unscaledDeltaTime;
            yield return null;
        }

        // Play the sound when reaching loadingScreen2 in Level_1
        if (sceneName == "Level_1" && i == 1) // 0-based index (0, 1, **2**)
        {
            if (audioSource != null && taDa != null)
            {
                audioSource.PlayOneShot(taDa);
            }
        }

        screen.SetActive(false);
    }

    if (sceneName == "Level_1")
    {
        loadingScreen4.SetActive(true);
        yield return new WaitForSecondsRealtime(3f);
        loadingScreen4.SetActive(false);
    }

    // After loading screens finish normally
    goScreen.SetActive(true);
    StartCoroutine(HandleGoScreen());
}



    private IEnumerator HandleSceneStartFromSkip()
    {
        yield return new WaitForSecondsRealtime(1.5f); // GO screen delay
        goScreen.SetActive(false);

        Time.timeScale = 1;
        IsGameActive = true;
        UpdateScoreText();
    }

    private IEnumerator HandleSceneStart()
    {
        // Let the scene fully initialize
        yield return null;

        // Load music after one frame (ensures AudioManager exists)
        audioManager = FindObjectOfType<AudioManager>();
        if (audioManager != null)
        {
            string currentScene = SceneManager.GetActiveScene().name;
            if (currentScene == "Level_0") audioManager.PlayLevelZeroMusic();
            if (currentScene == "Level_1") audioManager.PlayLevelOneMusic();
        }

        // Show loading screen for 3 seconds
        yield return new WaitForSecondsRealtime(3f);
        loadingScreen3.SetActive(false);

        // Show GO screen for 1.5 seconds
        goScreen.SetActive(true);
        yield return new WaitForSecondsRealtime(1.5f);
        goScreen.SetActive(false);

        // Begin the game
        Time.timeScale = 1;
        IsGameActive = true;
        UpdateScoreText();
    }

    private IEnumerator HandleGoScreen()
    {
        yield return new WaitForSecondsRealtime(1.5f); // Only GO! screen time
        goScreen.SetActive(false);

        Time.timeScale = 1;
        IsGameActive = true;
        UpdateScoreText();
    }


    public void ShowGameOverScreen()
    {
    IsGameActive = false;
    Time.timeScale = 0;
    gameOverScreen.SetActive(true);

    if (audioManager != null)
    {
        audioManager.PlayGameOverMusic();
    }

    StartCoroutine(ReturnToBootSceneAfterDelay(15f));
    }

    private IEnumerator ReturnToBootSceneAfterDelay(float delay)
    {
        yield return new WaitForSecondsRealtime(delay);
        Time.timeScale = 1; // Unpause before changing scenes
        SceneManager.LoadScene("BootScene");
    }

    public void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void IncrementScore(int value)
    {
        if (IsGameActive)
        {
            score += value;
            UpdateScoreText();
        }
    }

    private void UpdateScoreText()
    {
        if (scoreText != null)
        {
            scoreText.text = score.ToString("D4");
        }
    }

    public void ShowWinScreen()
    {
        Time.timeScale = 0;
        youWinScreen.SetActive(true);

        if (audioManager != null)
        {
            audioManager.PlayGameOverMusic();
        }

        StartCoroutine(ReturnToBootSceneAfterDelay(20f));
    }
}
