using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerController : MonoBehaviour
{

    private bool isFacingRight;
    private bool isEnemyInRange_LEFT;
    private bool isEnemyInRange_RIGHT;

    public float rotateSpeed;
    public float dashSpeed;
    public float attackRange;

    Quaternion rotTarget;

    FindClosest fc;
    Vector3 locTarget;

    public float missTimer;
    private float zeroTimeDash;
    private float dashCooldownTime = 2;
    private bool dashIsCoolDown = false;
    public playerAnimController theAnimController;
    public GameObject missText; 

    private bool isDead;
    private bool deadSoundPlayed;

    // Start is called before the first frame update
    void Start()
    {
        zeroTimeDash = missTimer;
        locTarget = transform.position;
        isFacingRight = true;
        isEnemyInRange_LEFT = false;
        isEnemyInRange_RIGHT = false;
        fc = GetComponent<FindClosest>();

        isDead = false;
        deadSoundPlayed = false;
        missText.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (!isDead)
        {
            if (!dashIsCoolDown)
            {
                
                if (Input.GetKeyDown(KeyCode.RightArrow))
                {
                    dashRight();
                    
                }

                if (Input.GetKeyDown(KeyCode.LeftArrow))
                {
                    dashLeft();
                    
                }

            }

            else if (dashIsCoolDown)
            {
                zeroTimeDash -= 1 / dashCooldownTime * Time.deltaTime;
                if (zeroTimeDash <= 0)
                {
                    dashIsCoolDown = false;
                    zeroTimeDash = missTimer;
                    missText.SetActive(false);
                }
            }

            setRotation();
            setLocation();

            if (fc.getClosestEnemy_LEFT() != Vector3.zero)
            {
                checkEnemyDistance_LEFT();
            }
            else
            {
                isEnemyInRange_LEFT = false;
            }

            if (fc.getClosestEnemy_RIGHT() != Vector3.zero)
            {
                checkEnemyDistance_RIGHT();
            }
            else
            {
                isEnemyInRange_RIGHT = false;
            }
        }
        else if(isDead && !deadSoundPlayed)
        {
            deadSoundPlayed = true;
            StartCoroutine(deadSound());
        }
    }

    IEnumerator deadSound()
    {
        yield return new WaitForSeconds(0.6f);
        FindObjectOfType<AudioManager>().Play("PlayerDead");
    }

    public void buttonDashLeft()
    {
        if (!dashIsCoolDown && !isDead)
        {
            //if (Input.GetKeyDown(KeyCode.RightArrow))
            //{
                dashLeft(); //Debug.Log("Left");
                //theAnimController.isAttacking();
            //}
        }
    }

    public void buttonDashRight()
    {
        if (!dashIsCoolDown && !isDead)
        {
            //if (Input.GetKeyDown(KeyCode.LeftArrow))
            //{
                dashRight(); //Debug.Log("Right");
                //theAnimController.isAttacking();
            //}
        }
    }

    void setRotation()
    {
        if (isFacingRight)
        {
            rotTarget = Quaternion.Euler(new Vector3(0, 110, 0));
        }
        else
        {
            rotTarget = Quaternion.Euler(new Vector3(0, -110, 0));
        }
            
        transform.rotation = Quaternion.Slerp(transform.rotation, rotTarget, rotateSpeed * Time.deltaTime);
    }

    void setLocation()
    {       
        transform.position = Vector3.Lerp(transform.position, new Vector3(locTarget.x, transform.position.y, 0), dashSpeed * Time.deltaTime);
    }

    void dashLeft()
    {
        theAnimController.isAttacking();
        isFacingRight = false;
        fc.setFace(isFacingRight);
        EnemyController temp;
        if (isEnemyInRange_LEFT)
        {
            locTarget = fc.getClosestEnemy_LEFT(); //Debug.Log("Dash Left");
            temp = fc.getClosestEnemyGenerator_LEFT();
            temp.enemyAttacked();

            // Sword SFX
            randomAttackSFX();
        }
        else
        {
            locTarget = transform.position + new Vector3(-attackRange, 0, 0);
            // Dash Cooldown
            dashIsCoolDown = true;
            missText.SetActive(true);

            GameObject camera = GameObject.Find("Main Camera");
            cameraShaker camShaker = camera.GetComponent<cameraShaker>();
            StartCoroutine(camShaker.Shake(.75f, .1f));
        }        
    }

    void dashRight()
    {
        theAnimController.isAttacking();
        isFacingRight = true;
        fc.setFace(isFacingRight);
        EnemyController temp;
        if (isEnemyInRange_RIGHT)
        {
            locTarget = fc.getClosestEnemy_RIGHT(); //Debug.Log("Dash Right");
            temp = fc.getClosestEnemyGenerator_RIGHT();
            temp.enemyAttacked();

            // Sword SFX
            randomAttackSFX();
        }
        else
        {
            locTarget = transform.position + new Vector3(attackRange, 0, 0);
            // Dash Cooldown
            dashIsCoolDown = true;
            missText.SetActive(true);

            GameObject camera = GameObject.Find("Main Camera");
            cameraShaker camShaker = camera.GetComponent<cameraShaker>();
            StartCoroutine(camShaker.Shake(.75f, .1f));
        }          
    }

    void randomAttackSFX()
    {
        int rand = Random.Range(0, 2);
        if(rand == 0)
        {
            FindObjectOfType<AudioManager>().Play("PlayerAttack1");
        }
        else
        {
            FindObjectOfType<AudioManager>().Play("PlayerAttack2");
        }
    }

    void checkEnemyDistance_LEFT()
    {
        Vector3 enemyLoc = fc.getClosestEnemy_LEFT();
        float distance = enemyLoc.x - transform.position.x;

        if(distance < 0)
        {
            distance *= -1;
        }

        if (distance <= attackRange)
        {
            isEnemyInRange_LEFT = true;
            Debug.DrawLine(transform.position, enemyLoc, new Color(0, 255, 0));
        }
        else
            isEnemyInRange_LEFT = false;

        //Debug.Log("Attack Available LEFT : " + isEnemyInRange_LEFT);
    }

    void checkEnemyDistance_RIGHT()
    {
        Vector3 enemyLoc = fc.getClosestEnemy_RIGHT();
        float distance = enemyLoc.x - transform.position.x;

        if (distance < 0)
        {
            distance *= -1;
        }

        if (distance <= attackRange)
        {
            isEnemyInRange_RIGHT = true;
            Debug.DrawLine(transform.position, enemyLoc, new Color(0, 255, 0));
        }
        else
            isEnemyInRange_RIGHT = false;

        //Debug.Log("Attack Available RIGHT : " + isEnemyInRange_RIGHT);
    }

    public void setDead()
    {
        isDead = true;
    }
    
}
