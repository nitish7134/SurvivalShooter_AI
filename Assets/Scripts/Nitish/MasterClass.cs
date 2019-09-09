using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MasterClass : MonoBehaviour
{
    public static Transform playerTransform;
    public static int maxSpawn = 5;
    public static int spawnCount = 0;
    public static float distanceMaintain = 5f;
    public static float minDistanceMaintain = 3f;
    public static GameObject enemy;
    public static GameObject player;
    public static PlayerMovement playerMovement;
    public static NavMeshAgent navMeshAgent;
    public static Animator anim;
    public static PlayerShooting playerShooting;
    public static List<GameObject> enemies = new List<GameObject>();
    public static GameObject ClosestEnemy;
    public static bool isKilling = false;
    public static EnemyHealth enemyHealth;
    public static bool shouldShoot;
    private void Awake()
    {
        Transform playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        enemy = (GameObject)Resources.Load("prefabs/ZomBear", typeof(GameObject));
        player = GameObject.FindGameObjectWithTag("Player");
        playerMovement = player.GetComponent<PlayerMovement>();
        navMeshAgent = player.GetComponent<NavMeshAgent>();
        anim = player.GetComponent<Animator>();
        playerShooting = player.GetComponentInChildren<PlayerShooting>();
        enemyHealth = enemy.GetComponent<EnemyHealth>();
    }
    

}
