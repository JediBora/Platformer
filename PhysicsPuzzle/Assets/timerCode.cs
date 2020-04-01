using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class timerCode : MonoBehaviour
{ //The time limit (in seconds)
    public float maxTime;
    //The time (in seconds)
    public float timePassing;



    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        TimerFunction();
    }

    void TimerFunction()
    {
        timePassing += Time.deltaTime;

       
    }
   
}
