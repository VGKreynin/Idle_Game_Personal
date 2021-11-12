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
        if (PlayerData.isSkillActive == true && PlayerData.currentSkillSelectedNumber == skillNumber)
        {
            SkillProgress(Time.deltaTime * 50 * PlayerData.skillMultipliersArray[0] * (1 + PlayerPrefs.GetFloat("SkillExpMult")));
        }
    }

    private void SkillProgress(float addProgress)
    {
        
        progressBar.value += addProgress;
        PlayerData.skillExpCurrentValue[skillNumber] = progressBar.value; //This used to save/load data
        if (progressBar.value >= PlayerData.skillExpMaxValue[skillNumber])
        {            
            progressBar.value = 0;
            PlayerData.skillExpCurrentValue[skillNumber] = 0; //This used to save/load data
            PlayerData.skillLvlValue[skillNumber] += 1;            
            gameManagerScr.skillExpensesValue -= PlayerData.skillCost[skillNumber]; //We need to erase old value of skill, to assign new value
            PlayerData.skillCost[skillNumber] *= gameManagerScr.costIncreaser[0];
            gameManagerScr.skillExpensesValue += PlayerData.skillCost[skillNumber];
            lvlValueText.text = PlayerData.skillLvlValue[skillNumber].ToString();
            PlayerData.skillExpMaxValue[skillNumber] *= gameManagerScr.expHardener[1]; //Each next level need more exprience
            progressBar.maxValue = PlayerData.skillExpMaxValue[skillNumber];
            PlayerData.skillMultipliersArray[skillNumber] += skillMultiplier; //Each next level global multiplier for skillMultipler            
            jobRequirementsScr.skillLvlChangeTrigger = true; //when current level changed we need to refresh Requirements
            skillRequirementsScr.skillLvlChangeTrigger = true; //when current level changed we need to refresh Requirements
        }
    }

    public void SkillActivation()
    {
        int x = 0;

        if (PlayerData.isSkillActive == false && gameManagerScr.moneyValue > PlayerData.skillCost[skillNumber]) //Starting skill progress
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
    