using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class PlayerMovement : MonoBehaviour
{
    private NavMeshAgent navMeshAgent;
	private Animator anim;
    private GameObject enemy;
    private EnemyHealth enemyHealth;
    
    private PlayerShooting playerShooting;
    private GameObject[] enemies;
    public float distanceMaintain = 3f;
    public float minDistanceMaintain = 1.5f;
    public bool isKilling = false;
    void Awake()
	{
        navMeshAgent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
        playerShooting = GetComponentInChildren<PlayerShooting>();
        enemies = GameObject.FindGameObjectsWithTag("Enemy");
    }
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
            NextEnemy();
        
        if (!isKilling)
        {
            if (enemy != null)
            {
                
                float interDist = Vector3.Distance(transform.position, enemy.transform.position);
                if (interDist <= distanceMaintain && interDist >= minDistanceMaintain)
                {
  
                    navMeshAgent.destination = transform.position;
                    navMeshAgent.isStopped = true;

                    isKilling = true;
                    KillEnemy();

                }
                else if (interDist < minDistanceMaintain)
                {
                    navMeshAgent.destination = Vector3.MoveTowards(transform.position, enemy.transform.position,interDist-distanceMaintain);
                    navMeshAgent.isStopped = false;

                    anim.SetBool("IsWalking", true);
                }

            }
            else
                NextEnemy();
        }
        else if (enemyHealth.currentHealth<=0)
        {
            playerShooting.setShoot(false);
            isKilling = false;
            enemy = null;
            NextEnemy();
        }

    }
 
    void NextEnemy()
    {
        enemies = GameObject.FindGameObjectsWithTag("Enemy");

        if (enemies.Length <= 0)
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
        for(int i = 0; i < enemies.Length; i++)
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
        anim.SetBool("IsWalking", false);
        transform.LookAt(enemy.transform);

        playerShooting.setShoot(true);
    }
    
}
