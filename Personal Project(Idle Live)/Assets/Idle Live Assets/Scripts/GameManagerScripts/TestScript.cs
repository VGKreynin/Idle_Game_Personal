using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TestScript : MonoBehaviour
{
    public void TestMethod()
    {
        for (int i = 0; i < SavableData.jobExpMaxValue.Length; i++)
        {
            Debug.Log("Lvl of " + i + " job = " + SavableData.jobLvlValue[i] + " MaxEXP = " + SavableData.jobExpMaxValue[i] + " Status = " + SavableData.jobEnabledStatus[i]);
        }
    }

    public void TestMethod2()
    {
        for (int i = 0; i < SavableData.jobExpMaxValue.Length; i++)
        {
            Debug.Log(i + " JobEnableStatus " + SavableData.jobEnabledStatus[i]);
            //Debug.Log(SavableData.jobExpMaxValue.Length);
        }
    }

    public void TestMethod3()
    {

        //SceneManager.LoadScene("Main scene");
        Debug.Log("CurrentJobNumber = " + SavableData.currentJobSelectedNumber);
        Debug.Log("IsJobActive? - " + SavableData.jobIsActive);
        Debug.Log("NewOrContinueGame - " + SavableData.newOrContinueGame);
    }
}
