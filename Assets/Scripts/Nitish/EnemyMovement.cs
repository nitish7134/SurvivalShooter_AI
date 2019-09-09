using UnityEngine;
using System.Collections;

public class EnemyMovement : MonoBehaviour
{
    void Update ()
    {
        transform.LookAt(MasterClass.playerTransform);
    }

}
