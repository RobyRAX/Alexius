using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SceneController : MonoBehaviour
{
    // This Function for controlling The Scene
    public GameObject gameOverPanel;
    public playerController thePlayer;

    private int score;
    public Text scoreText;

    public static bool SoundIsOn = true;

    private float timer;
    private bool restart;
    private bool backMenu;

    // Start is called before the first frame update
    void Start()
    {
        FindObjectOfType<AudioManager>().Play("BGM");
        gameOverPanel.SetActive(false);
        score = 0;
        scoreText.text = "" + score;

        timer = 0.3f;
        restart = false;
        backMenu = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(restart || backMenu)
        {
            timerOff();
        }        
    }

    void timerOff()
    {
        timer -= Time.deltaTime;
        if (timer <= 0)
        {
            if (restart)
            {
                SceneManager.LoadScene("MainScene"); //Debug.Log("Restart");
            }
            else
            {
                SceneManager.LoadScene("MenuScene"); //Debug.Log("Menu Loaded");
            }
        }
    }

    public void GameOver()
    { 
        // Stop the time
        Time.timeScale = 0;
        gameOverPanel.SetActive(true);
    }

    public void setPlayerDead()
    {
        thePlayer.setDead();
    }
    
    public void RestartGame()
    {
        FindObjectOfType<AudioManager>().Play("ButtonClick");
        Time.timeScale = 1;
        restart = true;
    }

    public void BackToMenu()
    {
        FindObjectOfType<AudioManager>().Play("ButtonClick");
        Time.timeScale = 1;
        backMenu = true;
    }

    public void addScore()
    {
        score += 10;
        scoreText.text = "" + score;
    }

    public int getScore()
    {
        return score;
    }
}
