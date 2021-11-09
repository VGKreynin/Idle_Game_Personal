using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class EcologyManager : MonoBehaviour
{
    
    public TextMeshProUGUI ecologyLvlText;
    public TextMeshProUGUI ecoCostText;
    
    [HideInInspector] public GameManager gameManagerScr;
    [HideInInspector] public PlanetManager planetManagerScr;
    
    private float basicEcoGain; // How much you gain at this ecology basically
    public float ecoBasicLvlCost; // The cost of lvl of ecology
    public int ecoCurrentLvl;
    public int ecoInvestmentNumber;

    [HideInInspector] public int buttonMultiplier; //variable used to chose the right emount of investment you buy when specific koef button pushed
    

    // Start is called before the first frame update
    void Start()
    {        
        gameManagerScr = GameObject.Find("GameManager").GetComponent<GameManager>();
        planetManagerScr = GameObject.Find("Planet").GetComponent<PlanetManager>();
        ecoCostText.text = "Cost: " + ecoBasicLvlCost.ToString("##");
        buttonMultiplier = 1; //Default value
        basicEcoGain = 10;
                
    }

    // Update is called once per frame
    void Update()
    {
        UpdateEcology(Time.deltaTime * basicEcoGain * gameManagerScr.techMultipliersArray[ecoInvestmentNumber] * ecoCurrentLvl);
        MoneyEnding(); //When money become 0, all expenses should be stopped
        if (ecoCurrentLvl != 0)
        {
            ecologyLvlText.text = ecoCurrentLvl.ToString("##");
        }
        else
        {
            ecologyLvlText.text = "0";
        }
        
    }

    private void UpdateEcology(float addProgress)
    {
        gameManagerScr.ecologyValue += addProgress;
        planetManagerScr.sliderArray[ecoInvestmentNumber].value += addProgress;
    }

    private void MoneyEnding() //When money become 0, all expenses should be stopped method
    {
        
        if (gameManagerScr.moneyValue < 0)
        {
            ecoCurrentLvl = 0;
            gameManagerScr.ecoInvestmentsExpensesValue = 0;
        }
    }

    public void EcologyPlusButton()
    {
        if(gameManagerScr.moneyValue > (ecoBasicLvlCost * buttonMultiplier))
        {
            ecoCurrentLvl += 1 * buttonMultiplier;
            gameManagerScr.ecoInvestmentsExpensesValue += ecoBasicLvlCost * buttonMultiplier;
            
        }
    }

    public void EcologyMinusButton()
    {
        if (ecoCurrentLvl > 0)
        {
            if(ecoCurrentLvl < buttonMultiplier)
            {
                gameManagerScr.ecoInvestmentsExpensesValue -= ecoBasicLvlCost * ecoCurrentLvl;
                ecoCurrentLvl = 0;                
            } else
            {
                ecoCurrentLvl -= 1 * buttonMultiplier;
                gameManagerScr.ecoInvestmentsExpensesValue -= ecoBasicLvlCost * buttonMultiplier;
            }                       
        }
    }

    public void ButtonMultiplierSetting(int x)
    {
        buttonMultiplier = x;
    }
}
