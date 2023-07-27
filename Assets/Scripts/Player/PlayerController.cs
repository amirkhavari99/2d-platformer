using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] [Tooltip("The force of the player jumps")] private float JumpForce = 6f;
    [SerializeField] [Tooltip("The force of the player move in direction X")] private float MoveForce = 2f;
    [SerializeField] [Tooltip("Moving less than this amount is considered Not Moving")] private float MovingEpsilon = 0.1f;
    [SerializeField] [Tooltip("The layermask of floors (platforms)")] private LayerMask floor;

    private SpriteRenderer sr;
    private Rigidbody2D rb;
    private BoxCollider2D boxCollider;
    private Animator animator;

    private bool finished = false;

    // Possible states of the player character (used same numbers for animation's int parameter)
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
        if (!LevelController.Instance.finished)// ask level controller if moving level is finished
        {
            if (Input.GetButtonDown("Jump") && IsOnTheFloor())
            {
                rb.velocity = new Vector2(rb.velocity.x, JumpForce);
            }

            float xAxisInput = Input.GetAxisRaw("Horizontal");
            rb.velocity = new Vector2(MoveForce * xAxisInput, rb.velocity.y);

            UpdateAnimation(xAxisInput);
        }
        else if(!finished)// dont want to set the velocity to zero more than once
        {
            rb.velocity = Vector2.zero;
            UpdateAnimation();
            finished = true;
        }
    }

    private void UpdateAnimation(float xAxisInput = 0)
    {
        // This functions updates the animation state of the player, Jump and Fall have a higher priority
        PlayerState state;
        if (xAxisInput > MovingEpsilon)
        {
            state = PlayerState.Running;
            sr.flipX = false;
        }
        else if (xAxisInput < -MovingEpsilon)
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
            //Debug.Log($"Setting Player State to {state}");
            animator.SetInteger("playerState", (int)state);
            currentState = state;
        }
    }

    private bool IsOnTheFloor()
    {
        // a boxCast that is a little lower than the main boxCollider of player, indicates whether is standing on a floor or not 
        return Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0f, Vector2.down, 0.1f, floor);
    }

    public Vector2 GetCurrentPosition()
    {
        //used for controlling the camera to follow the player
        return transform.position;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Collectible"))
        {
            // player got a collectible, notice level controller to increase the score
            LevelController.Instance.PlayerGotACollectible(collision.gameObject);
        }
        if (collision.gameObject.CompareTag("Finish"))
        {
            // player reached the final checkpoint of the level, notice level controller to finish the level
            LevelController.Instance.LevelCompleted();
        }
    }
}
