using System.Collections;
using UnityEngine;

public class NymphPlayerCollision : MonoBehaviour
{
    private Rigidbody2D rb;
    private GameManager gameManager;
    private bool gameOver = false;

    public AudioClip eatenSFX; // Assign in Inspector
    public AudioClip eating;
    public AudioClip point;
    private AudioSource audioSource;
    private Animator animator;
    private bool transitioningToNextLevel = false;  

    void Start()
{
    rb = GetComponent<Rigidbody2D>();
    gameManager = FindObjectOfType<GameManager>();
    audioSource = GetComponentInChildren<AudioSource>(); // ✅ This works even if it's a child
    animator = GetComponent<Animator>();
}


    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("FinishLine") && !transitioningToNextLevel)
        {
            transitioningToNextLevel = true;
            StartCoroutine(HandleLevelTransition());
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
        // Stop movement
        rb.velocity = Vector2.zero;
        rb.gravityScale = 0f;

        // Disable scrolling
        var grassMovement = FindObjectOfType<GrassMovement>();
        if (grassMovement != null) grassMovement.enabled = false;

        // Play eaten sound
        if (audioSource != null && eatenSFX != null)
        {
            audioSource.PlayOneShot(eatenSFX);
        }

        // Hide the nymph and all children
        foreach (var renderer in GetComponentsInChildren<SpriteRenderer>())
        {
            renderer.enabled = false;
        }

        // Wait for the audio to finish or a short delay
        yield return new WaitForSeconds(1f);

        // Trigger Game Over UI
        if (gameManager != null)
        {
            gameManager.ShowGameOverScreen();
        }
    }

    private IEnumerator HandleLevelTransition()
    {
        // Stop normal player control
        rb.velocity = Vector2.zero;
        rb.bodyType = RigidbodyType2D.Kinematic; // Optional: prevent physics glitches
        gameManager.IsGameActive = false;

        // Rotate 90° CCW
        transform.rotation = Quaternion.Euler(0, 0, 90);

        // Trigger upward crawl animation
        if (animator != null)
        {
            animator.SetFloat("Speed", 1f); // Or however your crawl animation is triggered
        }

        // Move upward off screen
        float crawlSpeed = 2f;
        float crawlDuration = 2f;
        float elapsed = 0f;

        while (elapsed < crawlDuration)
        {
            transform.position += Vector3.up * crawlSpeed * Time.deltaTime;
            elapsed += Time.deltaTime;
            yield return null;
        }

        // Now load Level_1
        if (gameManager != null)
        {
            gameManager.LoadLevel("Level_1");
        }
    }

}


