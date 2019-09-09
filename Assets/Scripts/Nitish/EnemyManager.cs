using UnityEngine;

public class EnemyManager : MonoBehaviour
{
   
    void Update()
    {
        if (MasterClass.spawnCount < MasterClass.maxSpawn)
        {
            if (Input.GetMouseButtonDown(0))
            {
                Ray camRay = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit floorHit;
                if (Physics.Raycast(camRay, out floorHit))
                {
                    Quaternion facePlayer = Quaternion.LookRotation(MasterClass.player.transform.position - floorHit.point);
                    MasterClass.enemies.Add(Instantiate(MasterClass.enemy, new Vector3(floorHit.point.x, 0, floorHit.point.z), facePlayer));
                    if(!MasterClass.isKilling)
                        MasterClass.playerMovement.NextEnemy();
                    MasterClass.spawnCount++;
                }
            }
        }
    }
}
