using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerMovement : MonoBehaviour
{
    private NavMeshAgent navMeshAgent;
	private Animator anim;
    private GameObject enemy;
    private EnemyHealth enemyHealth;
    private PlayerShooting playerShooting;

    public List <GameObject> enemies = new List<GameObject>();
    public float distanceMaintain = 5f;
    public float minDistanceMaintain = 3f;
    public bool isKilling = false;

    void Awake()
	{
        navMeshAgent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
        playerShooting = GetComponentInChildren<PlayerShooting>();
    }
    void Update()
    {
        if (!isKilling)
        {
            if (enemies.Count != 0)
            {
                if (enemy != null)
                {
                    float interDist = Vector3.Distance(transform.position, enemy.transform.position);
                    if (interDist <= distanceMaintain && interDist >= minDistanceMaintain)
                        KillEnemy();
                    else if (interDist < minDistanceMaintain)
                    {
                        
                        navMeshAgent.destination = Vector3.MoveTowards(transform.position, enemy.transform.position, interDist - distanceMaintain);
                        Debug.Log("To Close Moving Back; from " + transform.position + " to " + navMeshAgent.destination + " away from " + enemy.transform.position);
                        navMeshAgent.isStopped = false;
                        anim.SetBool("IsWalking", true);
                    }
                }
                else
                    NextEnemy();
            }
            return;
        }
        else if (enemyHealth.currentHealth <= 0)
        {
            playerShooting.setShoot(false);
            enemies.Remove(enemy);
            isKilling = false;
            NextEnemy();
        }
    }
 
    public void NextEnemy()
    {
        if (enemies.Count <= 0)
        {
            enemy = null;
            return;
        }
        findClosestEnemy();

        navMeshAgent.SetDestination(enemy.transform.position);
        navMeshAgent.isStopped = false;

        anim.SetBool("IsWalking", true);
    }

    public void findClosestEnemy()
    {
        float minDist = 5000;
        float temp;
        int enIndex=0;
        for(int i = 0; i < enemies.Count; i++)
        {
            temp = Vector3.Distance(transform.position, enemies[i].transform.position);
            if (temp < minDist) {
                minDist = temp;
                enIndex = i;
            }
        }
        enemy = enemies[enIndex];
        enemyHealth = enemy.GetComponent<EnemyHealth>();
    }
    public void KillEnemy()
    {
        isKilling = true;
        navMeshAgent.destination = transform.position;
        navMeshAgent.isStopped = true;
        anim.SetBool("IsWalking", false);
        transform.LookAt(enemy.transform);
        playerShooting.setShoot(true);

    }
    
}
