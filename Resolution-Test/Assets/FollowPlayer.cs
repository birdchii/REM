using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    private Animator anim;
    private Collider2D coll;
    private enum State { idle, chase }
    private State state = State.idle;

    [SerializeField] public bool hostile = false;
    [SerializeField] private float speed = 5f;
    [SerializeField] private float acceleration = 0.1f;
    public float currentspeed = 0f;
    private Rigidbody2D player;

    private void Start()
    {
        anim = GetComponent<Animator>();
        coll = GetComponent<Collider2D>();
    }

    private void Update()
    {
        Movement();
        AnimationState();
        anim.SetInteger("state", (int)state);
    }

    private void Movement()
    {
        if (hostile)
        {
            player = GameObject.Find("Player").GetComponent<Rigidbody2D>();
            transform.position = Vector2.MoveTowards(transform.position, player.position, speed * Time.deltaTime);

        }
    }

    private void AnimationState()
    {
        if (hostile)
        {
            state = State.chase;
        }
        else
        {
            state = State.idle;
        }
    }
}
