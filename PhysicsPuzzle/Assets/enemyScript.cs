using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyScript : MonoBehaviour
{
    public Vision vision;
    public GameObject player;
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
    }
}
