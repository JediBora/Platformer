﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class groundEnemyScript : MonoBehaviour
{
    public List<GameObject> waypoints;
    private int waypointIndex = 0;
    public float speed = 4;
    public PlayerMovement playerScript;

    public GameObject thHeart1;
    public GameObject thHeart2;
    public GameObject thHeart3;
    public GameObject thHeart4;
    public GameObject thHeart5;

    public GameObject bHeart1;
    public GameObject bHeart2;
    public GameObject bHeart3;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        MoveTowardWaypoint();

        if (playerScript.topHatHealth == 4)
        {
            thHeart5.SetActive(false);

        }
        if (playerScript.topHatHealth == 3)
        {
            thHeart4.SetActive(false);

        }
        if (playerScript.topHatHealth == 2)
        {
            thHeart3.SetActive(false);

        }
        if (playerScript.bandanaHealth == 2)
        {
            bHeart3.SetActive(false);
        }
        if (playerScript.topHatHealth == 1)
        {
            thHeart2.SetActive(false);

        }
        if (playerScript.bandanaHealth == 1)
        {
            bHeart2.SetActive(false);

        }
        if (playerScript.topHatHealth <= 0)
        {
            thHeart1.SetActive(false);
            SceneManager.LoadScene("GameOver");
        }
        if (playerScript.bandanaHealth <= 0)
        {
            bHeart1.SetActive(false);
            SceneManager.LoadScene("GameOver");
        }
    }
    void MoveTowardWaypoint()
    {
        transform.position = Vector3.MoveTowards(transform.position, waypoints[waypointIndex].transform.position, speed * Time.deltaTime);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject == waypoints[waypointIndex]) 
        {
            waypointIndex = (waypointIndex + 1) % waypoints.Count;
        }
        if (collision.gameObject.tag == "Player")
        {
            print("test");
            if (playerScript.bandanaActivated)
            {
                playerScript.bandanaHealth -= 1;
            }
            if (playerScript.topHatActivated)
            {
                playerScript.topHatHealth -= 1;
            }

        }
    }
}
