using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TestScript : MonoBehaviour
{
    public void TestMethod()
    {
        for (int i = 0; i < PlayerData.jobExpMaxValue.Length; i++)
        {
            Debug.Log("Lvl of " + i + " job = " + PlayerData.jobLvlValue[i] + " MaxEXP = " + PlayerData.jobExpMaxValue[i] + " Status = " + PlayerData.jobEnabledStatus[i]);
        }
    }

    public void TestMethod2()
    {
        for (int i = 0; i < PlayerData.jobExpMaxValue.Length; i++)
        {
            Debug.Log(i + " JobEnableStatus " + PlayerData.jobEnabledStatus[i]);
            //Debug.Log(PlayerData.jobExpMaxValue.Length);
        }
    }

    public void TestMethod3()
    {

        //SceneManager.LoadScene("Main scene");
        Debug.Log("CurrentJobNumber = " + PlayerData.currentJobSelectedNumber);
        Debug.Log("IsJobActive? - " + PlayerData.isJobActive);
        Debug.Log("NewOrContinueGame - " + PlayerData.newOrContinueGame);
    }
}
