using UnityEngine;

public class HummingbirdMovement : MonoBehaviour
{
    public float movementRadius = 2f; // Diameter of movement
    public float noiseScale = 1f; // Perlin noise scale
    public float speed = 5f; // Oscillation speed

    private Transform anchor;
    private GameManager gameManager;

    public bool isTesting = false;

    void Start()
    {
        anchor = transform.parent; // Anchor is the parent GameObject
        gameManager = FindObjectOfType<GameManager>();
    }

    void Update()
    {
        if ((gameManager != null && gameManager.IsGameActive) || isTesting)
        {
            // Calculate offset for movement
            float offsetX = (Mathf.PerlinNoise(Time.time * noiseScale, 0) - 0.5f) * movementRadius;
            float offsetY = Mathf.Sin(Time.time * speed) * movementRadius / 2f;

            // Apply movement relative to the anchor's world position + this object's local position
            Vector3 basePosition = anchor.position + transform.localPosition;
            transform.position = basePosition + new Vector3(offsetX, offsetY, 0);
        }
    }
}
