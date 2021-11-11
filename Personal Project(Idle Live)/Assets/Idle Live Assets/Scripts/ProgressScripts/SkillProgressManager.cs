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
    
    private float skillProgressValue; //% of current lvl progress
    [HideInInspector] public int currentLvl;
    private float expMaxValue; //How much experience needed to reach next level 
    public int skillNumber; //indentification number of skill
    public float skillMultiplier; //Personal skill multiplier value
    //Endurance - Skill Experience //Discipline - Job Experience //Motivation - Job payment //Negotiations - Ecology income //Management - Ecology cost decrease

    public float skillBasicCost; //Starting cost of skill
    private float skillCurrentCost; //Current cost of skill


    public int[] skillSkillReq; //Array contains required values of skill for every Skill. 0 is Discipline, 3 is Management


    // Start is called before the first frame update
    void Start()
    {
        progressBar = GetComponent<Slider>();        

        lvlValueText.text = "0";
        progressBar.value = 0;
        gameManagerScr = GameObject.Find("GameManager").GetComponent<GameManager>();
        jobRequirementsScr = GameObject.Find("Job Requirements Value").GetComponent<JobRequirements>();
        skillRequirementsScr = GameObject.Find("Skill Requirements Value").GetComponent<SkillRequirements>();

        skillCurrentCost = skillBasicCost;
    }

    // Update is called once per frame
    void Update()
    {
        MoneyEnding();
        if (gameManagerScr.isSkillActive == true && gameManagerScr.currentSkill == gameObject.name)
        {
            SkillProgress(Time.deltaTime * 50 * PlayerData.skillMultipliersArray[0] * (1 + PlayerPrefs.GetFloat("SkillExpMult")));
        }
    }

    private void SkillProgress(float addProgress)
    {
        skillProgressValue += addProgress;
        progressBar.value += addProgress;
        if (skillProgressValue >= expMaxValue)
        {
            skillProgressValue = 0;
            progressBar.value = 0;
            currentLvl += 1;
            gameManagerScr.skillExpensesValue -= skillCurrentCost; //We need to erase old value of skill, to assign new value
            skillCurrentCost *= gameManagerScr.costIncreaser[0];
            gameManagerScr.skillExpensesValue += skillCurrentCost;
            lvlValueText.text = currentLvl.ToString();
            expMaxValue *= gameManagerScr.expHardener[1]; //Each next level need more exprience
            progressBar.maxValue = expMaxValue;
            PlayerData.skillMultipliersArray[skillNumber] += skillMultiplier; //Each next level global multiplier for skillMultipler            
            gameManagerScr.skillsCurrentLvlArray[skillNumber] = currentLvl; //updating array storing current lvls of skills
            jobRequirementsScr.skillLvlChangeTrigger = true; //when current level changed we need to refresh Requirements
            skillRequirementsScr.skillLvlChangeTrigger = true; //when current level changed we need to refresh Requirements
        }
    }

    public void SkillActivation()
    {
        int x = 0;

        if (gameManagerScr.isSkillActive == false && gameManagerScr.moneyValue > skillCurrentCost) //Starting skill progress
        {
            gameManagerScr.isSkillActive = true;
            gameManagerScr.currentSkill = gameObject.name;
            x = 1;
            gameManagerScr.skillExpensesValue += skillCurrentCost;
        }

        if (gameManagerScr.isSkillActive == true && x == 0 && gameManagerScr.currentSkill == gameObject.name) //Stopping skill progress
        {
            gameManagerScr.isSkillActive = false;
            gameManagerScr.skillExpensesValue = 0;
        }

        if (gameManagerScr.isSkillActive == true && x == 0 && gameManagerScr.currentSkill != gameObject.name) //If push other skill button, we stop previous skill, and start this skill
        {
            gameManagerScr.currentSkill = gameObject.name;
            gameManagerScr.skillExpensesValue = 0;
            gameManagerScr.skillExpensesValue += skillCurrentCost;
        }

    }

    private void MoneyEnding() //When money become 0, all expenses should be stopped method
    {

        if (gameManagerScr.moneyValue < 0)
        {
            gameManagerScr.isSkillActive = false;
            gameManagerScr.skillExpensesValue = 0;
        }
    }
}
    