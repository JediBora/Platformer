using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class enemyScript : MonoBehaviour
{
    public Vision vision;
    public GameObject player;
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
        if (vision.objectsInVolume.Count > 0) 
        {
          transform.position = Vector2.MoveTowards(transform.position, player.transform.position, 5 * Time.deltaTime);
         
        }
  
        if (playerScript.topHatHealth == 4)
        {
            thHeart5.SetActive(false);

        }
        if ( playerScript.topHatHealth == 3)
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
        if( playerScript.topHatHealth ==1)
        {
            thHeart2.SetActive(false);

        }
        if (playerScript.bandanaHealth == 1) 
        {
            bHeart2.SetActive(false);
        
        }
       if( playerScript.topHatHealth <=0) 
        {
            thHeart1.SetActive(false);
            SceneManager.LoadScene("GameOver");
        }
        if (playerScript.bandanaHealth <= 0) 
        {
            SceneManager.LoadScene("GameOver");
            bHeart1.SetActive(false);
        }
    }
    private void OnTriggerEnter2D (Collider2D collision)
    {
        if (collision.gameObject.tag == "Player") 
        {
            //print("test");
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
