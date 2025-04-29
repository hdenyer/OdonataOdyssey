using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TroutSwimming : MonoBehaviour
{
    public float amplitude = 1f;       // How far up/down the trout swims
    public float frequency = 1f;       // How fast the wave oscillates
    public float verticalOffset = 0f;  // Y offset from anchor

    private Transform anchor;
    private float timeOffset;
    private GameManager gameManager;

    public bool isTesting = false;

    void Start()
    {
        anchor = transform.parent;
        gameManager = FindObjectOfType<GameManager>();
        timeOffset = Random.Range(0f, 2f * Mathf.PI); // Optional: offset for natural variation
    }

    void Update()
    {
        if ((gameManager != null && gameManager.IsGameActive) || isTesting)
        {
            float yOffset = Mathf.Sin(Time.time * frequency + timeOffset) * amplitude + verticalOffset;
            Vector3 newPos = new Vector3(anchor.position.x, anchor.position.y + yOffset, anchor.position.z);
            transform.position = newPos;
        }
    }
}

