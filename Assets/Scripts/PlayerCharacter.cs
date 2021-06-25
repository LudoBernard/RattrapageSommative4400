using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerCharacter : MonoBehaviour
{
    [SerializeField] private Animator animator_;
    [SerializeField] private Rigidbody2D body_;
    [SerializeField] private GameObject bulletPrefab_;
    [SerializeField] private Transform firePoint_;
    [SerializeField] private float bulletForce = 10f;
    [SerializeField] private int ammo_ = 5;
    [SerializeField] private float ammoRechargeTime_ = 1f;
    private Text text_;
    
    private Camera cam;
    private const float Speed = 4.0f;
    private Vector2 movement_;
    private Vector2 mousePos_;


    // Start is called before the first frame update
    void Start()
    {
        body_.GetComponent<Rigidbody2D>();
        cam = Camera.main;
        text_ = FindObjectOfType<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        text_.text = "Ammo: " + ammo_;
        movement_.x = Input.GetAxis("Horizontal");
        movement_.y = Input.GetAxis("Vertical"); 
        mousePos_ = cam.ScreenToWorldPoint(Input.mousePosition);
        
        Vector2 lookDir = mousePos_ - body_.position;
        
        animator_.SetFloat("Horizontal", movement_.x);
        animator_.SetFloat("Vertical", movement_.y);
        animator_.SetFloat("Speed", movement_.sqrMagnitude);
        animator_.SetFloat("MousePosX", lookDir.x);
        animator_.SetFloat("MousePosY", lookDir.y);

        if (ammoRechargeTime_ > 0)
        {
            ammoRechargeTime_ -= Time.deltaTime;
        }
        else
        {
            if (ammo_ < 5)
            {
                ammo_ += 1;
            }

            ammoRechargeTime_ = 1;
        }
        
        if (Input.GetButtonDown("Fire1"))
        {
            if (ammo_ != 0)
            {
                Shoot();
                ammo_ -= 1;
            }
        }

        
    }

    private void Shoot()
    {
        animator_.Play("PlayerShooting");
        Vector2 lookDir = mousePos_ - body_.position;
        GameObject bullet = Instantiate(bulletPrefab_, firePoint_.position, firePoint_.rotation);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        rb.AddForce(lookDir.normalized * bulletForce, ForceMode2D.Impulse);
    }

    private void FixedUpdate()
    {
        body_.MovePosition(body_.position + movement_ * Speed * Time.deltaTime);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Objectif"))
        {
            FindObjectOfType<GameManager>().Winnig();
        }

        if (other.gameObject.CompareTag("Slime") || other.gameObject.CompareTag("Skeleton"))
        {
            SceneManager.LoadScene("LudoScene");
        }
    }
}
