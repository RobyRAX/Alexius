using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyAnimController : MonoBehaviour
{
    public Animator animator;
    public GameObject parentObject;
    

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void enemyAttack()
    {
        animator.SetBool("isEnemyAttack", true);
    }

    public void enemyDead()
    {
        animator.SetBool("isEnemyDead", true);        
    }

    public void destroyParent()
    {
        EnemyController ec = parentObject.GetComponent<EnemyController>();
        ec.destroy();
    }

    public void setPlayerDead()
    {
        GameObject player = GameObject.Find("Char");
        playerAnimController pac = player.GetComponent<playerAnimController>();
        pac.dead();

        GameObject camera = GameObject.Find("Main Camera");
        cameraShaker camShaker = camera.GetComponent<cameraShaker>();
        StartCoroutine(camShaker.Shake(2f, .2f));
    }
}
