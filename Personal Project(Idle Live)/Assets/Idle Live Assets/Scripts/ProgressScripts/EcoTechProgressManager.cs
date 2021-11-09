using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class EcoTechProgressManager : MonoBehaviour
{
    public TextMeshProUGUI lvlValueText;

    private Slider progressBar;

    private GameManager gameManagerScr;

    private float techProgressValue; //% of current lvl progress
    private int currentLvl;
    private float expMaxValue; //How much experience needed to reach next level
    public int techNumber;//Use to identify technology
    [HideInInspector] public bool isTechActive;
    
    public float ecoTechBasicCost; //Starting cost of technology
    private float ecoTechCurrentCost; //Current cost of technology

    // Start is called before the first frame update
    void Start()
    {
        expMaxValue = 100;
        lvlValueText.text = "0";

        progressBar = GetComponent<Slider>();
        progressBar.value = 0;
        gameManagerScr = GameObject.Find("GameManager").GetComponent<GameManager>();

        ecoTechCurrentCost = ecoTechBasicCost;

    }

    // Update is called once per frame
    void Update()
    {
        MoneyEnding();
        if (isTechActive == true)
        {
            TechProgress(Time.deltaTime * 50 * PlayerData.skillMultipliersArray[5] * (1 + PlayerPrefs.GetFloat("TechExpMult")));
        }
    }

    private void TechProgress(float addProgress)
    {
        techProgressValue += addProgress;
        progressBar.value += addProgress;
        if (techProgressValue >= expMaxValue)
        {
            techProgressValue = 0;
            progressBar.value = 0;
            currentLvl += 1;            
            gameManagerScr.techMultipliersArray[techNumber] *= 1.05f;
            gameManagerScr.ecoTechExpensesValue -= ecoTechCurrentCost; //We need to erase old value of skill, to assign new value            
            ecoTechCurrentCost *= gameManagerScr.costIncreaser[1];
            gameManagerScr.ecoTechExpensesValue += ecoTechCurrentCost;            
            lvlValueText.text = currentLvl.ToString();
            expMaxValue *= gameManagerScr.expHardener[2]; //Each next level need more exprience
            progressBar.maxValue = expMaxValue;
        }
    }

    public void TechActivation()
    {
        int x = 0;
        
        if(isTechActive == false && gameManagerScr.moneyValue > ecoTechCurrentCost)
        {
            isTechActive = true;
            x = 1;
            gameManagerScr.ecoTechExpensesValue += ecoTechCurrentCost;
        }

        if (isTechActive == true && x == 0)
        {
            isTechActive = false;
            gameManagerScr.ecoTechExpensesValue -= ecoTechCurrentCost;
        }
    }

    private void MoneyEnding() //When money become 0, all expenses should be stopped method
    {

        if (gameManagerScr.moneyValue < 0)
        {
            isTechActive = false;
            gameManagerScr.ecoTechExpensesValue = 0;
        }
    }
}
