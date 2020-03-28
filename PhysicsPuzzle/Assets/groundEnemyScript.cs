using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class groundEnemyScript : MonoBehaviour
{
    public List<GameObject> waypoints;
    private int waypointIndex = 0;
    public float speed = 4;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        MoveTowardWaypoint();
        
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
      
    }
}
