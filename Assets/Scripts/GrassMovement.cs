using UnityEngine;

public class GrassMovement : MonoBehaviour
{
    public float scrollSpeed = 2f;
    public bool isTesting = false;

    private GameManager gameManager;

    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
    }

    void Update()
    {
        if ((gameManager != null && gameManager.IsGameActive) || isTesting)
        {
            transform.Translate(Vector2.left * scrollSpeed * Time.deltaTime);
        }
    }
}

