using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class ReincarnationManager : MonoBehaviour
{
    public TextMeshProUGUI ecoPointsText;

    [Header("JOB INCOME BUTTON")] //Parameters for this button
    public int[] jobIncomeLvlCost; //The cost of upgrage in EcoPoints
    public TextMeshProUGUI jobIncText, jobIncLvlText, jobIncCostText;    
    public GameObject jobUpgradeButton;

    [Header("JOB EXP BUTTON")]
    public int[] jobExpLvlCost; //The cost of upgrage in EcoPoints
    public TextMeshProUGUI jobExpText, jobExpLvlText, jobExpCostText;
    public GameObject jobExpButton;

    [Header("SKILL EXP BUTTON")]
    public int[] skillExpLvlCost; //The cost of upgrage in EcoPoints
    public TextMeshProUGUI skillExpText, skillExpLvlText, skillExpCostText;
    public GameObject skillExpButton;
    
    [Header("TECH EXP BUTTON")]
    public int[] techExpLvlCost; //The cost of upgrage in EcoPoints
    public TextMeshProUGUI techExpText, techExpLvlText, techExpCostText;
    public GameObject techExpButton;
    
    [Header("TECH COST BUTTON")]
    public int[] techCostLvlCost; //The cost of upgrage in EcoPoints
    public TextMeshProUGUI techCostText, techCostLvlText, techCostCostText;
    public GameObject techCostButton;

    // Start is called before the first frame update
    void Start()
    {
        //Test eco Points to start
        int x = 100;
        PlayerPrefs.SetInt("Ecology Points", x);

        LoadingReincarnationScreen();
    }    

    public void ReincarnationUpgrades(int x)
    {
        //JOB INCOME
        if (x == 1 && jobIncomeLvlCost[PlayerData.jobIncMultLvlR] <= PlayerPrefs.GetInt("Ecology Points")) //Buying upgrade
        {
            int y = PlayerPrefs.GetInt("Ecology Points");
            y -= jobIncomeLvlCost[PlayerData.jobIncMultLvlR];
            PlayerPrefs.SetInt("Ecology Points", y);
            ecoPointsText.text = y.ToString();
            PlayerData.jobIncMultR += 0.05f;
            PlayerData.jobIncMultLvlR += 1;                      
            jobIncText.text = "X: +" + (PlayerData.jobIncMultR * 100).ToString("##") + "%"; //Updating gain % text
            jobIncLvlText.text = "Lvl: " + PlayerData.jobIncMultLvlR + "/10"; //Updating lvl text
        }
        if (PlayerData.jobIncMultLvlR == 10)
        {
            jobUpgradeButton.SetActive(false);
        }
        if (PlayerData.jobIncMultLvlR < 10)
        {
            jobIncCostText.text = jobIncomeLvlCost[PlayerData.jobIncMultLvlR] + " EP";
        }

        //JOB EXP
        if (x == 2 && jobExpLvlCost[PlayerData.jobExpMultLvlR] <= PlayerPrefs.GetInt("Ecology Points")) //Buying upgrade
        {
            int y = PlayerPrefs.GetInt("Ecology Points");
            y -= jobExpLvlCost[PlayerData.jobExpMultLvlR];
            PlayerPrefs.SetInt("Ecology Points", y);
            ecoPointsText.text = y.ToString();
            PlayerData.jobExpMultR += 0.05f;
            PlayerData.jobExpMultLvlR += 1;                     
            jobExpText.text = "X: +" + (PlayerData.jobExpMultR * 100).ToString("##") + "%"; //Updating gain % text
            jobExpLvlText.text = "Lvl: " + PlayerData.jobExpMultLvlR + "/10"; //Updating lvl text
        }
        if (PlayerData.jobExpMultLvlR == 10)
        {
            jobExpButton.SetActive(false);
        }
        if (PlayerData.jobExpMultLvlR < 10)
        {
            jobExpCostText.text = jobExpLvlCost[PlayerData.jobExpMultLvlR] + " EP";
        }

        //SKILL EXP
        if (x == 3 && skillExpLvlCost[PlayerData.skillExpMultLvlR] <= PlayerPrefs.GetInt("Ecology Points")) //Buying upgrade
        {
            int y = PlayerPrefs.GetInt("Ecology Points");
            y -= skillExpLvlCost[PlayerData.skillExpMultLvlR];
            PlayerPrefs.SetInt("Ecology Points", y);
            ecoPointsText.text = y.ToString();
            PlayerData.skillExpMultR += 0.05f;
            PlayerData.skillExpMultLvlR += 1;            
            skillExpText.text = "X: +" + (PlayerData.skillExpMultR * 100).ToString("##") + "%"; //Updating gain % text
            skillExpLvlText.text = "Lvl: " + PlayerData.skillExpMultLvlR + "/10"; //Updating lvl text
        }
        if (PlayerData.skillExpMultLvlR == 10)
        {
            skillExpButton.SetActive(false);
        }
        if (PlayerData.skillExpMultLvlR < 10)
        {
            skillExpCostText.text = skillExpLvlCost[PlayerData.skillExpMultLvlR] + " EP";
        }

        //TECH EXP
        if (x == 4 && techExpLvlCost[PlayerData.techExpMultLvlR] <= PlayerPrefs.GetInt("Ecology Points")) //Buying upgrade
        {
            int y = PlayerPrefs.GetInt("Ecology Points");
            y -= techExpLvlCost[PlayerData.techExpMultLvlR];
            PlayerPrefs.SetInt("Ecology Points", y);
            ecoPointsText.text = y.ToString();
            PlayerData.techExpMultR += 0.05f;
            PlayerData.techExpMultLvlR += 1;            
            techExpText.text = "X: +" + (PlayerData.techExpMultR * 100).ToString("##") + "%"; //Updating gain % text
            techExpLvlText.text = "Lvl: " + PlayerData.techExpMultLvlR + "/10"; //Updating lvl text
        }
        if (PlayerData.techExpMultLvlR == 10)
        {
            techExpButton.SetActive(false);
        }
        if (PlayerData.techExpMultLvlR < 10)
        {
            techExpCostText.text = techExpLvlCost[PlayerData.techExpMultLvlR] + " EP";
        }

        //TECH COST
        if (x == 5 && techCostLvlCost[PlayerData.techCostMultLvlR] <= PlayerPrefs.GetInt("Ecology Points")) //Buying upgrade
        {
            int y = PlayerPrefs.GetInt("Ecology Points");
            y -= techExpLvlCost[PlayerData.techCostMultLvlR];
            PlayerPrefs.SetInt("Ecology Points", y);
            ecoPointsText.text = y.ToString();
            PlayerData.techCostMultR -= 0.01f;
            PlayerData.techCostMultLvlR += 1;
            techCostText.text = "X: " + (PlayerData.techCostMultR * 100).ToString("##") + "%"; //Updating gain % text
            techCostLvlText.text = "Lvl: " + PlayerData.techCostMultLvlR + "/10"; //Updating lvl text
        }
        if (PlayerData.techCostMultLvlR == 10)
        {
            techCostButton.SetActive(false);
        }
        if (PlayerData.techCostMultLvlR < 10)
        {
            techCostCostText.text = techCostLvlCost[PlayerData.techCostMultLvlR] + " EP";
        }
    }    

    public void HardResetPrefs()
    {
        PlayerPrefs.DeleteKey("Ecology Points");
        PlayerData.jobIncMultR = 0;
        PlayerData.jobIncMultLvlR = 0;
        PlayerData.jobExpMultR = 0;
        PlayerData.jobExpMultLvlR = 0;
        PlayerData.skillExpMultR = 0;
        PlayerData.skillExpMultLvlR = 0;
        PlayerData.techExpMultR = 0;
        PlayerData.techExpMultLvlR = 0;
        PlayerData.techCostMultR = 0;
        PlayerData.techCostMultLvlR = 0;        
    }

    public void LoadingReincarnationScreen()
    {
        // JOB INCOME 
        if (PlayerData.jobIncMultR == 0)
        {
            jobIncText.text = "X: +0%"; //Updating gain % text
        }
        else
        {
            jobIncText.text = "X: " + (PlayerData.jobIncMultR * 100).ToString("##") + "%"; //Updating gain % text
        }
        jobIncLvlText.text = "Lvl: " + PlayerData.jobIncMultLvlR + "/10"; //Updating lvl text
        if (PlayerData.jobIncMultLvlR < 10)
        {
            jobIncCostText.text = jobIncomeLvlCost[PlayerData.jobIncMultLvlR] + " EP";
        }
        if (PlayerData.jobIncMultLvlR == 10) //Button upgrade shouldnt appear after reload if upgrade is max
        {
            jobUpgradeButton.SetActive(false);
        }

        // JOB EXP        
        jobExpText.text = "X: " + (PlayerData.jobExpMultR * 100 + 100).ToString("##") + "%"; //Updating gain % text
        jobExpLvlText.text = "Lvl: " + PlayerData.jobExpMultLvlR + "/10"; //Updating lvl text
        if (PlayerData.jobExpMultLvlR < 10)
        {
            jobExpCostText.text = jobExpLvlCost[PlayerData.jobExpMultLvlR] + " EP";
        }
        if (PlayerData.jobExpMultLvlR == 10) //Button upgrade shouldnt appear after reload if upgrade is max
        {
            jobExpButton.SetActive(false);
        }

        // SKILL EXP        
        skillExpText.text = "X: " + (PlayerData.skillExpMultR * 100 + 100).ToString("##") + "%"; //Updating gain % text
        skillExpLvlText.text = "Lvl: " + PlayerData.skillExpMultLvlR + "/10"; //Updating lvl text
        if (PlayerData.skillExpMultLvlR < 10)
        {
            skillExpCostText.text = skillExpLvlCost[PlayerData.skillExpMultLvlR] + " EP";
        }
        if (PlayerData.skillExpMultLvlR == 10) //Button upgrade shouldnt appear after reload if upgrade is max
        {
            skillExpButton.SetActive(false);
        }

        // TECH EXP        
        techExpText.text = "X: " + (PlayerData.techExpMultR * 100 + 100).ToString("##") + "%"; //Updating gain % text
        techExpLvlText.text = "Lvl: " + PlayerData.techExpMultLvlR + "/10"; //Updating lvl text
        if (PlayerData.techExpMultLvlR < 10)
        {
            techExpCostText.text = techExpLvlCost[PlayerData.techExpMultLvlR] + " EP";
        }
        if (PlayerData.techExpMultLvlR == 10) //Button upgrade shouldnt appear after reload if upgrade is max
        {
            techExpButton.SetActive(false);
        }

        // TECH COST        
        techCostText.text = "X: " + (PlayerData.techCostMultR * 100 + 100).ToString("##") + "%"; //Updating gain % text
        techCostLvlText.text = "Lvl: " + PlayerData.techCostMultLvlR + "/10"; //Updating lvl text
        if (PlayerData.techCostMultLvlR < 10)
        {
            techCostCostText.text = techCostLvlCost[PlayerData.techCostMultLvlR] + " EP";
        }
        if (PlayerData.techCostMultLvlR == 10) //Button upgrade shouldnt appear after reload if upgrade is max
        {
            techCostButton.SetActive(false);
        }
    }
}
