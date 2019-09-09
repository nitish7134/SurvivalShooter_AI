using UnityEngine;

public class GameOverManager : MonoBehaviour
{
   
    Animator anim;
	float restartTimer;


    void Awake()
    {
        anim = GetComponent<Animator>();
    }

    [System.Obsolete]
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            anim.SetTrigger("GameOver");
            restartTimer += Time.deltaTime;
            Application.LoadLevel(Application.loadedLevel);

        }
    }
}
