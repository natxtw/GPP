using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoseScene : MonoBehaviour
{
    public void GoBackToMainMenu()
    {
        SceneManager.LoadScene("Main Menu");
    }

    public void ExitTheGame()
    {
        //Application.Quit(); //Only uncomment this on release.
        UnityEditor.EditorApplication.isPlaying = false;
        Debug.Log("You should have quit the game");
    }
}
