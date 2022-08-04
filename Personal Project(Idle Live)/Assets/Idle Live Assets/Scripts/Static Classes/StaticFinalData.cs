using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaticFinalData : MonoBehaviour
{
    public string[] jobsNamesArrayX, skillsNamesArrayX;
    public static string[] jobsNamesArray, skillsNamesArray; //Contain all names of all jobs and skills

    [SerializeField] private GameObject[] jobsArrayX; //This Array stores all jobs and translate them to JobArray static
    public static GameObject[] jobsArray = new GameObject[8]; //This Array stores all jobs Gameobjects to use

    private void Awake()
    {
        //Debug.Log("LoadingStaticFinalScript");
        JobsArrayLoading();
    }   

    private void JobsArrayLoading()
    {        
        for (int i = 0; i < jobsArrayX.Length; i++) //Copying jobs gameobjects to static massive
        {
            
            jobsArray[i] = jobsArrayX[i];
            
        }        
    }
}
