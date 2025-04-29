using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnchorMovement : MonoBehaviour
{
    public float scrollSpeed = 2f;
    private GameManager gameManager;

    public bool isTesting = false;

    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
    }


    void Update()
    {
        if ((gameManager != null && gameManager.IsGameActive) || isTesting)
        {
            transform.position += Vector3.left * scrollSpeed * Time.deltaTime;
        }
    }
}
