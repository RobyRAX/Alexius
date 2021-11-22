using UnityEngine.SceneManagement;
using UnityEngine;
using System.Collections;

public class MainMenuController : MonoBehaviour
{
    public GameObject creditPanel;
    public GameObject creditPanel2;
    public GameObject buttonHandler;

    private void Start()
    {
        creditPanel.SetActive(false);
    }

    public void Play()
    {
        StartCoroutine(PlaySound());       
    }

    public void Credit()
    {
        FindObjectOfType<AudioManager>().Play("ButtonClick");
        creditPanel.SetActive(true);
        buttonHandler.SetActive(false);
        creditPanel2.SetActive(false);
    }

    public void CreditNext()
    {
        FindObjectOfType<AudioManager>().Play("ButtonClick");
        creditPanel.SetActive(false);
        creditPanel2.SetActive(true);
    }

    public void CreditClose()
    {
        FindObjectOfType<AudioManager>().Play("ButtonClick");
        creditPanel.SetActive(false);
        buttonHandler.SetActive(true);
    }

    public void Quit()
    {
        Application.Quit();
    }

    IEnumerator PlaySound()
    {
        FindObjectOfType<AudioManager>().Play("ButtonClick");
        yield return new WaitForSeconds(0.3f);
        SceneManager.LoadScene("MainScene");
    }
}
