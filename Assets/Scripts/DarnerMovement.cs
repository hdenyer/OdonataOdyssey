using UnityEngine;

public class DarnerMovement : MonoBehaviour
{
    public float moveSpeed = 10f; // Speed of movement

    private Rigidbody2D rb;
    private Vector2 input;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        input = Vector2.zero;
        float currentY = transform.position.y;
        float currentX = transform.position.x;

        if (Input.GetKey(KeyCode.UpArrow) && currentY < 4.5f)
        {
            input.y += 1;
        }
        if (Input.GetKey(KeyCode.DownArrow) && currentY > -4.5f)
        {
            input.y -= 1;
        }
        if (Input.GetKey(KeyCode.LeftArrow) && currentX > -8f)
        {
            input.x -= 1;
        }
        if (Input.GetKey(KeyCode.RightArrow) && currentX < 7.4f)
        {
            input.x += 1;
        }
    }

    void FixedUpdate()
    {
        rb.velocity = input.normalized * moveSpeed;
    }
}
