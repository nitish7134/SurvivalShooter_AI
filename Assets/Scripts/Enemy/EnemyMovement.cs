using UnityEngine;
using System.Collections;

public class EnemyMovement : MonoBehaviour
{
    Transform player;
    void Awake ()
    {
        player = GameObject.FindGameObjectWithTag ("Player").transform;
    }


    void Update ()
    {
        transform.LookAt(player);
    }
}
