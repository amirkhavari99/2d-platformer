using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float JumpForce = 6f;
    [SerializeField] private float MoveForce = 2f;
    [SerializeField] private float MovingEpsilon = 0.1f;
    [SerializeField] private LayerMask floor;

    private SpriteRenderer sr;
    private Rigidbody2D rb;
    private BoxCollider2D boxCollider;
    private Animator animator;

    private enum PlayerState
    {
        Idle = 0,
        Running = 1,
        Jump = 2,
        Fall = 3
    }
    private PlayerState currentState;

    // Start is called before the first frame update
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();    
        rb = GetComponent<Rigidbody2D>();    
        boxCollider = GetComponent<BoxCollider2D>();
        animator = GetComponent<Animator>();    
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && IsOnTheFloor())
        {
            rb.velocity = new Vector2(rb.velocity.x, JumpForce);
        }

        if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
        {
            rb.velocity = new Vector2(-MoveForce, rb.velocity.y);
        }

        if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
        {
            rb.velocity = new Vector2(MoveForce, rb.velocity.y);
        }

        UpdateAnimation();
    }

    private void UpdateAnimation()
    {
        PlayerState state;
        if (rb.velocity.x > MovingEpsilon)
        {
            state = PlayerState.Running;
            sr.flipX = false;
        }
        else if (rb.velocity.x < -MovingEpsilon)
        {
            state = PlayerState.Running;
            sr.flipX = true;
        }
        else
        {
            state = PlayerState.Idle;
        }

        if (rb.velocity.y > MovingEpsilon)
        {
            state = PlayerState.Jump;
        }
        else if (rb.velocity.y < -MovingEpsilon)
        {
            state = PlayerState.Fall;
        }

        SetAnimationState(state);
    }

    private void SetAnimationState(PlayerState state)
    {
        if (currentState != state)
        {
            Debug.LogError($"Setting Player State to {state}");
            animator.SetInteger("playerState", (int)state);
            currentState = state;
        }
    }

    private bool IsOnTheFloor()
    {
        return Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0f, Vector2.down, 0.1f, floor);
    }

    public Vector2 GetCurrentPosition()
    {
        return transform.position;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Collectible"))
        {
            LevelController.Instance.PlayerGotAPoint(1);
            Destroy(collision.gameObject);
        }
    }
}
