using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public GameObject enemy;
    public GameObject player;
    public int maxSpawn;
    public int spawnCount;
    public PlayerMovement playerMovement;
    void Update()
    {
        if (spawnCount < maxSpawn)
        {
            if (Input.GetMouseButtonDown(0))
            {
                Ray camRay = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit floorHit;
                if (Physics.Raycast(camRay, out floorHit))
                {
                    Quaternion facePlayer = Quaternion.LookRotation(player.transform.position - floorHit.point);
                    playerMovement.enemies.Add(Instantiate(enemy, new Vector3(floorHit.point.x, 0, floorHit.point.z), facePlayer));
                    playerMovement.NextEnemy();
                    spawnCount++;
                }
            }
        }
    }
}
