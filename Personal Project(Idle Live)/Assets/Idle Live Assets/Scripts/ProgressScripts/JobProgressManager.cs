using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class JobProgressManager : MonoBehaviour
{
    public TextMeshProUGUI lvlValueText;
    
    private Slider progressBar;

    private GameManager gameManagerScr;
    private JobRequirements jobRequirementsScr;
    private SkillRequirements skillRequirementsScr;        
    
    public float basicJobPayment; // How much you gain at this job basically       
    public int jobNumber;//Use to identify job


    // Start is called before the first frame update
    void Start()
    {
        //PlayerData.jobExpMaxValue[jobNumber] = 100;       
        lvlValueText.text = PlayerData.jobLvlValue[jobNumber].ToString();
        progressBar = GetComponent<Slider>();
        progressBar.value = PlayerData.jobExpCurrentValue[jobNumber];        
        gameManagerScr = GameObject.Find("GameManager").GetComponent<GameManager>();        
        jobRequirementsScr = GameObject.Find("Job Requirements Value").GetComponent<JobRequirements>();
        skillRequirementsScr = GameObject.Find("Skill Requirements Value").GetComponent<SkillRequirements>();
    }

    // Update is called once per frame
    void Update()
    {
        if (PlayerData.isJobActive == true && PlayerData.currentJobSelectedNumber == jobNumber)            
        {                   
            JobProgress(Time.deltaTime * 50 * PlayerData.skillMultipliersArray[1] * (1 + PlayerPrefs.GetFloat("JobExpMult")));                               
        }
        else if (PlayerData.jobLvlLoading[jobNumber] == true) //This triggered one time when game load
        {
            lvlValueText.text = PlayerData.jobLvlValue[jobNumber].ToString();
            progressBar.maxValue = PlayerData.jobExpMaxValue[jobNumber];
            PlayerData.jobLvlLoading[jobNumber] = false;
        }

    }
       
    private void JobProgress(float addProgress)
    {       
        progressBar.value += addProgress;
        PlayerData.jobExpCurrentValue[jobNumber] = progressBar.value; //This used to save/load data
        if (progressBar.value >= PlayerData.jobExpMaxValue[jobNumber])
        {
            progressBar.value = 0;
            PlayerData.jobExpCurrentValue[jobNumber] = 0; //This used to save/load data
            PlayerData.jobLvlValue[jobNumber] += 1;
            lvlValueText.text = PlayerData.jobLvlValue[jobNumber].ToString();
            PlayerData.jobExpMaxValue[jobNumber] *= gameManagerScr.expHardener[0]; //Each next level need more exprience
            progressBar.maxValue = PlayerData.jobExpMaxValue[jobNumber];
            PlayerData.jobPayMultiplier[jobNumber] += 0.03f; //3% increase of payment per level
            PlayerData.currentJobPayMultiplier = PlayerData.jobPayMultiplier[jobNumber];            
            jobRequirementsScr.skillLvlChangeTrigger = true; //when current level changed we need to refresh Requirements
            skillRequirementsScr.skillLvlChangeTrigger = true; //when current level changed we need to refresh Requirements
        } else if (PlayerData.jobLvlLoading[jobNumber] == true) //This triggered one time when game load
        {
            lvlValueText.text = PlayerData.jobLvlValue[jobNumber].ToString();
            progressBar.maxValue = PlayerData.jobExpMaxValue[jobNumber];
            PlayerData.jobLvlLoading[jobNumber] = false;
        }
    }
    public void JobActivation() //Activate job, when click on slider
    {
        PlayerData.isJobActive = true;        
        PlayerData.currentBasicJobPayment = basicJobPayment;
        PlayerData.currentJobPayMultiplier = PlayerData.jobPayMultiplier[jobNumber];
        PlayerData.currentJobSelectedNumber = jobNumber;
    }        
}
