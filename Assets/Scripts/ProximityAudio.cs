using UnityEngine;

public class ProximityAudio : MonoBehaviour
{
    public Transform player;  // Reference to the player's transform
    public float maxDistance = 5f; // Maximum range where audio is heard
    private AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        if (audioSource != null)
        {
            audioSource.volume = 0;  // Start silent
            audioSource.Play();  // Play on loop but let script handle volume
        }
    }

    void Update()
    {
        if (player == null) return;

        float distance = Vector2.Distance(transform.position, player.position);
        float volume = Mathf.Clamp01(1 - (distance / maxDistance)); // Normalize volume
        audioSource.volume = volume;
    }
}
