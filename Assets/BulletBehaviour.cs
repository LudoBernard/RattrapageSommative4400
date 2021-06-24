using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBehaviour : MonoBehaviour
{
    private GameObject player_;
    void Awake()
    {
        player_ = GameObject.FindWithTag("Player");
        Physics2D.IgnoreCollision(player_.GetComponent<BoxCollider2D>(),
            gameObject.GetComponent<CircleCollider2D>(), true);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Cell") ||
            other.gameObject.CompareTag("Skeleton") ||
            other.gameObject.CompareTag("Slime"))
        {
            Destroy(gameObject);
        }
    }
}
