using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonBehaviour : MonoBehaviour
{
    private bool followingPlayer_ = false;
    private Animator animator_;
    private GameObject player_;
    [SerializeField] private float moveSpeed_ = 5.0f;
    [SerializeField] private float healPoints = 8.0f;
    private Rigidbody2D rigidBody;

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
