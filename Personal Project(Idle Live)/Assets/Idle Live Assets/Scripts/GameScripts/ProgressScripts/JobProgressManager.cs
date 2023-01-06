using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class JobProgressManager : MonoBehaviour
{
    public TextMeshProUGUI lvlValueText;
    
    private Slider progressBar;
    
    private JobRequirements jobRequirementsScr;
    private SkillRequirements skillRequirementsScr;        
    
    public float basicJobPayment; // How much you gain at this job basically       
    public int jobNumber;//Use to identify job


    // Start is called before the first frame update
    void Start()
    {            
        lvlValueText.text = SavableData.jobLvlValueArray[jobNumber].ToString();
        progressBar = GetComponent<Slider>();
        progressBar.value = SavableData.jobExpCurrentValueArray[jobNumber];
        progressBar.maxValue = SavableData.jobExpMaxValueArray[jobNumber];
        jobRequirementsScr = GameObject.Find("Job Requirements Value").GetComponent<JobRequirements>();
        skillRequirementsScr = GameObject.Find("Skill Requirements Value").GetComponent<SkillRequirements>();
    }

    // Update is called once per frame
    void Update()
    {
        if (SavableData.jobIsActive == true && SavableData.jobCurrentSelectedNumber == jobNumber)            
        {                   
            JobProgress(Time.deltaTime * 50 * SavableData.discMultiplierJobExp * (1 + PlayerPrefs.GetFloat("JobExpMult")));                               
        }        

    }
       
    private void JobProgress(float addProgress)
    {       
        progressBar.value += addProgress;
        SavableData.jobExpCurrentValueArray[jobNumber] = progressBar.value; //This used to save/load data
        if (progressBar.value >= SavableData.jobExpMaxValueArray[jobNumber])
        {
            progressBar.value = 0;
            SavableData.jobExpCurrentValueArray[jobNumber] = 0; //This used to save/load data
            SavableData.jobLvlValueArray[jobNumber] += 1;
            lvlValueText.text = SavableData.jobLvlValueArray[jobNumber].ToString();
            SavableData.jobExpMaxValueArray[jobNumber] *= StartParameters.jobExpHardener; //Each next level need more exprience
            progressBar.maxValue = SavableData.jobExpMaxValueArray[jobNumber];
            SavableData.jobPayMultiplierArray[jobNumber] += StartParameters.jobPaymentLvlMultiplier; //increase of payment per level            
            SavableData.currentJobPayMultiplier = SavableData.jobPayMultiplierArray[jobNumber];            
            jobRequirementsScr.skillLvlChangeTrigger = true; //when current level changed we need to refresh Requirements
            skillRequirementsScr.skillLvlChangeTrigger = true; //when current level changed we need to refresh Requirements
        }
    }
    public void JobActivation() //Activate job, when click on slider
    {
        SavableData.jobIsActive = true;        
        SavableData.currentBasicJobPayment = basicJobPayment;
        SavableData.currentJobPayMultiplier = SavableData.jobPayMultiplierArray[jobNumber];
        SavableData.jobCurrentSelectedNumber = jobNumber;
    }        
}
