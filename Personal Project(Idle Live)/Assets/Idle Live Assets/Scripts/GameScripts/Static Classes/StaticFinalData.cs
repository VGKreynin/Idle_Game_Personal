using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaticFinalData : MonoBehaviour
{
    [SerializeField] private string[] jobsNamesArrayX, skillsNamesArrayX;
    public static string[] jobsNamesArray, skillsNamesArray; //Contain all names of all jobs and skills

    [SerializeField] private GameObject[] jobsArrayX,skillsArrayX; //This Array stores all jobs,skills and translate them to static
    public static GameObject[] jobsArray = new GameObject[8]; //This Array stores all jobs Gameobjects to use
    public static GameObject[] skillsArray = new GameObject[5]; //This Array stores all skills Gameobjects to use

    private void Awake()
    {
        //Debug.Log("LoadingStaticFinalScript");
        JobsArrayLoading();
        SkillsArrayLoading();
    }   

    private void JobsArrayLoading()
    {
        jobsNamesArray = new string[jobsNamesArrayX.Length];
        jobsArray = new GameObject[jobsArrayX.Length];
        jobsArray = jobsArrayX;
        jobsNamesArray = jobsNamesArrayX;        
    }

    private void SkillsArrayLoading()
    {
        skillsNamesArray = new string[skillsNamesArrayX.Length];
        skillsArray = new GameObject[skillsArrayX.Length];
        skillsNamesArray = skillsNamesArrayX;
        skillsArray = skillsArrayX;
    }
}
