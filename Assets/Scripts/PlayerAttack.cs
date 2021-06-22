using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] private float attackDamage = 2.0f;
    private bool asattack = false;
    public float AttackDamage
    {
        get => attackDamage;
        set => attackDamage = value;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        asattack = false;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Slime") && asattack == false)
        {
            other.GetComponent<SlimeBehaviour>().CalculateDamage(AttackDamage);
            asattack = true;
        }
        else if(other.CompareTag("Skeleton") && asattack == false)
        {
            other.GetComponent<SkeletonBehaviour>().CalculateDamage(AttackDamage);
            asattack = true;
        }
    }
}