using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuScript : MonoBehaviour
{

    void Update()
    {
        //exit game on esc
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }

    //load level
    public void NextScene()
    {
        SceneManager.LoadScene(1);
    }

    //credits
    public void CreditsScene()
    {
        SceneManager.LoadScene(2);
    }

    //howtoplay
    public void HowToScene()
    {
        SceneManager.LoadScene(3);
    }

    //exit game
    public void ExitGame()
    {
        Application.Quit();
    }

    //GoBack
    public void GoBack()
    {
        SceneManager.LoadScene(0);
    }

}
