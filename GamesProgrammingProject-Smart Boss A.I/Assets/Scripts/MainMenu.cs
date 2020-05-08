using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class MainMenu : MonoBehaviour
{
    //Main menu script
    public void StartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void QuitGame()
    {
        //Application.Quit(); //Only uncomment this on release.
        UnityEditor.EditorApplication.isPlaying = false;
        Debug.Log("You should have quit the game");
    }
}
