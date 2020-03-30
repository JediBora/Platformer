using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomGenerator : MonoBehaviour
{

    public GameObject[] hatObjects;
    public GameObject[] weapons;

    public GameObject spawnPos;
    public GameObject spawnPos1;
    // public GameObject spawnPos2;
    // Start is called before the first frame update
    void Start()
    {
        randomObject();
    }

    // Update is called once per frame
    void Update()
    {



    }
    public void randomObject()
    {

        Instantiate(hatObjects[Random.Range(0, 2)], new Vector2(spawnPos.transform.position.x, spawnPos.transform.position.y), Quaternion.identity);
        Instantiate(weapons[Random.Range(0, 2)], new Vector2(spawnPos1.transform.position.x, spawnPos1.transform.position.y), Quaternion.identity);
        // Instantiate(hatObjects[Random.Range(0, 2)], new Vector2(spawnPos2.transform.position.x, spawnPos2.transform.position.y), Quaternion.identity);

    }

}
