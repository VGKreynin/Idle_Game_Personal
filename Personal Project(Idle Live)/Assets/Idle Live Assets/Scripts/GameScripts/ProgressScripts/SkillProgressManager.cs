using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class SkillProgressManager : MonoBehaviour
{
    public TextMeshProUGUI lvlValueText;
    private GameManager gameManagerScr;
    private JobRequirements jobRequirementsScr;
    private SkillRequirements skillRequirementsScr;

    private Slider progressBar;    
    
    [HideInInspector] public int currentLvl;    
    public int skillNumber; //indentification number of skill
    public float skillMultiplier; //Personal skill multiplier value
    //Endurance - Skill Experience //Discipline - Job Experience //Motivation - Job payment //Negotiations - Ecology income //Management - Ecology cost decrease

    public float skillBasicCost; //Starting cost of skill
    [SerializeField]private float skillCurrentCost; //Current cost of skill


    // Start is called before the first frame update
    void Start()
    {
        lvlValueText.text = SavableData.skillLvlValueArray[skillNumber].ToString();
        progressBar = GetComponent<Slider>();
        progressBar.value = SavableData.skillExpCurrentValueArray[skillNumber];
        progressBar.maxValue = SavableData.skillExpMaxValueArray[skillNumber];
        gameManagerScr = GameObject.Find("GameManager").GetComponent<GameManager>();
        jobRequirementsScr = GameObject.Find("Job Requirements Value").GetComponent<JobRequirements>();
        skillRequirementsScr = GameObject.Find("Skill Requirements Value").GetComponent<SkillRequirements>();

        skillCurrentCost = skillBasicCost;
    }

    // Update is called once per frame
    void Update()
    {
        MoneyEnding();
        if (SavableData.skillIsActive == true && SavableData.skillCurrentSelectedNumber == skillNumber)
        {
            SkillProgress(Time.deltaTime * 50 * SavableData.enduranceMultiplierSkillExp * (1 + PlayerPrefs.GetFloat("SkillExpMult")));
        }
    }

    private void SkillProgress(float addProgress)
    {        
        progressBar.value += addProgress;
        SavableData.skillExpCurrentValueArray[skillNumber] = progressBar.value; //This used to save/load data
        if (progressBar.value >= SavableData.skillExpMaxValueArray[skillNumber])
        {            
            progressBar.value = 0;
            SavableData.skillExpCurrentValueArray[skillNumber] = 0; //This used to save/load data
            SavableData.skillLvlValueArray[skillNumber] += 1;
            lvlValueText.text = SavableData.skillLvlValueArray[skillNumber].ToString();
            gameManagerScr.skillExpensesValue -= skillCurrentCost; //We need to erase old value of skill, to assign new value
            skillCurrentCost *= StartParameters.skillCostIncreaser;
            gameManagerScr.skillExpensesValue += skillCurrentCost;
            SavableData.skillExpMaxValueArray[skillNumber] *= StartParameters.skillExpHardener; //Each next level need more exprience
            progressBar.maxValue = SavableData.skillExpMaxValueArray[skillNumber];
            SkillGlobalMultiplierIncrease(skillNumber); //Each next level global multiplier for skillMultipler            
            
            jobRequirementsScr.skillLvlChangeTrigger = true; //when current level changed we need to refresh Requirements
            skillRequirementsScr.skillLvlChangeTrigger = true; //when current level changed we need to refresh Requirements
            //skill
        }
    }

    public void SkillActivation()
    {
        int x = 0;

        if (SavableData.skillIsActive == false && gameManagerScr.moneyValue > skillCurrentCost) //Starting skill progress
        {
            SavableData.skillIsActive = true;
            SavableData.skillCurrentSelectedNumber = skillNumber;
            x = 1;
            gameManagerScr.skillExpensesValue += skillCurrentCost;
        }

        if (SavableData.skillIsActive == true && x == 0 && SavableData.skillCurrentSelectedNumber == skillNumber) //Stopping skill progress
        {
            SavableData.skillIsActive = false;
            gameManagerScr.skillExpensesValue = 0;
        }

        if (SavableData.skillIsActive == true && x == 0 && SavableData.skillCurrentSelectedNumber != skillNumber) //If push other skill button, we stop previous skill, and start this skill
        {
            SavableData.skillCurrentSelectedNumber = skillNumber;
            gameManagerScr.skillExpensesValue = 0;
            gameManagerScr.skillExpensesValue += skillCurrentCost;
        }

    }

    private void MoneyEnding() //When money become 0, all expenses should be stopped method
    {

        if (gameManagerScr.moneyValue < 0)
        {
            SavableData.skillIsActive = false;
            gameManagerScr.skillExpensesValue = 0;
        }
    }

    private void SkillGlobalMultiplierIncrease(int skillNumber)
    {
        switch (skillNumber)
        {
            case 0: 
                SavableData.enduranceMultiplierSkillExp += skillMultiplier;
                break;
            case 1:
                SavableData.discMultiplierJobExp += skillMultiplier;
                break;
            case 2:
                SavableData.motivMultiplierJobPay += skillMultiplier;
                break;
            case 3:
                SavableData.negotiationMultiplierEcoIncome += skillMultiplier;
                break;
            case 4:
                SavableData.managementMultiplierEcoCostDecr += skillMultiplier;
                break;
        }
    }
}
    