using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartParameters : MonoBehaviour
{
        
    [Header("Job Required Skills")]
    public int[] cashierSkillReq;
    public int[] jrCoderSkillReq, srCoderSkillReq, prjmSkillReq, hodSkillReq, ceoSkillReq, presidentSkillReq;
    public int[,] jobRequiremetsMultiArray; //first value is job, second value is skill required values of each skill, from endurance 0 to Management 4

    [Header("Skill Required Skills")]
    public int[] disciplineSkillReq;
    public int[] motivationSkillReq, negotiatinsSkillReq, managementSkillReq;
    public int[,] skillRequiremetsMultiArray; //first value is job, second value is skill required values of each skill, from endurance 0 to Management 4

    [Header("Exp Hardener(How hard to get next level. Multiplier.)")]
    [SerializeField] private float jobExpHardenerX;
    [SerializeField] private float skillExpHardenerX, techExpHardenerX; //use to set value for public static variables below
    public static float jobExpHardener, skillExpHardener, techExpHardener; //next level of skill or job or tech demands more expirience. This variable shows how much in %

    [Header("Cost Increasers(For skills and Techs)")]
    [SerializeField] private float skillCostIncreaserX;
    [SerializeField] private float techCostIncreaserX; //use to set value for public static variables below
    public static float skillCostIncreaser, techCostIncreaser; //next level of skill or tech demands more expensive.
        
    [Header("Increase payment each lvl in %")]
    [SerializeField] private float jobPaymentLvlMultiplierX; 
    public static float jobPaymentLvlMultiplier; 
        
    [Header("Increase Ecology return by each lvl of technology in %")]
    [SerializeField] private float techEcologyUpgradeMultiplierX; 
    public static float techEcologyUpgradeMultiplier; 

    // Start is called before the first frame update
    void Start()
    {
        jobRequiremetsMultiArray = new int[8, (StaticFinalData.skillsNamesArray.Length + 1)];
        skillRequiremetsMultiArray = new int[5, StaticFinalData.skillsNamesArray.Length];
        LoadingJobMultiArray();
        LoadingSkillMultiArray();
        LoadValueForStaticVar();
    }  

    private void LoadingJobMultiArray()
    {
        
            for (int i = 0; i < StaticFinalData.skillsNamesArray.Length; i++)
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
        for (int i = 0; i < StaticFinalData.skillsNamesArray.Length; i++)
        {
            skillRequiremetsMultiArray[1, i] = disciplineSkillReq[i];
            skillRequiremetsMultiArray[2, i] = motivationSkillReq[i];
            skillRequiremetsMultiArray[3, i] = negotiatinsSkillReq[i];
            skillRequiremetsMultiArray[4, i] = managementSkillReq[i];   
        }
    }

    private void LoadValueForStaticVar()
    {
        jobExpHardener = jobExpHardenerX;
        skillExpHardener = skillExpHardenerX;
        techExpHardener = techExpHardenerX;

        skillCostIncreaser = skillCostIncreaserX;
        techCostIncreaser = techCostIncreaserX;

        jobPaymentLvlMultiplier = jobPaymentLvlMultiplierX;
        techEcologyUpgradeMultiplier = techEcologyUpgradeMultiplierX;
    }
}
