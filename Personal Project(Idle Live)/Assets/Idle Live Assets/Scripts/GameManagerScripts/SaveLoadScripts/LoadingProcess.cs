using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadingProcess : MonoBehaviour
{

    private void Awake()
    {
        Debug.Log("LoadingProcessScript");
        JobsActivationLoading();
    }    

    private void JobsActivationLoading() //When loading the game, we activate particular jobs according to saved progress
    {
        
        for (int i = 0; i < StaticFinalData.jobsArray.Length; i++) //Copying jobs gameobjects to static massive
        {
            
            StaticFinalData.jobsArray[i].SetActive(SavableData.jobEnabledStatus[i]);
        }
    }
}
