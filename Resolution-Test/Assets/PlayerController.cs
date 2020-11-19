using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    //Start() variables
    private Rigidbody2D rb;
    private Animator anim;
    private Collider2D coll;

    public int candies = 0;

    //FSM
    private enum State { idle, running, jumping, falling }
    private State state = State.idle;

    //Inspector variables

    [SerializeField] private LayerMask ground;
    [SerializeField] private float speed = 5f;
    [SerializeField] private float acceleration = 0.1f;
    [SerializeField] private float jumpForce = 10f;
    [SerializeField] public int candy = 0;
    [SerializeField] private Text candyText;
    [SerializeField] public int hitDelay = 3;
    public Transform groundCheckPoint;
    public float groundCheckRadius;
    public float distancePerSecond;
    public float currentspeed = 0f;
    private float lastHit = 0f;

    public int health = 3;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        coll = GetComponent<Collider2D>();
    }
    private void Update()
    {

        Movement();
        AnimationState();
        anim.SetInteger("state", (int)state); //sets animation based on Enumerator state

        if (health <= 0)
        {
            transform.position = new Vector3(-1, 1, 0);
            health = 3;
            //Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Collectable")
        {
            Destroy(collision.gameObject);
            candies += 1;
            candyText.text = candies.ToString();
        }

        {
            if (coll.gameObject.name.Equals("Platform"))
                this.transform.parent = coll.transform;
        }

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if ((collision.gameObject.tag == ("Enemy")) && collision.gameObject.GetComponent<FollowPlayer>().hostile && (lastHit <= Time.time))
        {
            health = health - 1;
            lastHit = Time.time + hitDelay;
            GetComponent<PlayerBlink>().startBlinking = true;
            rb.velocity = new Vector2(rb.velocity.x, 12);
            rb.velocity = new Vector2(rb.velocity.y, 12);

            // transform.position = new Vector3(-1, 1, 0);
        }    
    }

    void OnCollisionExit2D(Collision2D coll)
    {
        if (coll.gameObject.name.Equals("Platform"))
            this.transform.parent = coll.transform;
    }


    private void Movement()
    {
        float hDirection = Input.GetAxis("Horizontal");
        float wDirection = Input.GetAxis("Vertical");
        Vector2 plyvel = rb.velocity;
        float modspeed = acceleration * Time.deltaTime;

        //Moving left
        if (hDirection < 0)
        {
            currentspeed = Mathf.Lerp(currentspeed, -speed, modspeed);
            plyvel.x = currentspeed;
            transform.localScale = new Vector2(1, 1);
        }
        //Moving right
        else if (hDirection > 0)
        {
            currentspeed = Mathf.Lerp(currentspeed, speed, modspeed);
            plyvel.x = currentspeed;
            transform.localScale = new Vector2(-1, 1);
        }
        else if (coll.IsTouchingLayers())
        {
            currentspeed = Mathf.Lerp(currentspeed, 0, modspeed * 20);
            plyvel.x = currentspeed;
        }

        rb.velocity = plyvel;


        //Jumping
        if (Input.GetButtonDown("Jump") && coll.IsTouchingLayers())
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            state = State.jumping;
        }

        //Sync to frametime
        transform.Translate(0, 0, distancePerSecond * Time.deltaTime);


    }
    private void AnimationState()
    {
        if (state == State.jumping)
        {
            if (rb.velocity.y < .1f)
            {
                state = State.falling;
            }
        }
        else if (state == State.falling)
        {
            if (coll.IsTouchingLayers(ground))
            {
                state = State.idle;
            }
        }

        else if (Mathf.Abs(rb.velocity.x) > 2f)
        {
            //Moving
            state = State.running;
        }
        else
        {
            state = State.idle;
        }
    }

   


}
