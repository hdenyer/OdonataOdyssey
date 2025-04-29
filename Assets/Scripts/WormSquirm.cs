using UnityEngine;

public class WormSquirm : MonoBehaviour
{
    public float orbitRadius = 1f;         // Distance from anchor
    public float orbitSpeed = 1f;          // Speed of orbit around anchor
    public float selfRotationSpeed = 180f; // Rotation speed on own axis (degrees/sec)

    private Transform anchor;
    private GameManager gameManager;
    private float orbitAngle = 0f;

    public bool isTesting = false;

    void Start()
    {
        anchor = transform.parent;
        gameManager = FindObjectOfType<GameManager>();
        orbitAngle = Random.Range(0f, 360f); // Random start angle for variation
    }

    void Update()
    {
        if ((gameManager != null && gameManager.IsGameActive) || isTesting)
        {
            // 1. Orbit around the anchor
            orbitAngle += orbitSpeed * Time.deltaTime;
            if (orbitAngle > 360f) orbitAngle -= 360f;

            float rad = orbitAngle * Mathf.Deg2Rad;
            float xOffset = Mathf.Cos(rad) * orbitRadius;
            float yOffset = Mathf.Sin(rad) * orbitRadius;

            Vector3 offset = new Vector3(xOffset, yOffset, 0f);
            transform.position = anchor.position + offset;

            // 2. Rotate on its own axis
            transform.Rotate(Vector3.forward, selfRotationSpeed * Time.deltaTime);
        }
    }
}
