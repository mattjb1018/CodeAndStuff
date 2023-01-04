using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShooter : MonoBehaviour {

    [SerializeField]
    private GameObject bullet; //this will be the fireball or enemy projectile

    void Start()
    {
        StartCoroutine (Attack()); //this calls our attack function to start shooting bullets after start is played. 
    }

IEnumerator Attack()
    {
        yield return new WaitForSeconds (Random.Range(1, 3));
        Instantiate (bullet, transform.position, Quaternion.identity);
        StartCoroutine (Attack());
    }




}
