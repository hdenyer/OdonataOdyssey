using UnityEngine;
using UnityEngine.SceneManagement;

public class BootSceneManager : MonoBehaviour
{
    public GameObject startScreen;
    private AudioManager audioManager;

    private bool hasStarted = false; // Prevent multiple activations

    void Start()
    {
        Time.timeScale = 0;
        audioManager = FindObjectOfType<AudioManager>();
        if (audioManager != null)
            audioManager.PlayStartMusic();

        startScreen.SetActive(true);
    }

    void Update()
    {
        if (!hasStarted && Input.GetKeyDown(KeyCode.Space))
        {
            OnStartButtonPressed();
        }

        if (!hasStarted && Input.GetKeyDown(KeyCode.Alpha1))
        {
            SceneManager.LoadScene("Level_1");
        }
    }

    public void OnStartButtonPressed()
    {
        hasStarted = true; // Lock input
        Time.timeScale = 1;
        SceneManager.LoadScene("Level_0");
    }
}
