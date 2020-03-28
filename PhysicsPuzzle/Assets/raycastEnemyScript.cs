using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class raycastEnemyScript : MonoBehaviour
{
    public float speed = 2;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //shoots a raycast downwards
        RaycastHit2D hit = Physics2D.Raycast(transform.position, -Vector2.up, 100);
        Debug.DrawRay(transform.position, -Vector2.up);
        LayerMask mask = LayerMask.GetMask("Player");
        //if it hits player do something
        if (hit.collider != null) 
        {
            Vector2.MoveTowards(transform.position, Vector2.down,mask);
        }
    }
}
