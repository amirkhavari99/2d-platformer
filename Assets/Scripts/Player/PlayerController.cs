using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float JumpForce = 6f;
    [SerializeField] private float MoveForce = 2f;
    [SerializeField] private LayerMask floor;

    private Rigidbody2D rb;
    private BoxCollider2D boxCollider;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();    
        boxCollider = GetComponent<BoxCollider2D>();    
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
