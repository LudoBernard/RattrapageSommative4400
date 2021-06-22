using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;

public class PlayerCharacter : MonoBehaviour
{
    private enum State
    {
        NONE,
        Idle,
        Walk,
        Attack
    }
    [SerializeField] private Animator animator_;
    [SerializeField] private Transform playerSprite;
    [SerializeField] private AudioClip walkFx;
    [SerializeField] private AudioClip attackFx;
    [SerializeField] private Rigidbody2D body;

    private const float Speed = 5.0f;
    private State currentState_;
    private bool isFacingRight_ = false;
    private float deadZone_ = 0.1f;
    private Vector2 movement_;
    
    // Start is called before the first frame update
    void Start()
    {
        body.GetComponent<Rigidbody2D>();
        currentState_ = State.Idle;
    }

    // Update is called once per frame
    void Update()
    {
        movement_.x = Input.GetAxis("Horizontal");
        movement_.y = Input.GetAxis("Vertical");
    }

    private void FixedUpdate()
    {
        body.MovePosition(body.position + movement_ * Speed * Time.deltaTime);

        if (Input.GetAxis("Horizontal") > deadZone_ && isFacingRight_)
        {
            Flip();
        }

        if (Input.GetAxis("Horizontal") < -deadZone_ && !isFacingRight_)
        {
            Flip();
        }

        switch (currentState_)
        {
            case State.Idle:
                if (Mathf.Abs(Input.GetAxis("Horizontal")) > deadZone_)
                {
                    ChangeState(State.Walk);
                }
                
                if (Mathf.Abs(Input.GetAxis("Vertical")) > deadZone_)
                {
                    ChangeState(State.Walk);
                }

                if (Input.GetKey(KeyCode.Space))
                {
                    ChangeState(State.Attack);
                    break;
                }
                break;
            case State.Walk:
                if ((Input.GetAxis("Vertical") > -deadZone_ && Input.GetAxis("Vertical") < deadZone_)
                    && Input.GetAxis("Horizontal") > -deadZone_ && Input.GetAxis("Horizontal")< deadZone_)
                {
                    ChangeState(State.Idle);
                }

                if (Input.GetKey(KeyCode.Space))
                {
                    ChangeState(State.Attack);
                }
                break;
            case State.Attack:
                
                if (Mathf.Abs(Input.GetAxis("Horizontal")) > deadZone_)
                {
                    ChangeState(State.Walk);
                }
                break;
        }
    }

    private void Flip()
    {
        Vector3 newScale = playerSprite.transform.localScale;
        newScale.x *= -1;
        playerSprite.transform.localScale = newScale;
        isFacingRight_ = !isFacingRight_;
    }

    private void ChangeState(State state)
    {
        switch (state)
        {
            case State.Idle:
                animator_.Play("Idle1");
                break;
            case State.Walk:
                animator_.Play("Walk");
                break;
            case State.Attack:
                animator_.Play("Attack");
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(state), state, null);  
        }

        currentState_ = state;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Objectif"))
        {
            FindObjectOfType<GameManager>().Winnig();
        }
    }
}
