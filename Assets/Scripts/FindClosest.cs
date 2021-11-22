using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FindClosest : MonoBehaviour
{
    private bool isFacingRight = true;
    GameObject closestEnemy_LEFT;
    GameObject[] allEnemies_LEFT;

    GameObject closestEnemy_RIGHT;
    GameObject[] allEnemies_RIGHT;

    Transform thePlayer;

    // Update is called once per frame
    void Update ()
    {
        FindClosestEnemy_LEFT();
        FindClosestEnemy_RIGHT();
    }

	public void FindClosestEnemy_LEFT()
	{
		float distanceToClosestEnemy = Mathf.Infinity;
        closestEnemy_LEFT = null;
        allEnemies_LEFT = GameObject.FindGameObjectsWithTag("EnemyLeft");

		foreach (GameObject currentEnemy in allEnemies_LEFT)
        {
			float distanceToEnemy = (currentEnemy.transform.position - this.transform.position).sqrMagnitude;
			if (distanceToEnemy < distanceToClosestEnemy)
            {
				distanceToClosestEnemy = distanceToEnemy;
				closestEnemy_LEFT = currentEnemy;
			}
		}

        if(closestEnemy_LEFT != null)
        {
            Debug.DrawLine(this.transform.position, closestEnemy_LEFT.transform.position);
        }	
	}

    public void FindClosestEnemy_RIGHT()
    {
        float distanceToClosestEnemy = Mathf.Infinity;
        closestEnemy_RIGHT = null;
        allEnemies_RIGHT = GameObject.FindGameObjectsWithTag("EnemyRight");

        foreach (GameObject currentEnemy in allEnemies_RIGHT)
        {
            float distanceToEnemy = (currentEnemy.transform.position - this.transform.position).sqrMagnitude;
            if (distanceToEnemy < distanceToClosestEnemy)
            {
                distanceToClosestEnemy = distanceToEnemy;
                closestEnemy_RIGHT = currentEnemy;
            }
        }

        if(closestEnemy_RIGHT != null)
        {
            Debug.DrawLine(this.transform.position, closestEnemy_RIGHT.transform.position);
        } 
    }

    public void setFace(bool dir)
    {
        isFacingRight = dir;
    }

    public Vector3 getClosestEnemy_LEFT()
    {
        if(closestEnemy_LEFT != null)
        {
            return closestEnemy_LEFT.transform.position;
        }
        else
        {
            return Vector3.zero;
        }
    }

    public EnemyController getClosestEnemyGenerator_LEFT()
    {
        if (closestEnemy_LEFT != null)
        {
            return closestEnemy_LEFT.GetComponent<EnemyController>();
        }
        else
        {
            return null;
        }
    }

    public Vector3 getClosestEnemy_RIGHT()
    {
        if(closestEnemy_RIGHT != null)
        {
            return closestEnemy_RIGHT.transform.position;
        }
        else
        {
            return Vector3.zero;
        }
    }

    public EnemyController getClosestEnemyGenerator_RIGHT()
    {
        if (closestEnemy_RIGHT != null)
        {
            return closestEnemy_RIGHT.GetComponent<EnemyController>();
        }
        else
        {
            return null;
        }
    }


}
