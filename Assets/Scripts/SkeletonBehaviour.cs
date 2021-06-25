using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonBehaviour : MonoBehaviour
{
    private bool followingPlayer_ = false;
    private Animator animator_;
    private GameObject player_;
    [SerializeField] private float healPoints = 8.0f;
    private Rigidbody2D rigidBody;
    

    private void Start()
    {
        animator_ = gameObject.GetComponent<Animator>();
        rigidBody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        animator_.SetFloat("Speed", rigidBody.velocity.sqrMagnitude);
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
}
