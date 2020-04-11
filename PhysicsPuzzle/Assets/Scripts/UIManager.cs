using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Dropdown dropdown;
    public bool sampleScene = false;
    public bool deuterBool = false;
    public bool protanBool = false;
    public bool tritanBool = false;

    public void Quit()
    {
        Application.Quit(); // quits the application

    }
    private void Update()
    {
        if (dropdown.value == 0)
        {
            sampleScene = true;
            deuterBool = false;
            protanBool = false;
            tritanBool = false;

        }

        if (dropdown.value == 1)
        {
            sampleScene = false;
            deuterBool = true;
            protanBool = false;
            tritanBool = false;


        }
        if (dropdown.value == 2)
        {
            sampleScene = false;
            deuterBool = false;
            protanBool = true;
            tritanBool = false;

        }
        if (dropdown.value == 3)
        {
            sampleScene = false;
            deuterBool = false;
            protanBool = false;
            tritanBool = true;


        }
    }
    public void Play()
    {
        if (dropdown.value == 0)
        {
           // sampleScene = true;
            SceneManager.LoadScene("SampleScene"); // loads game scene
            
        }

        if (dropdown.value == 1)
        {
            //deuterBool = true;
            SceneManager.LoadScene("Deuteranopia"); // loads game scene
           
        }
        if (dropdown.value == 2)
        {
            //protanBool = true;
            SceneManager.LoadScene("Protanopia"); // loads game scene
            
        }
        if (dropdown.value == 3)
        {
            //tritanBool = true;
            SceneManager.LoadScene("Tritanopia"); // loads game scene
            
        }

    }
    public void Restart()
    {
        print("test");
        if (sampleScene == true)
        {
            print("Sampletest");
            SceneManager.LoadScene("SampleScene"); // loads game scene
        }
        if (deuterBool == true)
        {
            print("Detest");
            SceneManager.LoadScene("Deuteranpia"); // loads game scene
        }
        if (protanBool == true)
        {
            print("Protantest");
            SceneManager.LoadScene("Protanopia"); // loads game scene
        }
        if (tritanBool == true)
        {
            print("Tritantest");
            SceneManager.LoadScene("Tritanopia"); // loads game scene

        }
    }
    public void MainMenu()
    {

        SceneManager.LoadScene("MainMenu"); // loads main menu

    }
}
