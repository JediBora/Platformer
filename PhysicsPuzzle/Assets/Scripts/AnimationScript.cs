using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationScript : MonoBehaviour
{
    // Start is called before the first frame update
    public Animator shotgunAnim;
    public Animator rifleAnim;
    public bool shot = false;
    public GameObject palette1;
    public GameObject palette2;
    bool isActivated = false;
    public bool fired = false;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            //when I shoot change animation to shooting and set the collision to active to destroy the enemies
            shotgunAnim.SetBool("Shot", shot = true);
            rifleAnim.SetBool("Shot", shot = true);
            palette1.SetActive(true);
            palette2.SetActive(true);
            isActivated = true;
            fired = true;
        }
        else
        {
           //when I stop shooting change animation to idle
           
            shotgunAnim.SetBool("Shot", shot = false);
            rifleAnim.SetBool("Shot", shot = false);
            fired = false;
        }
        if (isActivated) 
        {
            StartCoroutine(Wait());
            isActivated = false;
        }
    }
    IEnumerator Wait()
    {
       //wait fow 0.9 seconds 
        yield return new WaitForSeconds(0.9f);
        palette1.SetActive(false);
        palette2.SetActive(false);
    }
}
