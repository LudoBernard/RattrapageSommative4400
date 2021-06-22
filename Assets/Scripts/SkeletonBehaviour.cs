using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonBehaviour : MonoBehaviour
{
    private enum State
    {
        NONE,
        Idle,
        Walk,
        Dead
    }
    
    private bool followingPlayer_ = false;
    private State currentState_;
    private Animator animator_;
    private GameObject player_;
    [SerializeField] private float moveSpeed_ = 5.0f;
    [SerializeField] private float healPoints = 8.0f;
    private Rigidbody2D rigidBody;
    

    private void Start()
    {
        player_ = GameObject.FindWithTag("Player");
        animator_ = gameObject.GetComponent<Animator>();
        rigidBody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (followingPlayer_ == true)
        {
            Chase();
        }
    }

    private void FixedUpdate()
    {
        switch (currentState_)
        {
            case State.Idle:
                if (followingPlayer_ == true)
                {
                    ChangeState(State.Walk);
                }

                if (healPoints <= 0)
                {
                    ChangeState(State.Dead);
                }
                break;
            case State.Walk:
                if (!followingPlayer_ == true)
                {
                    ChangeState(State.Idle);
                }
                
                if (healPoints <= 0)
                {
                    ChangeState(State.Dead);
                }

                break;
        }
    }

    private void Chase()
    {
        rigidBody.velocity = (player_.transform.position - transform.position).normalized * moveSpeed_;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("Following Player!");
            followingPlayer_ = true;
        }
    }
    
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("Stopped following Player and moving randomly!");
            followingPlayer_ = false;
        }
    }
    
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("Bumped into player!");
            followingPlayer_ = false;
        }
    }
    
    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("Exited collision with player and resumes chasing!");
            followingPlayer_ = true;
        }
    }
    
    public void CalculateDamage(float damage)
    {
        healPoints = healPoints - damage;
        animator_.Play("Hit");
        if (healPoints <= 0)
        {
            Destroy(gameObject);
        }
        
    }
    
    private void ChangeState(State state)
    {
        switch (state)
        {
            case State.Idle:
                animator_.Play("Idle");
                break;
            case State.Walk:
                animator_.Play("Walk");
                break;
            case State.Dead:
                animator_.Play("Death");
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(state), state, null);  
        }

        currentState_ = state;
    }
}
