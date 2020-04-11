using System.Collections;
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
            //decrease the heart limit
            thHeart5.SetActive(false);

        }
        if (playerScript.topHatHealth == 3)
        {
            //decrease the heart limit
            thHeart4.SetActive(false);

        }
        if (playerScript.topHatHealth == 2)
        {
            //decrease the heart limit
            thHeart3.SetActive(false);

        }
        if (playerScript.bandanaHealth == 2)
        {
            //decrease the heart limit
            bHeart3.SetActive(false);
        }
        if (playerScript.topHatHealth == 1)
        {
            //decrease the heart limit
            thHeart2.SetActive(false);

        }
        if (playerScript.bandanaHealth == 1)
        {
            //decrease the heart limit
            bHeart2.SetActive(false);

        }
        if (playerScript.topHatHealth <= 0)
        {
            //decrease the heart limit and switch to GameOver
            thHeart1.SetActive(false);
            SceneManager.LoadScene("MainMenu");
        }
        if (playerScript.bandanaHealth <= 0)
        {
            //decrease the heart limit and switch to GameOver
            bHeart1.SetActive(false);
            SceneManager.LoadScene("MainMenu");
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
