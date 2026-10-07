using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{

    public float speed = 0f;
    public Vector2 direction;
    private Rigidbody2D rb;
    //private bool isGrounded = true;

    private SpriteRenderer spriter; // 2D-nél a kép merre néz 

    private bool hasSnowball;
    //private float jumpVelocity = 12f;

    public GameObject snowball;

    public Enemy enemy;

    public int hp = 10;
    //private Vector2 distanceToEnemy;

    //private int cherrycounter = 0;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>(); //Ami obejctre rá van húzva a script arra rá húz mindent, független ha prívált
        spriter = GetComponent<SpriteRenderer>();

        if (rb != null)
        {
            rb.gravityScale = 0;
            rb.freezeRotation = true;
        }


    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (hasSnowball || !collision.gameObject.CompareTag("SnowPile")) return;

        hasSnowball = true;
        Destroy(collision.gameObject);

        //if (collision.gameObject.GetComponent<Rigidbody2D>() != null
        //    && collision.gameObject.CompareTag("Cherry"))
        //{
        //    cherrycounter++;
        //    Destroy(collision.gameObject);
        //}



    }

    public void TakeDamage(int damage)
    {
        hp -= damage;
        Debug.Log($"Taken {damage} amount of damage");
        if (hp <= 0)
        {
            SceneManager.LoadScene("SampleScene");
        }
    }
    void Update()
    {
        PlayerMovement();
        if (rb != null)
        {
            rb.linearVelocity = direction * speed;
        }
        else
        {
            transform.position += (Vector3)direction * speed * Time.deltaTime;
        }
        //isGrounded = Physics2D.Raycast(transform.position, Vector2.down, 0.12f);

        //GroundedCheck();


    }

    public void PlayerMovement()
    {
        float v = 0f;


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

        if (Keyboard.current.wKey.isPressed)
        {
            v = 1f;
        }
        else if (Keyboard.current.sKey.isPressed)
        {
            v = -1f;
        }

        if (h != 0f || v != 0f)
        {
            speed = 2f;
            direction = new Vector2(h, v).normalized;
        }
        else
        {
            speed = 0f;
            direction = Vector2.zero;
        }

        if ((Keyboard.current.eKey.wasPressedThisFrame || Mouse.current.leftButton.wasPressedThisFrame) && hasSnowball)
        {
            Vector2 mouse = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());

            Vector2 aimDirection = mouse - (Vector2)transform.position;

            GameObject ball = Instantiate(snowball, transform.position, Quaternion.identity); //Prefebet létrehoz a hiearhiában !
            ball.GetComponent<SnowBall>().Launch(aimDirection, true);
            hasSnowball = false;
        }

        //if (Keyboard.current.wKey.wasPressedThisFrame && isGrounded == true)
        //{
        //    if (rb != null)
        //    {
        //        rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpVelocity);
        //    }
        //    else
        //    {
        //        transform.position += Vector3.up * (jumpVelocity * 0.1f);
        //    }
        //    isGrounded = false;
        //}
    }

    //private void Attack()
    //{
    //    enemy.TakeDamage(5);
    //}

    //private void GroundedCheck()
    //{
    //    if (rb.linearVelocityY == 0)
    //    {
    //        isGrounded = true;
    //        Debug.Log("A földön vagy !");
    //    }

    //}
}
