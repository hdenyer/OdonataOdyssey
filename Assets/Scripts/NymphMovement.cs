using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NymphMovement : MonoBehaviour
{
    public float verticalSpeed = 5f;
    public Transform jaw; // Assign jaw GameObject (child of nymph)
    public float lungeAmount = 2f; // How far the jaw lunges right
    public float lungeDuration = 0.2f; // Total duration of lunge and retract
    public Animator animator;

    private Rigidbody2D rb;
    private bool isLunging = false;
    private Vector3 jawInitialLocalPos;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        if (jaw != null)
        {
            jawInitialLocalPos = jaw.localPosition;
            jaw.gameObject.SetActive(false);
        }
    }

    void Update()
{
    float verticalInput = 0f;
    float currentY = transform.position.y;

    if (Input.GetKey(KeyCode.UpArrow) && currentY < 4.5f)
    {
        verticalInput = 1f;
    }
    else if (Input.GetKey(KeyCode.DownArrow) && currentY > -4.5f)
    {
        verticalInput = -1f;
    }

    rb.velocity = new Vector2(0, verticalInput * verticalSpeed);
    animator.SetFloat("Speed", Mathf.Abs(verticalInput));

    // Jaw lunge
    if (Input.GetKeyDown(KeyCode.RightArrow) && !isLunging && jaw != null)
    {
        StartCoroutine(LungeJaw());
    }
}


    private IEnumerator LungeJaw()
    {
        isLunging = true;
        jaw.gameObject.SetActive(true);

        // Move jaw to the right (local space)
        Vector3 lungeTarget = jawInitialLocalPos + new Vector3(lungeAmount, 0f, 0f);
        jaw.localPosition = lungeTarget;

        yield return new WaitForSeconds(lungeDuration);

        // Retract jaw
        jaw.localPosition = jawInitialLocalPos;
        jaw.gameObject.SetActive(false);

        isLunging = false;
    }
}
