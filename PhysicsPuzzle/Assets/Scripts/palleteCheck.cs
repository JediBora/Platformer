﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class palleteCheck : MonoBehaviour
{
    public GameObject flyingEnemy;
    public GameObject flyingEnemy1;
    public GameObject flyingEnemy2;
    
    public GameObject groundEnemy;
    public GameObject groundEnemy1;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            //if the bullet collides with the enemy destroy the enemy
            Destroy(flyingEnemy);
        }
        if (collision.gameObject.tag == "GroundEnemy")
        {
            //if the bullet collides with the enemy destroy the enemy
            Destroy(groundEnemy);
        }
        if (collision.gameObject.tag == "GroundEnemy1")
        {
            //if the bullet collides with the enemy destroy the enemy
            Destroy(groundEnemy1);
        } 
        if (collision.gameObject.tag == "Enemy1")
        {
            //if the bullet collides with the enemy destroy the enemy
            Destroy(flyingEnemy1);
        }
        if (collision.gameObject.tag == "Enemy2")
        {
            //if the bullet collides with the enemy destroy the enemy
            Destroy(flyingEnemy2);
        }

    }
}
