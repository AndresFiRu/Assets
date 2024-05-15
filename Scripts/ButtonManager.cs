using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonManager : MonoBehaviour
{

    private ApplicationManager manager;


    // Start is called before the first frame update
    void Start()
    {
        manager = GameObject.Find("ApplicationManager").GetComponent<ApplicationManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnBtnPlayer1()
    {
        manager.choice = 0;
        SceneManager.LoadScene("Game");
    }

    public void OnBtnPlayer2()
    {
        manager.choice = 1;
        SceneManager.LoadScene("Game");
    }

    public void OnBtnPlayer3()
    {
        manager.choice = 2;
        SceneManager.LoadScene("Game");
    }

    public void OnBtnRestart()
    {
        manager.ResetGame();        
    }

    public void OnBtnQuit()
    {
        manager.QuitGame();
    }
}
