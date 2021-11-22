using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerAnimController : MonoBehaviour
{
    // This Function is for controlling player animation

    public Animator animator;

    private bool attacking;
    private int animState;

    public float timerAttackOff;
    private float timer;

    public playerController theController;
    public SceneController sceneController;

    // Start is called before the first frame update
    void Start()
    {       
        timer = timerAttackOff;
        attacking = false;
        animState = 0;
    }

    // Update is called once per frame
    void Update()
    {
        /*
        if(Input.GetKeyDown(KeyCode.RightArrow) && !theController.getCooldown())
        {
            resetTimer();
            attackOn();
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow) && !theController.getCooldown())
        {
            resetTimer();
            attackOn();
        }
        */

        timerOff();       
    }

    public void isAttacking()
    {
        resetTimer();
        attackOn();
    }

    public void attackOn()
    {
        attacking = true;
        animator.SetBool("isAttacking", attacking);

        if (animState == 0)
        {
            animState = 1;
            animator.SetInteger("animState", animState);           
        }
        else if(animState == 1)
        {
            animState = 2;
            animator.SetInteger("animState", animState);            
        }
        else if(animState == 2)
        {
            animState = 1;
            animator.SetInteger("animState", animState);
        }
    }

    public void dead()
    {
        animator.SetBool("isDead", true);       
    }

    void timerOff()
    {
        timer -= Time.deltaTime;
        if(timer < 0)
        {
            attacking = false;
            animState = 0;

            animator.SetBool("isAttacking", attacking);
            animator.SetInteger("animState", animState);
        }
    }

    void resetTimer()
    {
        timer = timerAttackOff;
    }  

    public void gameOver()
    {
        sceneController = FindObjectOfType<SceneController>();
        sceneController.GameOver();

        GameObject missText = GameObject.Find("missText");
        if(missText != null)
        {
            missText.SetActive(false);
        }       
    }
}
