using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float JumpForce = 6f;
    [SerializeField] private float MoveForce = 2f;

    private Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();    
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
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
