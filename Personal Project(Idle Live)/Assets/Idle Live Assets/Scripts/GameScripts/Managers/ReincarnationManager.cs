using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class ReincarnationManager : MonoBehaviour
{
    public TextMeshProUGUI reincarnationPointsText;

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
        SavableData.reincarnationPoints = 1000; //Test
        LoadingReincarnationScreen();
    }    

    public void ReincarnationUpgrades(int x)
    {
        //JOB INCOME
        if (x == 1 && jobIncomeLvlCost[SavableData.jobIncMultLvlR] <= SavableData.reincarnationPoints) //Buying upgrade
        {
            SavableData.reincarnationPoints -= jobIncomeLvlCost[SavableData.jobIncMultLvlR];            
            reincarnationPointsText.text = SavableData.reincarnationPoints.ToString();
            SavableData.jobIncMultR += 0.05f;
            SavableData.jobIncMultLvlR += 1;                      
            jobIncText.text = "X: + " + (SavableData.jobIncMultR * 100).ToString("##") + "%"; //Updating gain % text
            jobIncLvlText.text = "Lvl: " + SavableData.jobIncMultLvlR + "/10"; //Updating lvl text
        }
        if (SavableData.jobIncMultLvlR == 10)
        {
            jobUpgradeButton.SetActive(false);
        }
        if (SavableData.jobIncMultLvlR < 10)
        {
            jobIncCostText.text = jobIncomeLvlCost[SavableData.jobIncMultLvlR] + " EP";
        }

        //JOB EXP
        if (x == 2 && jobExpLvlCost[SavableData.jobExpMultLvlR] <= SavableData.reincarnationPoints) //Buying upgrade
        {
            SavableData.reincarnationPoints -= jobExpLvlCost[SavableData.jobExpMultLvlR];
            reincarnationPointsText.text = SavableData.reincarnationPoints.ToString();
            SavableData.jobExpMultR += 0.05f;
            SavableData.jobExpMultLvlR += 1;                     
            jobExpText.text = "X: + " + (SavableData.jobExpMultR * 100).ToString("##") + "%"; //Updating gain % text
            jobExpLvlText.text = "Lvl: " + SavableData.jobExpMultLvlR + "/10"; //Updating lvl text
        }
        if (SavableData.jobExpMultLvlR == 10)
        {
            jobExpButton.SetActive(false);
        }
        if (SavableData.jobExpMultLvlR < 10)
        {
            jobExpCostText.text = jobExpLvlCost[SavableData.jobExpMultLvlR] + " EP";
        }

        //SKILL EXP
        if (x == 3 && skillExpLvlCost[SavableData.skillExpMultLvlR] <= SavableData.reincarnationPoints) //Buying upgrade
        {
            SavableData.reincarnationPoints -= skillExpLvlCost[SavableData.skillExpMultLvlR];            
            reincarnationPointsText.text = SavableData.reincarnationPoints.ToString();
            SavableData.skillExpMultR += 0.05f;
            SavableData.skillExpMultLvlR += 1;            
            skillExpText.text = "X: + " + (SavableData.skillExpMultR * 100).ToString("##") + "%"; //Updating gain % text
            skillExpLvlText.text = "Lvl: " + SavableData.skillExpMultLvlR + "/10"; //Updating lvl text
        }
        if (SavableData.skillExpMultLvlR == 10)
        {
            skillExpButton.SetActive(false);
        }
        if (SavableData.skillExpMultLvlR < 10)
        {
            skillExpCostText.text = skillExpLvlCost[SavableData.skillExpMultLvlR] + " EP";
        }

        //TECH EXP
        if (x == 4 && techExpLvlCost[SavableData.techExpMultLvlR] <= SavableData.reincarnationPoints) //Buying upgrade
        {
            SavableData.reincarnationPoints -= techExpLvlCost[SavableData.techExpMultLvlR];            
            reincarnationPointsText.text = SavableData.reincarnationPoints.ToString();
            SavableData.techExpMultR += 0.05f;
            SavableData.techExpMultLvlR += 1;            
            techExpText.text = "X: + " + (SavableData.techExpMultR * 100).ToString("##") + "%"; //Updating gain % text
            techExpLvlText.text = "Lvl: " + SavableData.techExpMultLvlR + "/10"; //Updating lvl text
        }
        if (SavableData.techExpMultLvlR == 10)
        {
            techExpButton.SetActive(false);
        }
        if (SavableData.techExpMultLvlR < 10)
        {
            techExpCostText.text = techExpLvlCost[SavableData.techExpMultLvlR] + " EP";
        }

        //TECH COST
        if (x == 5 && techCostLvlCost[SavableData.techCostMultLvlR] <= SavableData.reincarnationPoints) //Buying upgrade
        {
            SavableData.reincarnationPoints -= techExpLvlCost[SavableData.techCostMultLvlR];            
            reincarnationPointsText.text = SavableData.reincarnationPoints.ToString();
            SavableData.techCostMultR -= 0.01f;
            SavableData.techCostMultLvlR += 1;
            techCostText.text = "X: - " + (Mathf.Abs(SavableData.techCostMultR * 100)).ToString("##") + "%"; //Updating gain % text
            techCostLvlText.text = "Lvl: " + SavableData.techCostMultLvlR + "/10"; //Updating lvl text
        }
        if (SavableData.techCostMultLvlR == 10)
        {
            techCostButton.SetActive(false);
        }
        if (SavableData.techCostMultLvlR < 10)
        {
            techCostCostText.text = techCostLvlCost[SavableData.techCostMultLvlR] + " EP";
        }
    }    

    public void HardResetPrefs()
    {
        SavableData.reincarnationPoints = 0;
        SavableData.jobIncMultR = 0;
        SavableData.jobIncMultLvlR = 0;
        SavableData.jobExpMultR = 0;
        SavableData.jobExpMultLvlR = 0;
        SavableData.skillExpMultR = 0;
        SavableData.skillExpMultLvlR = 0;
        SavableData.techExpMultR = 0;
        SavableData.techExpMultLvlR = 0;
        SavableData.techCostMultR = 0;
        SavableData.techCostMultLvlR = 0;        
    }

    public void LoadingReincarnationScreen()
    {
        // JOB INCOME 
        if (SavableData.jobIncMultR == 0)
        {
            jobIncText.text = "X: + 0%"; //Updating gain % text
        }
        else
        {
            jobIncText.text = "X: + " + (SavableData.jobIncMultR * 100).ToString("##") + "%"; //Updating gain % text
        }
        jobIncLvlText.text = "Lvl: " + SavableData.jobIncMultLvlR + "/10"; //Updating lvl text
        if (SavableData.jobIncMultLvlR < 10)
        {
            jobIncCostText.text = jobIncomeLvlCost[SavableData.jobIncMultLvlR] + " EP";
        }
        if (SavableData.jobIncMultLvlR == 10) //Button upgrade shouldnt appear after reload if upgrade is max
        {
            jobUpgradeButton.SetActive(false);
        }

        // JOB EXP    
        if (SavableData.jobExpMultR == 0)
        {
            jobExpText.text = "X: + 0%"; //Updating gain % text
        }
        else
        {
            jobExpText.text = "X: + " + (SavableData.jobExpMultR * 100).ToString("##") + "%"; //Updating gain % text
        }        
        jobExpLvlText.text = "Lvl: " + SavableData.jobExpMultLvlR + "/10"; //Updating lvl text
        if (SavableData.jobExpMultLvlR < 10)
        {
            jobExpCostText.text = jobExpLvlCost[SavableData.jobExpMultLvlR] + " EP";
        }
        if (SavableData.jobExpMultLvlR == 10) //Button upgrade shouldnt appear after reload if upgrade is max
        {
            jobExpButton.SetActive(false);
        }

        // SKILL EXP 
        if (SavableData.skillExpMultR == 0)
        {
            skillExpText.text = "X: + 0%"; //Updating gain % text
        }
        else
        {
            skillExpText.text = "X: + " + (SavableData.skillExpMultR * 100).ToString("##") + "%"; //Updating gain % text
        }        
        skillExpLvlText.text = "Lvl: " + SavableData.skillExpMultLvlR + "/10"; //Updating lvl text
        if (SavableData.skillExpMultLvlR < 10)
        {
            skillExpCostText.text = skillExpLvlCost[SavableData.skillExpMultLvlR] + " EP";
        }
        if (SavableData.skillExpMultLvlR == 10) //Button upgrade shouldnt appear after reload if upgrade is max
        {
            skillExpButton.SetActive(false);
        }

        // TECH EXP  
        if (SavableData.techExpMultR == 0)
        {
            techExpText.text = "X: + 0%"; //Updating gain % text
        }
        else
        {
            techExpText.text = "X: + " + (SavableData.techExpMultR * 100).ToString("##") + "%"; //Updating gain % text
        }
        techExpLvlText.text = "Lvl: " + SavableData.techExpMultLvlR + "/10"; //Updating lvl text
        if (SavableData.techExpMultLvlR < 10)
        {
            techExpCostText.text = techExpLvlCost[SavableData.techExpMultLvlR] + " EP";
        }
        if (SavableData.techExpMultLvlR == 10) //Button upgrade shouldnt appear after reload if upgrade is max
        {
            techExpButton.SetActive(false);
        }

        // TECH COST   
        if (SavableData.techCostMultR == 0)
        {            
            techCostText.text = "X: - 0%"; //Updating gain % text            
        }
        else
        {
            techCostText.text = "X: - " + (Mathf.Abs(SavableData.techCostMultR * 100)).ToString("##") + "%"; //Updating gain % text
        }        
        techCostLvlText.text = "Lvl: " + SavableData.techCostMultLvlR + "/10"; //Updating lvl text
        if (SavableData.techCostMultLvlR < 10)
        {
            techCostCostText.text = techCostLvlCost[SavableData.techCostMultLvlR] + " EP";
        }
        if (SavableData.techCostMultLvlR == 10) //Button upgrade shouldnt appear after reload if upgrade is max
        {
            techCostButton.SetActive(false);
        }
    }
}
