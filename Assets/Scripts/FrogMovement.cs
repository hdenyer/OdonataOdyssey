using System.Collections;
using UnityEngine;

public class FrogMovement : MonoBehaviour
{
    public float kickDistance = 0.5f;     // How far forward the frog lunges left
    public float kickFrequency = 1f;      // How fast the frog kicks (in Hz)
    public float smoothness = 1f;         // Controls how soft or punchy the motion is

    private Transform anchor;
    private GameManager gameManager;
    private float timeOffset;

    public bool isTesting = false;

    void Start()
    {
        anchor = transform.parent;
        gameManager = FindObjectOfType<GameManager>();
        timeOffset = Random.Range(0f, 2f * Mathf.PI); // To desync if needed
    }

    void Update()
    {
        if ((gameManager != null && gameManager.IsGameActive) || isTesting)
        {
            // Use a ping-pong sine wave to simulate rhythmic forward kick
            float kickOffset = Mathf.Sin((Time.time + timeOffset) * kickFrequency * Mathf.PI * 2f);
            float easedOffset = Mathf.SmoothStep(0, 1, (kickOffset + 1f) / 2f); // Normalize and ease

            float xOffset = -easedOffset * kickDistance; // Negative X = leftward kick
            Vector3 newPos = new Vector3(anchor.position.x + xOffset, anchor.position.y, anchor.position.z);
            transform.position = newPos;
        }
    }
}
