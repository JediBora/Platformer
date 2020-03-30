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
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {

            shotgunAnim.SetBool("Shot", shot = true);
            rifleAnim.SetBool("Shot", shot = true);
            palette1.SetActive(true);
            palette2.SetActive(true);
            isActivated = true;
        }
        else
        {
           
           
            shotgunAnim.SetBool("Shot", shot = false);
            rifleAnim.SetBool("Shot", shot = false);
            
        }
        if (isActivated) 
        {
            StartCoroutine(Wait());
            isActivated = false;
        }
    }
    IEnumerator Wait()
    {
        print("wait");
        yield return new WaitForSeconds(0.9f);
        palette1.SetActive(false);
        palette2.SetActive(false);
    }
}
