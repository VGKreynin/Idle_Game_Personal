﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    private StartParameters startParameters;
    private SaveLoadManager saveLoadScr;

    public TextMeshProUGUI moneyValueText;
    public TextMeshProUGUI ecologyValueText;
    public TextMeshProUGUI lvlValueText;
    public TextMeshProUGUI incomeValueText;
    public TextMeshProUGUI expensesValueText;
    public TextMeshProUGUI netValueText;
    public TextMeshProUGUI gameSpeedValueText;

    [HideInInspector] public float moneyValue, ecologyValue, incomeValue, totalExpenses;
    [HideInInspector] public float ecoTechExpensesValue, ecoInvestmentsExpensesValue, skillExpensesValue;
                
    public float[] techMultipliersArray;//Contains multipliers of all Eco technologies

    private float netValue; 

    [HideInInspector] public Slider progressBar;    
    
    [HideInInspector] public string currentSkill; //Currently selected job and skill    

    [HideInInspector] public int[] skillsCurrentLvlArray; //Storing current lvls of all jobs and skills
        
    public float ecologyToWin; //Emount of Ecology point needed to win the game(WINCONDITION)    

    private void Awake()
    {        
        
        startParameters = gameObject.GetComponent<StartParameters>();
        saveLoadScr = gameObject.GetComponent<SaveLoadManager>();

        saveLoadScr.LoadingGame();
    }
    // Start is called before the first frame update
    void Start()
    {
        LoadMultipliersArray(); //Setting first value of all multipliers to 1        
        skillsCurrentLvlArray = new int[startParameters.skillsNamesArray.Length];

        SkillsLvlZero(); //assign all skills lvl 0 at the start in skillCurrentLvl Array
        IncExpNetVisualise();
        gameSpeedValueText.text = Time.timeScale.ToString("##"); //Loading start text value
    }

    // Update is called once per frame
    void Update()
    {
        NetCalculation();
        UpdateMoney(Time.deltaTime * netValue);
        IncExpNetVisualise();
    }
               
    public void ButtonJobDeactivation() //Stop all Jobs function
    {
        SavableData.jobIsActive = false;
    }

    private void UpdateMoney(float addProgress) //Current money calculation method
    {
        moneyValue += addProgress;
    }

    public void IncExpNetVisualise() //Function, that visualise income, expenses and net values when they changed
    {
        moneyValueText.text = moneyValue.ToString("##.#");
        if (netValue == 0)
        {
            netValueText.text = "0";
        }
        else
        {
            netValueText.text = netValue.ToString("##.#");
        }
        incomeValueText.text = incomeValue.ToString("##.#");
        expensesValueText.text = TotalExpenses().ToString("##.#");
        ecologyValueText.text = ecologyValue.ToString("##.#");
    }

    private void NetCalculation() //Net calculation method
    {
        incomeValue = SavableData.currentBasicJobPayment * SavableData.jobPayMultiplier[SavableData.currentJobSelectedNumber] * SavableData.motivMultiplierJobPay * (1 + SavableData.jobIncMultR);
        netValue = incomeValue - TotalExpenses();
    }

    private float TotalExpenses() //Calculating expenses from all activities
    {
        totalExpenses = (ecoTechExpensesValue * (1 + PlayerPrefs.GetFloat("TechCostMult"))) + (ecoInvestmentsExpensesValue / SavableData.managementMultiplierEcoCostDecr) + skillExpensesValue;
        return totalExpenses;
    }

    private void SkillsLvlZero() //assign all skills lvl 0 at the start in skillCurrentLvl Array
    {
        for (int i = 0; i < startParameters.skillsNamesArray.Length; i++)
        {
            skillsCurrentLvlArray[i] = 0;
        }
    }

    private void LoadMultipliersArray() //Setting first value of all multipliers to 1
    {           
        for (int i = 0; i < techMultipliersArray.Length; i++)
        {
            techMultipliersArray[i] = 1;
        }        
    }     

    public void GameSpeed(int addSpeed)
    {
        if(Time.timeScale > 0)
        {
            Time.timeScale += addSpeed;
            gameSpeedValueText.text = Time.timeScale.ToString("##");
            if(Time.timeScale == 0)
            {
                gameSpeedValueText.text = "0";
            }
        } else if(Time.timeScale == 0 && addSpeed > 0)
        {
            Time.timeScale += addSpeed;
            gameSpeedValueText.text = Time.timeScale.ToString("##");
        }
    }

    public void ExitToStartMenu() //Used to got to StartMenuScene
    {
        SceneManager.LoadScene("Start Menu");
    }                
    
}
