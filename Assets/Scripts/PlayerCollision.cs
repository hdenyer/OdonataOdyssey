using System.Collections;
using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    private Rigidbody2D rb;
    private GameManager gameManager;
    private bool gameOver = false;
    private AudioSource audioSource;
    public AudioClip eatenSFX; // Assign in Inspector
    public AudioClip eating;
    public AudioClip point;

    private Transform kiss; // Track kiss so we can setActive false on start

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        gameManager = FindObjectOfType<GameManager>();
        audioSource = GetComponentInChildren<AudioSource>();

        // Find the kiss object once at start
        kiss = transform.Find("Kiss");
        if (kiss != null)
        {
            kiss.gameObject.SetActive(false); // ✅ Ensure kiss is hidden at game start
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("EndGame"))
        {
            StartCoroutine(HandleEndGameSequence());
        }

        if (other.gameObject.CompareTag("Enemy") && !gameOver)
        {
            gameOver = true;
            StartCoroutine(GameOverSequence());
        }
        else if (gameManager != null)
        {
            if (other.CompareTag("Fly"))
            {
                gameManager.IncrementScore(25);
                if (audioSource != null && point != null)
                {
                    audioSource.PlayOneShot(point);
                }
                Destroy(other.gameObject);
            }
            else if (other.CompareTag("Mosquito"))
            {
                gameManager.IncrementScore(50);
                if (audioSource != null && point != null)
                {
                    audioSource.PlayOneShot(point);
                }
                Destroy(other.gameObject);
            }
            else if (other.CompareTag("Butterfly"))
            {
                gameManager.IncrementScore(250);
                if (audioSource != null && point != null)
                {
                    audioSource.PlayOneShot(point);
                }
                Destroy(other.gameObject);
            }
        }
    }

    private IEnumerator GameOverSequence()
    {
        rb.velocity = Vector2.zero;
        rb.gravityScale = 1f;

        if (audioSource != null && eatenSFX != null)
        {
            audioSource.PlayOneShot(eatenSFX);
        }

        transform.rotation = Quaternion.Euler(0, 0, 180);

        yield return new WaitForSeconds(0.5f);

        if (gameManager != null)
        {
            gameManager.ShowGameOverScreen();
        }
    }

    private IEnumerator HandleEndGameSequence()
    {
        // Freeze everything
        rb.velocity = Vector2.zero;
        rb.bodyType = RigidbodyType2D.Kinematic;
        gameManager.IsGameActive = false;

        GameObject femaleDarner = GameObject.FindGameObjectWithTag("FemaleDarner");

        if (femaleDarner != null)
        {
            Rigidbody2D femaleRb = femaleDarner.GetComponent<Rigidbody2D>();
            if (femaleRb != null)
            {
                femaleRb.bodyType = RigidbodyType2D.Kinematic;
            }

            // Move female toward player
            Vector3 startFemalePos = femaleDarner.transform.position;
            Vector3 playerPos = transform.position + new Vector3(1.5f, 0f, 0f);

            float approachDuration = 2f;
            float elapsed = 0f;

            while (elapsed < approachDuration)
            {
                elapsed += Time.deltaTime;
                float t = elapsed / approachDuration;

                femaleDarner.transform.position = Vector3.Lerp(startFemalePos, playerPos, t);

                yield return null;
            }
        }

        // Show the kiss heart
        if (kiss != null)
        {
            kiss.gameObject.SetActive(true);
        }

        // Play smooch sound using the player's AudioSource!
        if (audioSource != null && gameManager.smoochClip != null)
        {
            audioSource.PlayOneShot(gameManager.smoochClip);
        }

        // Wait for smooch clip to finish (or fallback if null)
        yield return new WaitForSecondsRealtime(gameManager.smoochClip != null ? gameManager.smoochClip.length : 1f);

        // Show the "You Win" screen
        if (gameManager != null)
        {
            gameManager.ShowWinScreen();
        }
    }
}
