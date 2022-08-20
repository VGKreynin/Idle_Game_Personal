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
        //SavableData.jobExpMaxValue[jobNumber] = 100;       
        lvlValueText.text = SavableData.jobLvlValue[jobNumber].ToString();
        progressBar = GetComponent<Slider>();
        progressBar.value = SavableData.jobExpCurrentValue[jobNumber];                
        jobRequirementsScr = GameObject.Find("Job Requirements Value").GetComponent<JobRequirements>();
        skillRequirementsScr = GameObject.Find("Skill Requirements Value").GetComponent<SkillRequirements>();
    }

    // Update is called once per frame
    void Update()
    {
        if (SavableData.jobIsActive == true && SavableData.currentJobSelectedNumber == jobNumber)            
        {                   
            JobProgress(Time.deltaTime * 50 * SavableData.discMultiplierJobExp * (1 + PlayerPrefs.GetFloat("JobExpMult")));                               
        }
        else if (SavableData.jobLvlLoading[jobNumber] == true) //This triggered one time when game load
        {
            lvlValueText.text = SavableData.jobLvlValue[jobNumber].ToString();
            progressBar.maxValue = SavableData.jobExpMaxValue[jobNumber];
            SavableData.jobLvlLoading[jobNumber] = false;
        }

    }
       
    private void JobProgress(float addProgress)
    {       
        progressBar.value += addProgress;
        SavableData.jobExpCurrentValue[jobNumber] = progressBar.value; //This used to save/load data
        if (progressBar.value >= SavableData.jobExpMaxValue[jobNumber])
        {
            progressBar.value = 0;
            SavableData.jobExpCurrentValue[jobNumber] = 0; //This used to save/load data
            SavableData.jobLvlValue[jobNumber] += 1;
            lvlValueText.text = SavableData.jobLvlValue[jobNumber].ToString();
            SavableData.jobExpMaxValue[jobNumber] *= StartParameters.jobExpHardener; //Each next level need more exprience
            progressBar.maxValue = SavableData.jobExpMaxValue[jobNumber];
            SavableData.jobPayMultiplier[jobNumber] += StartParameters.jobPaymentLvlMultiplier; //increase of payment per level            
            SavableData.currentJobPayMultiplier = SavableData.jobPayMultiplier[jobNumber];            
            jobRequirementsScr.skillLvlChangeTrigger = true; //when current level changed we need to refresh Requirements
            skillRequirementsScr.skillLvlChangeTrigger = true; //when current level changed we need to refresh Requirements
        } else if (SavableData.jobLvlLoading[jobNumber] == true) //This triggered one time when game load
        {
            lvlValueText.text = SavableData.jobLvlValue[jobNumber].ToString();
            progressBar.maxValue = SavableData.jobExpMaxValue[jobNumber];
            SavableData.jobLvlLoading[jobNumber] = false;
        }
    }
    public void JobActivation() //Activate job, when click on slider
    {
        SavableData.jobIsActive = true;        
        SavableData.currentBasicJobPayment = basicJobPayment;
        SavableData.currentJobPayMultiplier = SavableData.jobPayMultiplier[jobNumber];
        SavableData.currentJobSelectedNumber = jobNumber;
    }        
}
