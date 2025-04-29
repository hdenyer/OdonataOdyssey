using UnityEngine;

public class TroutMovement : MonoBehaviour
{
    public float jumpHeight = 3f; // Maximum height of the leap
    public float jumpSpeed = 2f; // Speed of the leap
    public float orbitRadius = 1f; // Radius for the jump's arc

    private Transform anchor;
    private float angle = 0f; // Angle for the orbit
    private GameManager gameManager;

    void Start()
    {
        anchor = transform.parent; // The anchor point
        gameManager = FindObjectOfType<GameManager>();
    }

    void Update()
    {
        if (gameManager != null && gameManager.IsGameActive)
        {
            angle += jumpSpeed * Time.deltaTime; // Increment the angle

            // Calculate arc movement
            float offsetX = Mathf.Cos(angle) * orbitRadius;
            float offsetY = Mathf.Sin(angle) * jumpHeight;

            // Apply movement relative to the anchor's world position + this object's local position
            Vector3 basePosition = anchor.position + transform.localPosition;
            transform.position = basePosition + new Vector3(offsetX, offsetY, 0);

            // Reset angle after one cycle
            if (angle > Mathf.PI * 2) angle -= Mathf.PI * 2;
        }
    }
}
