using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private int speed;
    private bool isAttacked;
    private bool isLife;

    public GameObject enemyModel;

    public SceneController theController;

    static bool Attacking;

    // Start is called before the first frame update
    void Start()
    {
        speed = 10;
        isAttacked = false;
        isLife = true;
        Attacking = false;

        theController = FindObjectOfType<SceneController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isLife)
        {
            // Set enemy speed
            transform.Translate(speed * Time.deltaTime, 0, 0);
        }
       


        // Destroy enemy if out of range (Beta)
        if(transform.position.x < -100 || transform.position.x > 100 || transform.position.y < -30)
        {
            Destroy(gameObject);
        }
    }

    
    public void enemyAttacked()
    {
        isAttacked = true;
    }

    void OnCollisionEnter(Collision colliderInfo)
    {      
        if(colliderInfo.collider.name == "PlayerController")
        {           
            enemyAnimController enemyAC = enemyModel.GetComponent<enemyAnimController>();
            setOff();
            
            if (!isAttacked)
            {
                enemyAC.enemyAttack();

                // Set player dead so player can't move anymore
                theController.setPlayerDead();
                if (!Attacking)
                {
                    FindObjectOfType<AudioManager>().Play("MonsterAttack");
                    Attacking = true;
                }
                                
                //Debug.Log("Player Dead");
            }
            else
            {
                enemyAC.enemyDead();
                theController.addScore();

                int temp = Random.Range(0, 2);
                if (temp == 0)
                {
                    FindObjectOfType<AudioManager>().Play("MonsterDead1");
                }
                else
                {
                    FindObjectOfType<AudioManager>().Play("MonsterDead2");
                }
                //Debug.Log("Enemy Dead");
            }            
        }
    }

    public void destroy()
    {
        Destroy(gameObject);
    }

    public void setOff()
    {
        isLife = false;
        GetComponent<BoxCollider>().enabled = false;
        GetComponent<Rigidbody>().useGravity = false;
        gameObject.tag = "DeadEnemy";
    }

    public void setGameOver()
    {
        theController.GameOver();
    }

    public void setSpeed(int inputSpeed)
    {
        speed = inputSpeed;
        //Debug.Log("CURRENT SPEED : " + speed);
    }
}
