using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartMenuScript : MonoBehaviour
{
    public void NewGame()
    {
        PlayerData.newOrContinueGame = 0;
        SceneManager.LoadScene("Main Scene");
    }

    public void ContinueGame()
    {
        PlayerData.newOrContinueGame = 1;
        SceneManager.LoadScene("Main Scene");
    }
    
}
