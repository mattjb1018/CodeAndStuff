using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectible : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        if (Door.instance != null)
        {
            Door.instance.collectiblesCount++;
        }
    }

    
    void OnTriggerEnter2D(Collider2D target)
    {
        if (target.tag == "player")
        {
            Destroy(gameObject);
            if (Door.instance != null)
            {
                Door.instance.DecrementCollectibles();
            }
        }
    }
}
