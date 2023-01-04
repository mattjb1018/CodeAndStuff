using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectible2 : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        if (Door2.instance != null)
        {
            Door2.instance.collectiblesCount++;
        }
    }


    void OnTriggerEnter2D(Collider2D target)
    {
        if (target.tag == "player")
        {
            Destroy(gameObject);
            if (Door2.instance != null)
            {
                Door2.instance.DecrementCollectibles();
            }
        }
    }
}