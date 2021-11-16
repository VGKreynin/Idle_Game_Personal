using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    private StartParameters startParameters;
    private SaveLoad saveLoadScr;

    public TextMeshProUGUI moneyValueText;
    public TextMeshProUGUI ecologyValueText;
    public TextMeshProUGUI lvlValueText;
    public TextMeshProUGUI incomeValueText;
    public TextMeshProUGUI expensesValueText;
    public TextMeshProUGUI netValueText;
    public TextMeshProUGUI gameSpeedValueText;

    [HideInInspector] public float moneyValue, ecologyValue, incomeValue, totalExpenses;
    [HideInInspector] public float ecoTechExpensesValue, ecoInvestmentsExpensesValue, skillExpensesValue;
    
    public float[] expHardener; //next level of skill or job or tech demands more expirience. This variable shows how much in %
    //0 - job hardener, 1 - skill hardener, 2 - ecoTechHardener
    public float[] costIncreaser; //next level of skill or tech demands more money. This variable shows how much in %
    //0 - skill, 1 - tech increaser
    public float[] techMultipliersArray;//Contains multipliers of all Eco technologies

    private float netValue; 

    [HideInInspector] public Slider progressBar;      
        
    public float ecologyToWin; //Emount of Ecology point needed to win the game(WINCONDITION)

    [SerializeField] private GameObject[] jobsArrayX; //This Array stores all jobs and translate them to JobArray static
    public static GameObject[] jobsArray = new GameObject[8]; //This Array stores all jobs Gameobjects to use
    [SerializeField] private GameObject[] skillsArrayX; //This Array stores all skills and translate them to SkillsArray static
    public static GameObject[] skillsArray = new GameObject[5]; //This Array stores all skills Gameobjects to use

    private void Awake()
    {
        JobsandSkillsActivationLoading(); //Loading activation Data of jobs
        
        startParameters = gameObject.GetComponent<StartParameters>();
        saveLoadScr = gameObject.GetComponent<SaveLoad>();

        saveLoadScr.LoadingGame();
    }
    // Start is called before the first frame update
    void Start()
    {
        LoadMultipliersArray(); //Setting first value of all multipliers to 1         

        SkillsLvlZero(); //assign all skills lvl 0 at the start in skillCurrentLvl Array
        IncExpNetVisualise();
        gameSpeedValueText.text = Time.timeScale.ToString("##"); //Loading start text value
    }

    // Update is called once per frame
    void Update()
    {
        MoneyEndingStopExpences();
        NetCalculation();
        UpdateMoney(Time.deltaTime * netValue);
        IncExpNetVisualise();

    }
               
    public void ButtonJobDeactivation() //Stop all Jobs function
    {
        PlayerData.isJobActive = false;
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
        incomeValue = PlayerData.currentBasicJobPayment * PlayerData.jobPayMultiplier[PlayerData.currentJobSelectedNumber] * PlayerData.skillMultipliersArray[2] * (1 + PlayerData.jobIncMultR);
        netValue = incomeValue - TotalExpenses();
    }

    private float TotalExpenses() //Calculating expenses from all activities
    {
        totalExpenses = (ecoTechExpensesValue * (1 + PlayerPrefs.GetFloat("TechCostMult"))) + (ecoInvestmentsExpensesValue / PlayerData.skillMultipliersArray[4]) + skillExpensesValue;
        return totalExpenses;
    }

    private void SkillsLvlZero() //assign all skills lvl 0 at the start
    {
        for (int i = 0; i < startParameters.skillsNamesArray.Length; i++)
        {
            PlayerData.skillLvlValue[i] = 0;
        }
    }

    private void LoadMultipliersArray() //Setting first value of all multipliers to 1
    {
        for (int i = 0; i < PlayerData.skillMultipliersArray.Length; i++)
        {
            PlayerData.skillMultipliersArray[i] = 1;
        }
        
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

    public void StartMenu()
    {
        SceneManager.LoadScene("Start Menu");
    }         

    public void JobsandSkillsActivationLoading()
    {
        Debug.Log("JobArrayLoading");
        for (int i = 0; i < jobsArrayX.Length; i++) //Copying jobs gameobjects to static massive
        {
            jobsArray[i] = jobsArrayX[i];
            jobsArray[i].SetActive(PlayerData.jobEnabledStatus[i]);
        }
        for (int i = 0; i < skillsArrayX.Length; i++) //Copying skills gameobjects to static massive
        {
            skillsArray[i] = skillsArrayX[i];
            skillsArray[i].SetActive(PlayerData.skillEnabledStatus[i]);
        }
    }
    public void NewOrContinueValue(int newOrContinue)
    {
        PlayerData.newOrContinueGame = newOrContinue;
        SceneManager.LoadScene("Main Scene");
    }

    private void MoneyEndingStopExpences() //When money become 0, all expenses should be stopped method
    {

        if (moneyValue < 0)
        {
            PlayerData.isSkillActive = false;
            skillExpensesValue = 0;
        }
    }
}
