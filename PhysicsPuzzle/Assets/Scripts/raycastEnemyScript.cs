using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class raycastEnemyScript : MonoBehaviour
{
    public float speed = 2;
    public LayerMask mask;
    public GameObject endPoint;
    RaycastHit2D hit;
    bool isActivated;
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
    void FixedUpdate()
    {

        //shoots a raycast downwards
        hit = Physics2D.Raycast(transform.position, -Vector2.up * 20, mask);
        Debug.DrawRay(transform.position, -Vector2.up * 20);

        //if it hits player do something
        if (hit.collider != null)
        {
            if (hit.collider.gameObject.tag == "Player")
            {
                isActivated = true;
                print("hit");
                
            }
        }
    }
    private void Update()
    {
        MoveToward();

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
            //load GameOver
        }
        if (playerScript.bandanaHealth <= 0)
        {
            bHeart1.SetActive(false);
        }
    }
    void MoveToward() {
        if (isActivated)
        {
            transform.position = Vector2.MoveTowards(transform.position, endPoint.transform.position, speed * Time.deltaTime);
        }
       
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
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
