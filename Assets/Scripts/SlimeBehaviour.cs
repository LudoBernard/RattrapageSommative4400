using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeBehaviour : MonoBehaviour
{
    private bool followingPlayer_ = false;
    [SerializeField] private float moveSpeed_ = 5.0f;
    [SerializeField] private GameObject player_;
    [SerializeField] private float healPoints = 8.0f;
    private Rigidbody2D rigidBody;

    private void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        player_ = GameObject.FindWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if (followingPlayer_ == true)
        {
            Chase();
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
        if (healPoints <= 0)
        {
            Destroy(gameObject);
        }
    }
    
}
