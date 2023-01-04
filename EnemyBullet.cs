using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{

    void OnCollisionEnter2D(Collision2D target)
    {
        if (target.gameObject.tag == "ground")
        {
            Destroy (gameObject);
        }
        if (target.gameObject.tag == "player")
        {
            Destroy(gameObject);
        }
    }

}
