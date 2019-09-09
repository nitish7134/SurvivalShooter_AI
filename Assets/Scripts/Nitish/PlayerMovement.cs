using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerMovement : MonoBehaviour
{
    void Update()
    {
        if (!MasterClass.isKilling)
        {
            if (MasterClass.enemies.Count != 0)
            {
                if (MasterClass.ClosestEnemy != null)
                {
                    float interDist = Vector3.Distance(transform.position, MasterClass.ClosestEnemy.transform.position);
                    if (interDist <= MasterClass.distanceMaintain && interDist >= MasterClass.minDistanceMaintain)
                        KillEnemy();
                    else if (interDist < MasterClass.minDistanceMaintain)
                    {
                        MasterClass.navMeshAgent.destination = transform.position - ((MasterClass.ClosestEnemy.transform.position - transform.position).normalized * MasterClass.minDistanceMaintain);
                        MasterClass.navMeshAgent.isStopped = false;
                        MasterClass.anim.SetBool("IsWalking", true);
                    }
                }
                else
                    NextEnemy();
            }
            return;
        }
        else if (MasterClass.enemyHealth.currentHealth <= 0)
        {
            MasterClass.shouldShoot = false;
            MasterClass.enemies.Remove(MasterClass.ClosestEnemy);
            MasterClass.isKilling = false;
            NextEnemy();
        }
    }

    public void NextEnemy()
    {
        if (MasterClass.enemies.Count <= 0)
        {
            MasterClass.ClosestEnemy = null;
            return;
        }
        float minDist = 5000;
        float temp;
        int enIndex = 0;
        for (int i = 0; i < MasterClass.enemies.Count; i++)
        {
            if (MasterClass.enemies[i] == null && !ReferenceEquals(MasterClass.enemies[i], null))
            {
                MasterClass.enemies.RemoveAt(i--);
                continue;
            }
            temp = Vector3.Distance(transform.position, MasterClass.enemies[i].transform.position);
            if (temp < minDist)
            {
                minDist = temp;
                enIndex = i;
            }
        }
        MasterClass.ClosestEnemy = MasterClass.enemies[enIndex];
        MasterClass.enemyHealth = MasterClass.ClosestEnemy.GetComponent<EnemyHealth>();
        MasterClass.navMeshAgent.SetDestination(MasterClass.ClosestEnemy.transform.position);
        MasterClass.navMeshAgent.isStopped = false;
        MasterClass.anim.SetBool("IsWalking", true);
    }


    public void KillEnemy()
    {
        transform.LookAt(MasterClass.ClosestEnemy.transform);
        MasterClass.isKilling = true;
        MasterClass.navMeshAgent.destination = transform.position; //So After Killing player stays where it is
        MasterClass.navMeshAgent.isStopped = true;
        MasterClass.anim.SetBool("IsWalking", false);
        MasterClass.shouldShoot = true;
    }


}