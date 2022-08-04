using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartMenuScript : MonoBehaviour
{
     public void NewGame()
    {
        SavableData.newOrContinueGame = 0;
        SceneManager.LoadScene("Main Scene");
    }

    public void ContinueGame()
    {
        SavableData.newOrContinueGame = 1;
        SceneManager.LoadScene("Main Scene");
    }
    
}
