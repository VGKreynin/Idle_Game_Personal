using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TestScript : MonoBehaviour
{
    public void TestMethod()
    {
        for (int i = 0; i < SavableData.jobExpMaxValueArray.Length; i++)
        {
            Debug.Log("Lvl of " + i + " job = " + SavableData.jobLvlValueArray[i] + " MaxEXP = " + SavableData.jobExpMaxValueArray[i] + " Status = " + SavableData.jobEnabledStatusArray[i]);
        }
    }

    public void TestMethod2()
    {
        for (int i = 0; i < SavableData.jobExpMaxValueArray.Length; i++)
        {
            Debug.Log(i + " JobEnableStatus " + SavableData.jobEnabledStatusArray[i]);
            //Debug.Log(SavableData.jobExpMaxValue.Length);
        }
    }

    public void TestMethod3()
    {

        //SceneManager.LoadScene("Main scene");
        Debug.Log("CurrentJobNumber = " + SavableData.jobCurrentSelectedNumber);
        Debug.Log("IsJobActive? - " + SavableData.jobIsActive);
        Debug.Log("NewOrContinueGame - " + SavableData.newOrContinueGame);
    }
}
