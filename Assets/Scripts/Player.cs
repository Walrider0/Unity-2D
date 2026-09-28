using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{

    public float speed = 0f;
    public Vector2 direction;
    private Rigidbody2D rb;
    private bool isGrounded = true;

    private SpriteRenderer spriter; // 2D-nél a kép merre néz 

    private float jumpVelocity = 12f;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>(); //Ami obejctre rá van húzva a script arra rá húz mindent, független ha prívált
        spriter = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        PlayerMovement();
        if (rb != null)
        {
            rb.linearVelocity = new Vector2(direction.x, rb.linearVelocity.y);
        }
        else
        {
            transform.position += (Vector3)direction * speed * Time.deltaTime;
        }
        //isGrounded = Physics2D.Raycast(transform.position, Vector2.down, 0.12f);

        GroundedCheck();
    }

    public void PlayerMovement()
    {
        float h = 0f;
        if (Keyboard.current.dKey.isPressed) //input.GetKeyDown(KeyCode.D) - A régi megolás
        {
            h = 1f;
            spriter.flipX = false;
        }
        else if (Keyboard.current.aKey.isPressed)
        {
            h = -1f;
            spriter.flipX = true;
        }
        if (h != 0)
        {
            speed = 2f;
            direction = new Vector2(h, 0f);
        }
        else
        {
            speed = 0f;
            direction = Vector2.zero;
        }

        if (Keyboard.current.wKey.wasPressedThisFrame && isGrounded == true)
        {
            if (rb != null)
            {
                rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpVelocity);
            }
            else
            {
                transform.position += Vector3.up * (jumpVelocity * 0.1f);
            }
            isGrounded = false;
        }
    }

    private void GroundedCheck()
    {
        if (rb.linearVelocityY == 0)
        {
            isGrounded = true;
            Debug.Log("A földön vagy !");
        }

    }
}
