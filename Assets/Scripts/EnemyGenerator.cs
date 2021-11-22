using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGenerator : MonoBehaviour
{
    // Generate Random Enemy here

    // For cooldown
    private float coolDownTime;
    private float theTime;
    private float levelUpTime;

    // For Spawning Enemy 
    public GameObject theEnemy;
    public Transform spawnPoint1; // From left
    public Transform spawnPoint2; // From Right
    private int randomSpawnLimit;

    public Transform player;
    private bool canSpawnLeft;
    private bool canSpawnRight;
    private bool skipSpawn;
    private float TempTime = 3f;

    private int playerScore, savePS, sendSpeed;
    SceneController theController;
    
    // Start is called before the first frame update
    void Start()
    {
        // Set spawn cooldown in 1.5 second
        coolDownTime = 1.5f;
        theTime = coolDownTime;

        // Set limit to 5
        randomSpawnLimit = 5;

        canSpawnLeft = true;
        canSpawnRight = true;
        skipSpawn = false;

        theController = FindObjectOfType<SceneController>();
        playerScore = theController.getScore();
        savePS = playerScore;
        sendSpeed = 10;
    }

    // Update is called once per frame
    void Update()
    {
        playerScore = theController.getScore();
        lvlProgression();
        spawnEnemy();
        canSpawnCheck();
        skipSpawning();
    }

    private void canSpawnCheck()
    {
        if(player.position.x >= 10)
        {
            canSpawnRight = false;
        }
        else
        {
            canSpawnRight = true;
        }

        if (player.position.x <= -10)
        {
            canSpawnLeft = false;
        }
        else
        {
            canSpawnLeft = true;
        }
    }

    private void skipSpawning()
    {
        if (skipSpawn)
        {
            TempTime -= Time.deltaTime;
            if (TempTime <= 0)
            {
                skipSpawn = false;
                TempTime = 3;
            }
        }        
    }

    private void spawnEnemy()
    {
        theTime -= Time.deltaTime;

        if (theTime <= 0)
        {
            GameObject spawnedEnemy;
            EnemyController spawnedEnemyController;
            int randomSpawn = Random.Range(0, 10);
           
            if (randomSpawn < randomSpawnLimit && canSpawnLeft && !skipSpawn)
            {
                // Spawn Left
                spawnedEnemy = Instantiate(theEnemy, spawnPoint1.position, spawnPoint1.rotation);
                spawnedEnemyController = spawnedEnemy.GetComponent<EnemyController>(); 

                // Do something with spawned enemy
                spawnedEnemy.gameObject.tag = "EnemyLeft";
                spawnedEnemyController.setSpeed(sendSpeed);
            }

            randomSpawn = Random.Range(0, 10);

            if (randomSpawn < randomSpawnLimit && canSpawnRight && !skipSpawn)
            {
                // Spawn Right
                spawnedEnemy = Instantiate(theEnemy, spawnPoint2.position, spawnPoint2.rotation);
                spawnedEnemyController = spawnedEnemy.GetComponent<EnemyController>();
  
                // Do something with spawned enemy
                spawnedEnemy.gameObject.tag = "EnemyRight";
                spawnedEnemyController.setSpeed(sendSpeed);
            }

            theTime = coolDownTime;
        }
    }

    void LateUpdate()
    {
        
    }

    void lvlProgression()
    {       
        if(playerScore > savePS + 100)
        {
            sendSpeed += 2;
            savePS = playerScore;

            if (coolDownTime > 0.8f)
            {
                coolDownTime -= 0.2f;
            }
            if (randomSpawnLimit < 7)
            {
                randomSpawnLimit += 1;
            }

            skipSpawn = true;
        }    
        
        // Maximum Level !!!!
        if(playerScore > 500)
        {
            coolDownTime = 0.5f;
            randomSpawnLimit = 11;
        }
    }
}
