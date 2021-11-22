using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGeneratorWave : MonoBehaviour
{
    // Generate Random Enemy here

    // For Spawning Enemy 
    public GameObject theEnemy;

    public Transform spawnPointL_1;
    //public Transform spawnPointL_2;
    //public Transform spawnPointL_3;// From left


    public Transform spawnPointR_1;
    //public Transform spawnPointR_2;
    //public Transform spawnPointR_3;// From Right

    public Transform player;

    private float spawnTimer;
    public float inputSpawnTimer;

    int randomSpawn;
    //public int spawnLimit;
    public int inputSpawnLimit_L;
    public int inputSpawnLimit_R;

    private int spawnLimit_L;
    private int spawnLimit_R;

    private bool leftAvailable = true;
    private bool rightAvailable = true;

    SceneController sceneController;
    private float saveScore;

    // Start is called before the first frame update
    void Start()
    {
        sceneController = FindObjectOfType<SceneController>();
        saveScore = sceneController.getScore();        

        spawnTimer = inputSpawnTimer;
        spawnLimit_L = inputSpawnLimit_L;
        spawnLimit_R = inputSpawnLimit_R;
    }

    // Update is called once per frame
    void Update()
    {      
        spawnEnemy();
        /*Debug.Log(randomSpawn);
        Debug.Log(spawnLimit_L + " " + spawnLimit_R);
        Debug.Log(rightAvailable + " " + leftAvailable);*/
        lvlProgression();
    }

    void LateUpdate()
    {
        //setPos();
    }

    void randomSystem()
    {
        if(leftAvailable && rightAvailable)
            randomSpawn = Random.Range(1, 7);

        if (leftAvailable && !rightAvailable)
            randomSpawn = Random.Range(1, 4);

        if (!leftAvailable && rightAvailable)
            randomSpawn = Random.Range(4, 7);
    }

    private void spawnEnemy()
    {
        spawnTimer -= Time.deltaTime;
        randomSystem();

        GameObject spawnedEnemy;
        EnemyController spawnedEnemyController;

        if (spawnTimer < 0)
        {
            if (randomSpawn > 0 && randomSpawn <= 3 && leftAvailable)//1 2 3
            {
                // Spawn Left 1
                spawnedEnemy = Instantiate(theEnemy, spawnPointL_1.position, spawnPointL_1.rotation);
                spawnedEnemyController = spawnedEnemy.GetComponent<EnemyController>();

                // Do something with spawned enemy
                spawnedEnemy.gameObject.tag = "EnemyLeft";
                spawnLimit_L--;

                if (!rightAvailable)
                    spawnLimit_R = inputSpawnLimit_R;
            }

            if (randomSpawn > 3 && rightAvailable)//4 5 6
            {
                // Spawn Right 3
                spawnedEnemy = Instantiate(theEnemy, spawnPointR_1.position, spawnPointR_1.rotation);
                spawnedEnemyController = spawnedEnemy.GetComponent<EnemyController>();

                // Do something with spawned enemy
                spawnedEnemy.gameObject.tag = "EnemyRight";
                spawnLimit_R--;

                if (!leftAvailable)
                    spawnLimit_L = inputSpawnLimit_L;
            }
            spawnTimer = inputSpawnTimer;
        }
        waveSystem();
    }

    void waveSystem()
    {
        if (spawnLimit_L == 0)
        {
            leftAvailable = false;
        }
        else if (spawnLimit_L > 0)
            leftAvailable = true;

        if (spawnLimit_R == 0)
        {
            rightAvailable = false;
        }
        else if (spawnLimit_R > 0)
            rightAvailable = true;
    }

    void setPos()
    {
        transform.position = player.position;
    }

    public float sendSpeed;

    void lvlProgression()
    {
        if(sceneController.getScore() >  saveScore + 10)
        {
            sendSpeed += 0.5f;
            inputSpawnTimer -= 0.025f;
            saveScore = sceneController.getScore();

            Debug.Log(spawnTimer + " " + sendSpeed);
        }
    }
}



