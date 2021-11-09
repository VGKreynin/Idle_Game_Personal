using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartParameters : MonoBehaviour
{
    public string[] jobsNamesArray;
    public string[] skillsNamesArray;
    
    [Header("Job Required Skills")]
    public int[] cashierSkillReq;
    public int[] jrCoderSkillReq, srCoderSkillReq, prjmSkillReq, hodSkillReq, ceoSkillReq, presidentSkillReq;
    public int[,] jobRequiremetsMultiArray; //first value is job, second value is skill required values of each skill, from endurance 0 to Management 4

    [Header("Skill Required Skills")]
    public int[] disciplineSkillReq;
    public int[] motivationSkillReq, negotiatinsSkillReq, managementSkillReq;
    public int[,] skillRequiremetsMultiArray; //first value is job, second value is skill required values of each skill, from endurance 0 to Management 4

    // Start is called before the first frame update
    void Start()
    {
        jobRequiremetsMultiArray = new int[8, (skillsNamesArray.Length + 1)];
        skillRequiremetsMultiArray = new int[5, skillsNamesArray.Length];
        LoadingJobMultiArray();
        LoadingSkillMultiArray();        
    }  

    private void LoadingJobMultiArray()
    {
        
            for (int i = 0; i < skillsNamesArray.Length; i++)
        {
            jobRequiremetsMultiArray[1, i] = cashierSkillReq[i];
            jobRequiremetsMultiArray[2, i] = jrCoderSkillReq[i];
            jobRequiremetsMultiArray[3, i] = srCoderSkillReq[i];
            jobRequiremetsMultiArray[4, i] = prjmSkillReq[i];
            jobRequiremetsMultiArray[5, i] = hodSkillReq[i];
            jobRequiremetsMultiArray[6, i] = ceoSkillReq[i];
            jobRequiremetsMultiArray[7, i] = presidentSkillReq[i];
        }    
    }

    private void LoadingSkillMultiArray()
    {
        for (int i = 0; i < skillsNamesArray.Length; i++)
        {
            skillRequiremetsMultiArray[1, i] = disciplineSkillReq[i];
            skillRequiremetsMultiArray[2, i] = motivationSkillReq[i];
            skillRequiremetsMultiArray[3, i] = negotiatinsSkillReq[i];
            skillRequiremetsMultiArray[4, i] = managementSkillReq[i];   
        }
    }
}
