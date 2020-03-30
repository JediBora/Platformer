using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public void Quit()
    {
        Application.Quit(); // quits the application

    }

    public void Play()
    {
        SceneManager.LoadScene("SampleScene"); // loads game scene

    }
    public void Restart()
    {
        SceneManager.LoadScene("SampleScene"); // loads game scene

    }
    public void MainMenu()
    {

        SceneManager.LoadScene("MainMenu"); // loads main menu

    }
}
